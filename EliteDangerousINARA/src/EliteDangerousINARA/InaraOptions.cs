namespace NSW.EliteDangerous.INARA
{
    public class InaraOptions
    {
        /// <summary>
        /// INARA url. Default 'https://inara.cz/'
        /// </summary>
        public string Url { get; set; } = "https://inara.cz/";
        /// <summary>
        /// If you are developing the application and/or testing new updates, please set this as 'true'.
        /// This setting will skip the updates for global events like setCommunityGoal, which may otherwise cause the problems with your test data.
        /// Please note that newly accepted applications in development has this set by default on server side, but don't rely on it and set it on your side, too.
        /// </summary>
        public bool IsDevelopment { get; set; }
        /// <summary> Name of the application used </summary>
        public string ApplicationName { get; set; }
        /// <summary> Version of the application (like: '1.2.4', please keep this standard versioning format as there may be a version control in effect)</summary>
        public string ApplicationVersion { get; set; }
        /// <summary> User's personal API key (set by user) or generic application key (for general read-only events) </summary>
        public string ApiKey { get; set; }
        /// <summary>
        /// In-game CMDR name of user (not set by user, get this from journals or cAPI to ensure it is a correct in-game name to avoid future problems).
        /// It is recommended to be always set when no generic API key is used (otherwise some events may not work).
        /// </summary>
        public string Commander { get; set; }
        /// <summary>
        /// Commander's unique Frontier ID (is provided by journals since 3.3) in a format: 'F123456' or '123456'. When not known, set nothing.
        /// </summary>
        public string FrontierId { get; set; }
    }
}