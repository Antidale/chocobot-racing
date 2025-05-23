
namespace chocobot_racing.RollCommand.DTOs;

public abstract class FeApiResponse
{
    public string Status { get; set; } = string.Empty;
    public string Error { get; set; } = string.Empty;

    public static T SetError<T>(string error) where T : FeApiResponse, new()
    {
        return new T
        {
            Status = "error",
            Error = error
        };
    }
}
