using System.Threading.Tasks;

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
    using Models;
    using CommandParsers;


    public class WeatherModule
        : NancyModule
    {
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

                        return await client.GetWeatherAsync(postalCode, units);
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
            Task<string> GetWeatherAsync(string postalCode, Units unit);
        }

        public class WeatherClient
            : IWeatherClient
        {
            const string ZipCodeApiUrl = "http://api.openweathermap.org/data/2.5/weather?zip={0},us&appid={1}";

            static readonly Lazy<string> _apiKey =
                new Lazy<string>(() => ConfigurationManager.AppSettings["OpenWeatherMap-ApiKey"]);

            public async Task<string> GetWeatherAsync(string postalCode, Units unit)
            {
                var client = new HttpClient();
                var requestUrl = string.Format(ZipCodeApiUrl, postalCode, _apiKey.Value);
                var result = await client.GetStringAsync(requestUrl);

                JObject obj = JsonConvert.DeserializeObject<JObject>(result);

                switch (unit)
                {
                    case Units.Metric:
                        return string.Format("Currently it's {0}C in {1}.",
                            KelvinToMetric(obj["main"]["temp"].Value<float>()), obj["name"].Value<string>());
                    default:
                        return string.Format("Currently it's {0}F in {1}.",
                            KelvinToImperial(obj["main"]["temp"].Value<float>()), obj["name"].Value<string>());
                }
            }

            float KelvinToImperial(float value)
            {
                return (float) ((value - 273.15)*1.8) + 32;
            }

            float KelvinToMetric(float value)
            {
                return (float) (value - 273.15);
            }
        }
    }
}
