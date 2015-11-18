
$(document).ready(function () {
    $('#username').attr('disabled', true);
    $('#staffId').attr('disabled', true);
    $('#currentDate').attr('disabled', true);
    $('#productId').attr('disabled', true);
});


$(document).ready(function () {
    $("#btn").click(function () {
        $('#username').attr('disabled', false);
        $('#staffId').attr('disabled', false);
        $('#currentDate').attr('disabled', false);
        $('#productId').attr('disabled', false);
    });
});

function confirmation() {
    return confirm("Are you sure you want to delete this record");
};

