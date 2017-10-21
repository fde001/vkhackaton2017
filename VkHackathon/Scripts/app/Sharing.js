var friends = [];
var templates = {};


function initializeSharing() {
    VK.api("friends.get", {
        fields: [ 'photo_50']
    },
        function (data) {
            if (!data && !data.response) return;
            for (var iF = 0; iF < data.response.length; iF++) {
                friends.push({
                    uid: data.response[iF].uid,
                    first_name: data.response[iF].first_name,
                    last_name: data.response[iF].last_name,
                    photo: data.response[iF].photo_50,
                });
            }
        });

    $.get('/Metadata/SharingTemplates', function (data) {
        templates = data
    });
}

function findUnknown(place, time) {
    VK.api("users.get",
        {
        },
        function (data) {
            var pl = $.extend({}, place);
            pl.description = "";

            $.post('/Suggest/Random', {
                user: data.response[0], time: ((time.getTime() * 10000) + 621355968000000000), place: pl 
            });
    });
}

function createFriendsMessage(place, time, friends) {
    var timeString = time.toString();
    var title = templates.FriendsChatTitle.replace("{{placeTitle}}", place.title).replace("{{time}}", timeString);
    var message = templates.FriendsChat.replace("{{placeTitle}}", place.title).replace("{{time}}").replace("{{url}}");

    $.post('/Suggest/Friends', { friends: friends, title: title, message: message });
}

