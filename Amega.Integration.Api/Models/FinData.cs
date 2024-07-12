namespace Amega.Integration.Api.Models;

public record FinData
{
    public required string Ticker { get; set; }
    public DateTime QuoteTimestamp { get; set; }
    public double BidPrice { get; set; }
    public double BidSize { get; set; }
    public double AskPrice { get; set; }
    public double AskSize { get; set; }
    public double MidPrice { get; set; }
}
