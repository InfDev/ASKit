using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ASKit.Common.Mail;
using ASKit.Mail.MailKit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace ASKit.Mail.Extensions.DependencyInjection
{
    // https://learn.microsoft.com/en-us/dotnet/core/extensions/options-library-authors

    /// <summary>
    /// Mail service extensions
    /// </summary>
    public static class SkMailServiceCollectionExtensions
    {
        /// <summary>
        /// Register SkMailService and MkMailOptions with initialization from configuration.
        /// </summary>
        /// <param name="services"> Service collection</param>
        /// <param name="configuration">Full configuration or already provided section</param>
        /// <param name="configureOptions">Action for additional configure options</param>
        public static void AddSkMailService(this IServiceCollection services, 
            IConfiguration configuration, 
            Action<SkMailOptions>? configureOptions = null)
        {
            var cfg = configuration is IConfigurationSection ? configuration : configuration.GetSection(SkMailOptions.Section);
            services.Configure<SkMailOptions>(cfg);
            //if (configureOptions != null)
            //    services.Configure(configureOptions);
            services.AddTransient<ISkMailService, SkMailService>();
        }

        /// <summary>
        /// Register SkMailService and MkMailOptions with initialization via action.
        /// </summary>
        /// <param name="services"> Service collection</param>
        /// <param name="configureOptions">Action for configure options</param>
        /// <remarks></remarks>
        public static void AddSkMailService(this IServiceCollection services, Action<SkMailOptions> configureOptions)
        {
            services.Configure(configureOptions);
            services.AddScoped<ISkMailService, SkMailService>();
        }

    }
}