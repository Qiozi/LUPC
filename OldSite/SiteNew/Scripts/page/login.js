; $(function () {

    var validate = $("#loginform").validate({
        rules: {
            user: {
                required: true,
                maxlength: 50,
                minlength: 3,

            },
            pass: {
                required: true,
                maxlength: 16,
                minlength: 4
            }
        },
        messages: {
            user: {
                required: " Enter your user name.",
                minlength: " At least 2 characters are necessary",
                maxlength: " At least 50 characters are necessary"
            },
            pass: {
                required: " Enter your password.",
                minlength: " At least 4 characters are necessary",
                maxlength: " At least 16 characters are necessary"
            }
        },
        errorPlacement: function (error, element) {
            element.parent().parent().find('.error').html('');
            //if (element.is(":radio"))
            //    error.appendTo(element.parent());
            //else if (element.is(":checkbox"))
            //    error.appendTo(element.parent());
            //else if (element.is("input[name=captcha]"))
            //    error.appendTo(element.parent());
            //else {
            //    //error.insertAfter(element);
            //    element.parent().parent().find('.alert-danger').html(error);
            //}
            if (error[0].innerText != '') {
                error.appendTo(element.parent().parent().find('.error'));                
            }

            error.css({
                'color': 'red',
                'font-weight': 'normal'
            });
        },
        success: function (label) {
            //var display = false;
            //for (var i = 0; i < label.length; i++)
            //    if (label[i].innerText != '')
            //        display = true;
            //if (!display)
            //    $('#loginform').find('.alert-danger').css({ display: 'none' });

        }
    });
    $("input:reset").click(function () {
        validate.resetForm();
    });
});

function login() {
    var username = $('#user').val();
    var pwd = $('#pass').val();
    //var rememberMe = $('#cbRememberMe').is(":checked");
    $('.alert-danger').hide(500, function () {
        $(this).html("");
    });

    $.ajax({
        type: 'get',
        url: '/cmds/user.aspx',
        data: {
            cmd: 'login',
            username: username,
            pwd1: md5(pwd)
        },
        error: function (g, s, t) {
            //alert(s);
            //$('.alert-danger').show(1000).html(s);
            $('.alert-danger').show(800, function () {
                $(this).html(g);
            });
        },
        success: function (response) {
            response = JSON.parse(response);
            if (response.Success) {
                // window.localStorage.setItem('UserToken', response.Data);
                if ($('#ReturnUrl').val() !== "" && $('#ReturnUrl').val() !== null) {
                    window.location.href = $('#ReturnUrl').val();
                } else
                    window.location.href = "/myAccount.aspx";

            } else {
                $('#loginform').find('.error').html("<span style='color:red;'>"+ response.ErrMsg +"</span>"); //util.alertError();
            }
        }
    });
}

$(document).keydown(function (event) {
    if (event.keyCode === 13) {
        $('#loginform').submit();
    }
});

$('#user').keydown(function (event) {
    if (event.keyCode === 13) {
        $('#pass').focus();
    }
})