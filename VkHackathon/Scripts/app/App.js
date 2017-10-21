var map;
var lastQuery = "";
var places;


//VK.callMethod("setTitle", "Changed title");
$("#placesSearch")[0].value = "Бар";

$("#placesSearch").keydown(function (e) {
    if (e.keyCode == 13) {
    funcSearch();
    }
});

function funcSearch() {

    $("#loading").show();

    var query = $("#placesSearch").val();

    var center = map.getCenter();
    var bounds = map.getBounds();
    var radius = ymaps.coordSystem.geo.getDistance(center, bounds[0]);

    findPlaces(center, radius, query,
        function (data) {
                     data = data || [];
                     places = data.sort(function (a, b) {
                         return a.rating = b.rating;
                     }).slice(0,200);

            

            var $places = $("#places");
            if (query != lastQuery) {
                map.geoObjects.removeAll();
                $places.empty();
            }

            for (var i = 0; i < places.length; i++) {

                if ($places.find('.' + places[i].id).length == 0) {

                    $places.append(`<tr id="${places[i].id}"> <td><img src="${places[i].photo}"></td><td><a target="_blank" href="${places[i].url}">${places[i].title}</a></td><td>${places[i].distance} м</td></tr>`);

                    var placemark = new ymaps.Placemark([places[i].lat, places[i].lon],
                        {
                            //balloonContent: `<img src="${places[i].photo}"><a target="_blank" href="${places[i].url}">${places[i].title}</a>`
                        },
                        {
                            preset: 'islands#icon',
                            iconColor: '#0095b6'
                        });

                    function addPlaceClick(i) {
                        placemark.events.add('click', function () {

                            //$("#placeModal .modal-title").text(places[i].title);

                            $("#placeLink").attr("target", "_blank");
                            $("#placeLink").attr("href", places[i].url);
                            $("#placeLink").text(places[i].title);
                            $("#placeDesc").html(places[i].description);

                            if (!places[i].photo || places[i].photo == '') 
                                $("#placeImg").hide();
                            else
                                $("#placeImg").attr("src", places[i].photo).show();

                            $("#placeModal").modal("show");
                        });
                    }

                    addPlaceClick(i);

                    map.geoObjects.add(placemark);
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