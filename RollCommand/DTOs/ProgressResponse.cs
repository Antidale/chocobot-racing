
namespace chocobot_racing.RollCommand.DTOs;

/// <summary>
/// A class encapsulating the data that is returned from hitting the /generate or /task endpoints for Free Enterprise seed generation
/// </summary>
public class ProgressResponse : FeApiResponse
{
    public string task_id { get; set; } = string.Empty;
    public string seed_id { get; set; } = string.Empty;

    public void Deconstruct(out string status, out string seedId, out string error)
    {
        seedId = seed_id;
        status = Status;
        error = Error;
    }
}
