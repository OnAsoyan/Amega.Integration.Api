using Amega.Integration.Api.Services;
using Microsoft.Extensions.Options;

namespace Amega.Integration.Api.Configuration;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddPriceService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddOptions<TiingoSettings>()
                         .BindConfiguration(TiingoSettings.SectionName)
                         .ValidateDataAnnotations()
                         .ValidateOnStart();

        serviceCollection.AddHttpClient("Tiingo", (serviceProvider, httpClient) =>
        {
            var tiingoSettings = serviceProvider.GetRequiredService<IOptions<TiingoSettings>>().Value; 
            httpClient.BaseAddress = new Uri($"{tiingoSettings.ApiURL}/top?token={tiingoSettings.ApiToken}&tickers=");
        });

        serviceCollection.AddTransient<IInstrumentPriceService, InstrumentPriceService>();

        return serviceCollection;
    }
}
