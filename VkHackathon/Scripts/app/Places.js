
function getZoneType(radius) {
    if (radius < 300) {
        return 1;
    } else if (radius < 2400) {
        return 2;
    } else {
        return 3;
    }
}


function findPlacesInVK(coordinates, count, radius, query, callback) {
    VK.api("places.search",
        { "latitude": coordinates[0], "longitude": coordinates[1], "count": count, "radius": getZoneType(radius), "q": query },

        function (data) {
            var result = [];
            var places = data.response.filter(x => x.distance < radius).sort((a, b) => a.distance - b.distance);

            for (var iCnt = 0; iCnt < places.length; iCnt++) {
                result.push({
                    lat: places[iCnt].latitude,
                    lon: places[iCnt].longitude,
                    url: `https://vk.com/club${places[iCnt].group_id}`,
                    title: places[iCnt].title,
                    distance: places[iCnt].distance,
                    icon: places[iCnt].icon,
                    photo: places[iCnt].group_photo,
                    description: '',
                    popularity: undefined
                });
            }

            callback(result);
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

