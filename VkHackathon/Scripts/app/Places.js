
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

                    callback(result);
                }
            );



            

            
        }
    );
}

function findPlacesInOpenData(coordinates, count, radius, query, callback) {
    //museum-exhibits
    //egrkn
    //stat_zoo
    //cinema
    //concert_halls
    //events
    //museums
    //parks
    //movie_gross
    //theaters
    //philharmonic
    //circuses
}

