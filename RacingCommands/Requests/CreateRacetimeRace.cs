using System;
using System.Text;

namespace chocobot_racing.RacingCommands.Requests;

public class CreateRacetimeRace
{
    public string? Goal { get; set; } = string.Empty;
    public string? CustomGoal { get; set; } = null;
    public bool TeamRace { get; set; } = false;
    public bool Invitational { get; set; } = false;
    public bool Unlisted { get; set; } = false;
    public bool Partitionable { get; set; } = false;
    public bool HideEntrants { get; set; } = false;
    public bool Ranked { get; set; } = true;
    public string InfoUser { get; set; } = string.Empty;
    public string InfoBot { get; set; } = string.Empty;
    public bool RequireEvenTeams { get; set; } = false;
    public int StartDelay { get; set; } = 15;
    public int TimeLimit { get; set; } = 24;
    public bool TimeLimitAutoComplete { get; set; } = false;
    public bool StreamingRequired { get; set; } = true;
    public bool AutoStart { get; set; } = true;
    public bool AllowComments { get; set; } = true;
    public bool HideComments { get; set; } = true;
    public bool AllowPreraceChat { get; set; } = true;
    public bool AllowMidraceChat { get; set; } = true;
    public bool AllowNonEntrantChat { get; set; } = true;
    public int ChatMessageDelay { get; set; } = 0;

    public StringContent ToStringContent() =>
        new($"goal={Goal}&custom_goal={CustomGoal}&ranked={Ranked}&info_user={InfoUser}&start_delay={StartDelay}&time_limit={TimeLimit}&streaming_required={StreamingRequired}&auto_start={AutoStart}&allow_comments={AllowComments}&hide_comments={HideComments}&allow_prerace_chat={AllowPreraceChat}&allow_midrace_chat={AllowMidraceChat}&allow_non_entrant_chat={AllowNonEntrantChat}&chat_message_delay={ChatMessageDelay}&recordable=True", Encoding.UTF8, "application/x-www-form-urlencoded");
}
