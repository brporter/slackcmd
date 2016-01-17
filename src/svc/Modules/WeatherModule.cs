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
        const string ZipCodeApiUrl = "http://api.openweathermap.org/data/2.5/weather?zip={0},us&appid={1}&units=imperial";

        static readonly Lazy<string> _apiKey = new Lazy<string>(() => ConfigurationManager.AppSettings["OpenWeatherMap-ApiKey"]);

        public WeatherModule()
        {
            Post["/weather", true] = async (_, token) =>
            {
                var request = this.Bind<SlackRequest>();
                var parser = new WeatherCommandParser();

                Command c = null;
                if (parser.TryParse(request, out c))
                {
                    var client = new HttpClient();
                    var requestUrl = string.Format(ZipCodeApiUrl, c.Arguments.First(), _apiKey.Value);
                    var result = await client.GetStringAsync(requestUrl);

                    JObject obj = JsonConvert.DeserializeObject<JObject>(result);

                    string response = string.Format("Currently it's {0}F in {1}.", obj["main"]["temp"].Value<float>(),
                        obj["name"].Value<string>());

                    return response;
                }

                return "Got it.";
            };
        }
    }
}
