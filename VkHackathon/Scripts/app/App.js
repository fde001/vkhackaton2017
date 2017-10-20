﻿var map;
var lastQuery="";


VK.callMethod("setTitle", "Changed title");
$("#placesSearch")[0].value = "Бар";




function funcSearch() {

    $("#loading").show();

    var query = $("#placesSearch").val();

    var center = map.getCenter();
    var bounds = map.getBounds();
    var radius = ymaps.coordSystem.geo.getDistance(center, bounds[0]) / 1.5;

    findPlacesInVK(center, 400, radius, query,
        function (data) {

            var places = data.filter(x => x.photo != null &&
                ((x.start && x.end && x.end > new Date()) ||
                    (x.start && !x.end && x.end > new Date()) ||
                    (!x.start && !x.end))
            );

            var $places = $("#places");
            if (query != lastQuery) {
                map.geoObjects.removeAll();
                $places.empty();
            }

            for (var i = 0; i < places.length; i++) {

                if ($places.find('.' + places[i].id).length == 0) {

                    $places.append(`<tr id="${places[i].id}"> <td><img src="${places[i].photo}"></td><td><a target="_blank" href="${places[i].url}">${places[i].title}</a></td><td>${places[i].distance} м</td></tr>`);

                    map.geoObjects
                        .add(new ymaps.Placemark([places[i].lat, places[i].lon],
                            {
                                balloonContent: `<img src="${places[i].photo}"><a target="_blank" href="${places[i].url}">${places[i].title}</a>`
                            },
                            {
                                preset: 'islands#icon',
                                iconColor: '#0095b6'
                            }));
                }
            }

            lastQuery = query;
            $("#loading").hide();
        });
}


ymaps.ready(function () {
    map = new ymaps.Map('map',
        {
            center: [59.935634, 30.325935],
            zoom: 15,
            controls: ["zoomControl", "geolocationControl", "rulerControl"]
        },
        {
            searchControlProvider: 'yandex#search'
        });

    map.events.add('boundschange', function (event) {
        funcSearch();
    });

    funcSearch();

});