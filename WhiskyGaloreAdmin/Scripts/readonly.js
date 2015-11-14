
$(document).ready(function () {
    $('#username').attr('disabled', true);
});

$(document).ready(function () {
    $("#btn").click(function () {
        $('#username').attr('disabled', false);
    });
});

function confirmation() {
    return confirm("Are you sure you want to delete this record");
};

