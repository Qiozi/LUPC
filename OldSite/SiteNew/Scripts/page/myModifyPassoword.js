$(function () {
    function submitPassword() {
        var oldPassword = $('#oldPassword').val();
        var newPassword = $('#newPassword').val();
        var confirmPassword = $('#confirmPassword').val();

        ////var rememberMe = $('#cbRememberMe').is(":checked");
        $('.alert-danger').hide(500, function () {
            $(this).html("");
        });


        $.ajax({
            type: 'get',
            url: '/cmds/user.aspx',
            data: {
                cmd: 'modifyPassword',
                oldPassword: oldPassword,
                pwd1: newPassword,
                pwd2: confirmPassword
            },
            error: function (g, s, t) {
                //alert(s);
                util.alertError(s);
            },
            success: function (response) {
                if (response.indexOf('OK') > -1) {
                    util.alertSuccess(response);
                } else{
                    util.alertError(response);
                }
            }
        });
    }
    $('.btnSubmit').on('click', submitPassword);

    $('#oldPassword').keydown(function (event) {
        console.log(event)
        if (event.keyCode === 13) {
            $('#newPassword').focus();
        }
    })
    $('#newPassword').keydown(function (event) {
        console.log(event)
        if (event.keyCode === 13) {
            $('#confirmPassword').focus();
        }
    })
    $('#confirmPassword').keydown(function (event) {
        console.log(event)
        if (event.keyCode === 13) {
            submitPassword();
        }
    })
});
