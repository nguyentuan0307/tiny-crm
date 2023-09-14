namespace IAM.Business.Models;

public class AuthenticationClaim
{
    public AuthenticationClaim(string type, string value)
    {
        Type = type;
        Value = value;
    }

    public string Type { get; set; }
    public string Value { get; set; }
}