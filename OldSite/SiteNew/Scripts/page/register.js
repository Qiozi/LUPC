 $(function () {
     $('#btnSignIn').on("click", function () {
         var username = $('#InputEmail1').val();
         var pwd1 = $('#InputPassword1').val();
         var pwd2 = $('#InputPassword2').val();

         $('.alert-danger').hide(500, function () {
             $(this).html("");
         });

         $.ajax({
             type: 'get',
             url: '/cmds/user.aspx',
             data: {
                 cmd: 'register',
                 username: username,
                 pwd1: pwd1,
                 pwd2: pwd2
             },
             error: function (g, s, t) {
                 $('.alert-danger').show(800, function () {
                     $(this).html(g);
                 });
             },
             success: function (m, s) {
                 if (s === "success") {
                     var r = eval('(' + m + ')');
                     if (r.result === "OK") {
                         if ($('#ReturnUrl').val() !== "") {
                             window.location.href = $('#ReturnUrl').val();
                         } else
                             window.location.href = "/myAccount.aspx";
                     } else {
                         if (r.msg.indexOf('Password') > -1) {
                             //$('#form-group-pwd1').addClass("has-error");
                         }
                         $('.alert-danger').show(800, function () {
                             $(this).html("<span class='glyphicon glyphicon-remove'></span> " + r.msg);
                         });
                     }

                 }
             }
         });
     });
});

 $('#InputEmail1').keydown(function (event) {

    if (event.keyCode === 13) {
        $('#InputPassword1').focus();
    }
 })
 $('#InputPassword1').keydown(function (event) {

     if (event.keyCode === 13) {
         $('#InputPassword2').focus();
     }
 })
 $('#InputPassword2').keydown(function (event) {
 
     if (event.keyCode === 13) {
         $('#btnSignIn').trigger('click');
     }
 })
 $(document).keydown(function (event) {
     if (event.keyCode === 13)
         $('#loginform').submit();
 });