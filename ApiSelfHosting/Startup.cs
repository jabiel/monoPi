using System;
using Microsoft.Owin.Hosting;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ApiSelfHosting.Startup))]

namespace ApiSelfHosting
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
#if DEBUG
            app.UseErrorPage();
#endif


            app.UseWelcomePage(new Microsoft.Owin.Diagnostics.WelcomePageOptions()
            {
                Path = new PathString("/welcome")
            });

            app.Run(context =>
            {
                context.Response.ContentType = "text/html";

                string output = string.Format(
                    "<p>I'm running on {0} </p><p>From assembly {1}</p>",
                    Environment.OSVersion,
                    System.Reflection.Assembly.GetEntryAssembly().FullName
                    );

                return context.Response.WriteAsync(output);

            });
        }
    }
}
