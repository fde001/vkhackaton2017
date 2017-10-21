var ticketingMetadata = [];

function inititalizeTicketing() {
    $.get('/Metadata/Ticketing', function (data) {
        ticketingMetadata = data
    });
}

function getTicketingConfiguration(id) {
    return ticketingMetadata.find(function (elem) { return elem.ExternalId === (''+ id) });
}