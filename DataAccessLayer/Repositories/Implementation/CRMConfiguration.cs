namespace DataAccessLayer.Repositories.Implementation;

public class CRMConfiguration
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public bool IsOnlineEnv { get; set; } = false;
}
