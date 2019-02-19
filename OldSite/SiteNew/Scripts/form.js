var MyValidator = function() {
    var handleSubmit = function() {
        $('.form-horizontal').validate({
            errorElement : 'span',
            errorClass : 'help-block',
            focusInvalid : false,
            rules : {
                name : {
                    required : true,
                    email: true
                },
                password : {
                    required : true,
                    minlength: 4,
                    maxlength: 10
                }
            },
            messages : {
                name : {
                    required: "Username is required.",
                    email:"format is error."
                },
                password : {
                    required: "Password is required.",
                    minlength: "Password minlength 4",
                    maxlength: "Password maxlength 10"
                }
            },

            highlight : function(element) {
                $(element).closest('.form-group').addClass('has-error');
            },

            success : function(label) {
                label.closest('.form-group').removeClass('has-error');
                label.remove();
            },

            errorPlacement : function(error, element) {
                element.parent('div').append(error);
            },

            submitHandler : function(form) {
                form.submit();
            }
        });

        $('.form-horizontal input').keypress(function(e) {
            if (e.which == 13) {
                if ($('.form-horizontal').validate().form()) {
                    $('.form-horizontal').submit();
                }
                return false;
            }
        });
    }
    return {
        init : function() {
            handleSubmit();
        }
    };

}();
