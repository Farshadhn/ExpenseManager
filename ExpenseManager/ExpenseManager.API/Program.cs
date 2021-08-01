using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManager.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            #region Sentry/NLog

            //We used NLog.Targets.Sentry2 library formerly
            //But it was not based on NetStandard and used an unstable SharpRaven library
            //So we decided to replace it with a better library
            //The NLog.Targets.Sentry3 library supports NetStandard2.0 and uses an updated version of SharpRaven library.
            //But Sentry.NLog is the official sentry library integrated with nlog and better than all others.

            //NLog.Targets.Sentry3
            //https://github.com/CurtisInstruments/NLog.Targets.Sentry

            //Sentry SDK for .NET
            //https://github.com/getsentry/sentry-dotnet

            //Sample integration of NLog with Sentry
            //https://github.com/getsentry/sentry-dotnet/tree/master/samples/Sentry.Samples.NLog


            //Set deafult proxy
            //WebRequest.DefaultWebProxy = new WebProxy("http://127.0.0.1:8118", true) { UseDefaultCredentials = true };

            // You can configure your logger using a configuration file:

            // If using an NLog.config xml file, NLog will load the configuration automatically Or, if using a
            // different file, you can call the following to load it for you: 
            //LogManager.Configuration = LogManager.LoadConfiguration("NLog-file.config").Configuration;

            var logger = LogManager.GetCurrentClassLogger();

            // Or you can configure it with code:
            //UsingCodeConfiguration();

            #endregion

            try
            {
                logger.Debug("init main");
                await CreateHostBuilder(args).Build().RunAsync();
            }
            catch (Exception ex)
            {
                //NLog: catch setup errors
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                LogManager.Flush();
                LogManager.Shutdown();
            }
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureLogging(options => options.ClearProviders())
                .UseNLog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //webBuilder.ConfigureLogging(options => options.ClearProviders());
                    //webBuilder.UseNLog();
                    webBuilder.UseStartup<Startup>();
                });

    }
}
