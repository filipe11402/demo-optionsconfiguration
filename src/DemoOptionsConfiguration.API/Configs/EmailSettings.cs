namespace DemoOptionsConfiguration.API.Configs
{
    public class EmailSettings
    {
        public const string OutlookKey = "Outlook";
        public const string GmailKey = "Gmail";

        public string Server { get; set; }

        public int Port { get; set; }
    }
}
