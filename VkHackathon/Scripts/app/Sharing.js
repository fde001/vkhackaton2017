var friends = [];


function initializeSharing() {
    VK.api("friends.get", {
        fields: [ 'photo_50']
    },
        function (data) {
            if (!data && !data.response) return;
            for (var iF = 0; iF < data.response.length; iF++) {
                friends.push(new {
                    uid: data.response[iF].uid,
                    first_name: data.response[iF].first_name,
                    last_name: data.response[iF].last_name,
                    photo: data.response[iF].photo_50,
                });
            }
    });
}