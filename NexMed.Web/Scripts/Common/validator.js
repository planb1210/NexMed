$(window).on('load', function () {
    $("#password1").change(function () {
        validatePassword();
    });

    $("#password2").change(function () {
        validatePassword();
    });
});

function validatePassword() {
    var pass2 = $("#password2").val();
    var pass1 = $("#password1").val();

    if (pass1 != pass2) {
        $("#password2")[0].setCustomValidity("Passwords Don't Match");
    }
    else {
        $("#password2")[0].setCustomValidity('');
    }
}