using System;

namespace chocobot_racing.Constants;

public class FeStatusConstants
{
    /// <summary>
    /// Indicates a task is complete.
    /// </summary>
    public const string Done = "done";
    /// <summary>
    /// Indicates that the request is in some sort of error state.
    /// </summary>
    public const string Error = "error";
    /// <summary>
    /// Indicates that a task is in progress.
    /// </summary>
    public const string InProgres = "in_progress";
    /// <summary>
    /// Indicates that a task has yet to start.
    /// </summary>
    public const string Pending = "pending";
    /// <summary>
    /// Inidcates that the call completed successfully.
    /// </summary>
    public const string Okay = "ok";
    /// <summary>
    /// Indicates that the seed already exists. Check the seed_id property.
    /// </summary>
    public const string Exists = "exists";
}
