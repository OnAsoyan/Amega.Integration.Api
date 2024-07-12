namespace Amega.Integration.Api.Configuration;

public class TiingoSettings
{
    public static string SectionName = "Tiingo";
    public required string ApiURL { get; set; }
    public required string ApiToken { get; set; } 
}
