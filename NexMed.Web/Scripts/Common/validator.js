$(window).on('load', function () {
    $("#password1").change(function () {
        validatePassword();
    });

    $("#password2").change(function () {
        validatePassword();
    });

    $("#getWeather").click(function () {
        var data = { cityId: $("#cities").val() };
        $.post("/Member/GetWeather", data, getWeather );
    });
});

function validatePassword() {
    var pass2 = $("#password2").val();
    var pass1 = $("#password1").val();

    if (pass1 !== pass2) {
        $("#password2")[0].setCustomValidity("Passwords Don't Match");
    }
    else {
        $("#password2")[0].setCustomValidity('');
    }
}

function getWeather(result) {
    if (result) {
        $("#weather_info_error").addClass('hidden');
        $("#weather_info").removeClass("hidden");

        $("#weather_info #temperature").html(result.Temperature);
        $("#weather_info #wind_speed").html(result.WindSpeed);
        $("#weather_info #pressure").html(result.Pressure);
    }
    else {
        $("#weather_info").addClass('hidden');
        $("#weather_info_error").removeClass("hidden");
    }
}