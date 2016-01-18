using Nancy.Responses;

namespace BryanPorter.SlackCmd.Modules
{
    using Nancy;
    using Nancy.ModelBinding;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Models;
    using CommandParsers;

    public class WeatherModule
        : NancyModule
    {
        const string IconUrlFormat = "http://openweathermap.org/img/w/{0}.png";
        const string ShareArgument = "share";

        public WeatherModule(IWeatherCommandParser parser, IWeatherClient client)
        {
            Post["/weather", true] = async (_, token) =>
            {
                var request = this.Bind<SlackRequest>();

                try
                {
                    Command c = null;
                    if (parser.TryParse(request, out c))
                    {
                        var postalCode = c.Arguments.First();
                        var units = c.Arguments.Count() == 2
                            ? (Units) Enum.Parse(typeof (Units), c.Arguments.ElementAt(1), true)
                            : Units.Imperial;

                        var responseType =
                            c.Arguments.Any(arg => arg.Equals(ShareArgument, StringComparison.OrdinalIgnoreCase))
                                ? ResponseType.InChannel
                                : ResponseType.Ephemeral;

                        var weatherData = await client.GetWeatherAsync(postalCode, units);
                        var currentCondition = weatherData.WeatcherCondition.FirstOrDefault();

                        var unitString = units == Units.Imperial ? "F" : "C";
                        var currentConditionIconUrl = string.Empty;
                        var currentConditionText = string.Empty;

                        if (currentCondition != null)
                        {
                            currentConditionIconUrl = string.Format(IconUrlFormat, currentCondition.IconId);
                            currentConditionText = $"{currentCondition.Main} - {currentCondition.Description}";
                        }

                        return
                            new SlackResponse()
                            {
                                ResponseType = ResponseType.Ephemeral,
                                Text = $"Here's the current weather for {weatherData.CityName}!",
                                Attachments = new[]
                                {
                                    new SlackResponseAttachment()
                                    {
                                        Title = "Current Conditions",
                                        ThumbUrl = currentConditionIconUrl,
                                        Text = currentConditionText,
                                        Fields = new []
                                        {
                                            new SlackField()
                                            {
                                                Title = "Temperature",
                                                Value = $"{Math.Round(weatherData.Weather.Temperature, 2)}° {unitString}",
                                                IsShort = true
                                            },
                                            new SlackField()
                                            {
                                                Title = "Humidity",
                                                Value = $"{weatherData.Weather.Humidity}%",
                                                IsShort = true
                                            }, 
                                            new SlackField()
                                            {
                                                Title = "Pressure",
                                                Value = $"{weatherData.Weather.Pressure} hPa",
                                                IsShort = true
                                            }, 
                                        }
                                    }
                                }
                            };
                    }
                }
                catch
                {
                    // TODO: don't filter critical exception types
                    return DefaultResponse();
                }

                return DefaultResponse();
            };
        }

        string DefaultResponse()
        {
            return "Sorry, I didn't understand that.";
        }

        public enum Units
        {
            Imperial,
            Metric
        }

        public interface IWeatherClient
        {
            Task<WeatherResponse> GetWeatherAsync(string postalCode, Units unit);
        }

        public class WeatherClient
            : IWeatherClient
        {
            const string ZipCodeApiUrl = "http://api.openweathermap.org/data/2.5/weather?zip={0},us&appid={1}&units={2}";

            static readonly Lazy<string> _apiKey =
                new Lazy<string>(() => ConfigurationManager.AppSettings["OpenWeatherMap-ApiKey"]);

            public async Task<WeatherResponse> GetWeatherAsync(string postalCode, Units unit)
            {
                var client = new HttpClient();
                var requestUrl = string.Format(ZipCodeApiUrl, postalCode, _apiKey.Value, unit);
                var result = await client.GetStringAsync(requestUrl);

                return JsonConvert.DeserializeObject<WeatherResponse>(result);

                //JObject obj = JsonConvert.DeserializeObject<JObject>(result);

                //string temperatureString;

                //switch (unit)
                //{
                //    case Units.Metric:
                //        temperatureString =
                //            $"Currently it's {KelvinToMetric(obj["main"]["temp"].Value<float>())}C in {obj["name"].Value<string>()}.";
                //        break;
                //    default:
                //        temperatureString =
                //            $"Currently it's {KelvinToImperial(obj["main"]["temp"].Value<float>())}F in {obj["name"].Value<string>()}.";
                //        break;
                //}

                //return temperatureString;
            }

            //float KelvinToImperial(float value)
            //{
            //    return (float) ((value - 273.15)*1.8) + 32;
            //}

            //float KelvinToMetric(float value)
            //{
            //    return (float) (value - 273.15);
            //}
        }
    }
}
