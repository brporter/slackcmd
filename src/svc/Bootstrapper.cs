using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BryanPorter.SlackCmd.CommandParsers;
using BryanPorter.SlackCmd.Modules;
using Nancy.Bootstrappers.Ninject;
using Ninject;

namespace BryanPorter.SlackCmd
{
    public class Bootstrapper
        : NinjectNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(IKernel existingContainer)
        {
            existingContainer.Bind<ICommandParser>().To<WeatherCommandParser>();

            // Weather Module
            existingContainer.Bind<IWeatherCommandParser>().To<WeatherCommandParser>();
            existingContainer.Bind<WeatherModule.IWeatherClient>().To<WeatherModule.WeatherClient>();
        }
    }
}
