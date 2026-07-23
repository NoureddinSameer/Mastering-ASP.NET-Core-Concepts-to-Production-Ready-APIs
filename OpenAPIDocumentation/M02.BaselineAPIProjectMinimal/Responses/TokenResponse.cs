namespace M02.BaselineAPIProjectMinimal.Responses;

public class TokenResponse
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime Expires { get; set; }
}