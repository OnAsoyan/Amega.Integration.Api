using Amega.Integration.Api.Configuration;
using Amega.Integration.Api.Services;
using Amega.Integration.Api.Socket;

namespace Amega.Integration.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddPriceService();
            builder.Services.AddSingleton<IFinInstrumentService, FinInstrumentServiceMock>();

            builder.Services.AddHostedService<PriceUpdateService>();

            var app = builder.Build(); 

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseWebSockets();
            app.Use(async (context, next) =>
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    await WebSocketHandler.HandleAsync(webSocket);
                }
                else
                {
                    await next();
                }
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
