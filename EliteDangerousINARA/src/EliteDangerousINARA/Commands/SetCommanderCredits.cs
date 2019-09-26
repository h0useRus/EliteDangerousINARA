using Newtonsoft.Json;

namespace NSW.EliteDangerous.INARA.Commands
{
    /// <summary>
    /// Sets current credits, assets and loans and a record is added to the credits log (if the credits/assets value differs).
    /// <remarks>
    /// Warning: Do NOT set credits/assets unless you are absolutely sure they are correct. The journals currently doesn't contain crew wage cuts, so credit gains are very probably off for most of the players. Also, please, do not send each minor credits change, as it will spam player's credits log with unusable data and they won't be most likely very happy about it. It may be good to set credits just on the session start, session end and on the big changes or in hourly intervals.
    /// There may be some separate transaction log for every minor credits change introduced later, which won't mess player's main credits log.
    /// </remarks>
    /// </summary>
    public class SetCommanderCredits : Command
    {
        internal override string CommandName => "setCommanderCredits";
        [JsonProperty("commanderCredits")]
        public long Credits { get; internal set; }
        [JsonProperty("commanderAssets")]
        public long? Assets { get; set; }
        [JsonProperty("commanderLoan")]
        public long? Loan { get; set; }

        public SetCommanderCredits(long credits)
        {
            Credits = credits;
        }
    }
}