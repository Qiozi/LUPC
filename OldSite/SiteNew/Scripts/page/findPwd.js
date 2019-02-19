$(function () {
    $('#btnSubmit').on('click', function () {
        var user = $('#user').val();
        if (user.length > 3) {
            $.get("/cmds/user.aspx?cmd=findpwd&username=" + $('#user').val(), {}, function (data) {
                if (data.indexOf("success") > -1) {
                    $('#MsgInfo').html("<div class=\"alert alert-success\" role=\"alert\">" + data + "</div>");
                } else {
                    $('#MsgInfo').html("<div class=\"alert alert-danger\" role=\"alert\">" + data + "</div>");
                }
            });
        }
    })
})
$(document).keydown(function (event) {
    if (event.keyCode === 13)
        $('#btnSubmit').trigger('click');
});