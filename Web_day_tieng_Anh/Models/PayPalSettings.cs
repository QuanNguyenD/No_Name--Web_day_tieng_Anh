namespace Web_day_tieng_Anh.Models
{
    public class PayPalSettings
    {
        public string Mode { get; set; }
        public int ConnectionTimeout { get; set; }
        public int RequestRetries { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
