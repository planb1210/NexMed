$(window).on('load', function () {
    $("#password1").change(function () {
        validatePassword();
    });

    $("#password2").change(function () {
        validatePassword();
    });

    $("#getWeather").click(function () {
        var data = { cityId: $("#cities").val() };
        var weather = new Weather();
        weather.takeWeather(data);
    });
});