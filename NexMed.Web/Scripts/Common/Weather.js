class Weather {
    constructor() {
        var self = this;
        self.url = "/Member/GetWeather";
        self.type = "POST";
    }

    async takeWeather(args) {
        var self = this;
        var jsonData = await self.getJsonWeatherData(args);
        self.setWeatherData(jsonData);
    }

    async getJsonWeatherData(args) {
        var self = this;
        var result = await $.ajax({
            url: self.url,
            type: self.type,
            data: args
        });

        return result;
    }

    setWeatherData(result) {
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
}