
function getZoneType(radius) {
    if (radius < 100) {
        return 1;
    } else if (radius < 1500) {
        return 2;
    } else if (radius < 15000) {
        return 3;
    } else
        return 4;
}


function findPlacesInVK(coordinates, count, radius, query, callback) {
    VK.api("places.search",
        { "latitude": coordinates[0], "longitude": coordinates[1], "count": count, "radius": getZoneType(radius), "q": query },

        function (data) {
            var result = [];

            if (!data.response || data.response[0] == 0) {
                callback(result);
                return;
            }

            var places = data.response.filter(x => x.distance < radius && x.group_id).sort((a, b) => a.distance - b.distance);

            var ids = places.map(function (p) { return p.group_id; }).join();

            VK.api("groups.getById", {
                group_ids: ids, fields: [  'is_closed', 'deactivated', 'is_member', 'members_count', 'type', 'description', 'site', 'start_date', 'finish_date', 'verified'] },
                function (data) {
                    var groups = data.response.filter(x => x.deactivated == undefined &&
                        (x.is_closed == 0 || x.is_member == 1))

                    for (var iCnt = 0; iCnt < places.length; iCnt++) {
                        var grp = groups.find(
                            function (group) {
                                return group.gid === places[iCnt].group_id;
                            });

                        if (!grp) continue;

                        result.push({
                            id: places[iCnt].pid,
                            lat: places[iCnt].latitude,
                            lon: places[iCnt].longitude,
                            url: `https://vk.com/club${grp.gid}`,
                            title: places[iCnt].title,
                            distance: places[iCnt].distance,
                            icon: places[iCnt].icon,
                            photo: places[iCnt].group_photo,
                            description: grp.description,
                            members: grp.members_count,
                            site: grp.site,
                            end: grp.finish_date ? new Date(parseInt(grp.finish_date)) : undefined,
                            start: grp.start_date ? new Date(parseInt(grp.start_date)) : undefined,
                            source_start : grp.start_date

                        });
                    }

                    result = result.filter(x => x.photo != null &&
                                    ((x.start && x.end && x.end > new Date()) ||
                                    (x.start && !x.end && x.end > new Date()) ||
                                    (!x.start && !x.end))
                                );
                    callback(result);
                }
            );



            

            
        }
    );
}

function findPlacesInDb(coordinates, radius, query, callback) {
    $.get('/Query', { lat: coordinates[0], lon: coordinates[1], dist: radius, query: query},
        function (data) {

            var result = [];

            for (var iCnt = 0; iCnt < data.length; iCnt++) {
                result.push({
                    id: data[iCnt].Id,
                    lat: data[iCnt].Lat,
                    lon: data[iCnt].Lon,
                    url: data[iCnt].Url,
                    title: data[iCnt].Title,
                    distance: data[iCnt].Distance,
                    //photo: places[iCnt].group_photo,
                    description: data[iCnt].Description,
                    //members: grp.members_count,
                    site: data[iCnt].Site
                });
            }


            callback(result);
        }
    );

    
}

function findPlaces(coordinates, radius, query, callback) {
    var vkData = undefined;
    var dbData = undefined;
    var processed = false;

    findPlacesInVK(coordinates, 400, radius, query, function (data) {
        vkData = data;
    });
    findPlacesInDb(coordinates, radius, query, function (data) {
        dbData = data;
    });

    function mergeData() {
        if (!vkData || !dbData) {
            setTimeout(mergeData, 100);
            return;
        }

        processed = true;
        var result = [];

        for (var iVK = 0; iVK < vkData.length; iVK++) {
            result.push(vkData[iVK]);
        }


        for (var iDb = 0; iDb < dbData.length; iDb++) {
            var foundDuplicate = false;
            for (var iVK = 0; iVK < vkData.length; iVK++) {
                foundDuplicate = foundDuplicate || checkSame(vkData[iVK], dbData[iDb]);
                if (foundDuplicate) break;
            }

            if (!foundDuplicate) result.push(dbData[iDb]);
        }

        callback(result);   

    }

    function checkSame(vkObj, dbObj) {
        if (calcCrow(vkObj.lat, vkObj.lon, dbObj.lat, dbObj.lon) > 100)
            return false;
        if (levenshteinDistance(vkObj.title.toLowerCase(), dbObj.title.toLowerCase()) > vkObj.title.length * 0.4)
            return false;   
       
        return true;
    }

    setTimeout(mergeData, 100);

    setTimeout(function () {
        if (!processed) {
            vkData = vkData || [];
            dbData = dbData || [];
            mergeData();
        }
    }, 3000)
}