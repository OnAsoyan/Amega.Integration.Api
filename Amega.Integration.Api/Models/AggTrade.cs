namespace Amega.Integration.Api.Models;

public class AggTrade
{
    public string e { get; set; }    // Event type
    public long E { get; set; }      // Event time
    public string s { get; set; }    // Symbol
    public long a { get; set; }      // Aggregate trade ID
    public string p { get; set; }    // Price
    public string q { get; set; }    // Quantity
    public long f { get; set; }      // First trade ID
    public long l { get; set; }      // Last trade ID
    public long T { get; set; }      // Trade time
    public bool m { get; set; }      // Is the buyer the market maker?
    public bool M { get; set; }      // Ignore
}
