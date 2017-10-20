var myMap;
VK.callMethod("setTitle", "Changed title");
$("#placesSearch")[0].value = "Бар";



function funcSearch(radius) {

    myMap.geoObjects.removeAll();
    $("#places").empty();
    radius = radius || 1;

    var center = myMap.getCenter();
    var bounds = myMap.getBounds();
    var radius = ymaps.coordSystem.geo.getDistance(center, bounds[0]) / 1.5;

    findPlacesInVK(center, 200, radius, $("#placesSearch")[0].value,
        function (data) {

            var places = data.filter(x => x.photo != null &&
                ((x.start && x.end && x.end > new Date()) ||
                    (x.start && !x.end && x.end > new Date()) ||
                    (!x.start && !x.end))
            );

            for (var i = 0; i < places.length; i++) {
                $("#places").append(`<tr> <td><img src="${places[i].photo}"></td><td><a target="_blank" href="${places[i].url}">${places[i].title}</a></td><td>${places[i].distance} м</td></tr>`);

                myMap.geoObjects
                    .add(new ymaps.Placemark([places[i].lat, places[i].lon],
                        {
                            balloonContent: `<img src="${places[i].photo}"><a target="_blank" href="${places[i].url}">${places[i].title}</a>`
                        },
                        {
                            preset: 'islands#icon',
                            iconColor: '#0095b6'
                        }));
            }
        });
}


ymaps.ready(function () {
    myMap = new ymaps.Map('map',
        {
            center: [59.935634, 30.325935],
            zoom: 15,
            controls: ["zoomControl", "geolocationControl", "rulerControl"]
        },
        {
            searchControlProvider: 'yandex#search'
        });

    myMap.events.add('boundschange', function (event) {
        funcSearch();
    });

    funcSearch();

});