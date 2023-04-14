using Mango.Services.Email.Messaging;

namespace Mango.Services.Email.Extension
{
    public static class ApplicationBuilderExtensions
    {
        public static IAzureServiceBusConsumer ServiceBusConsumer { get; set; }

        public static IApplicationBuilder UseAzureServiceBusConsumer(this IApplicationBuilder app)
        {
            ServiceBusConsumer = app.ApplicationServices.GetService<IAzureServiceBusConsumer>();
            var hostApplicationLife = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            hostApplicationLife.ApplicationStarted.Register(OnStarted);
            hostApplicationLife.ApplicationStopping.Register(OnStopping);

            return app;
        }

        private static void OnStarted()
        {
            ServiceBusConsumer.Start();
        }

        private static void OnStopping()
        {
            ServiceBusConsumer.Stop();
        }
    }
}
