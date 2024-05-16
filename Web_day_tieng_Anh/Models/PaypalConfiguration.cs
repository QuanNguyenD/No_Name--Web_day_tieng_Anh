using Microsoft.Extensions.Options;
using PayPal.Api;

namespace Web_day_tieng_Anh.Models
{
    public class PaypalConfiguration
    {
        private readonly PayPalSettings _payPalSettings;

        public PaypalConfiguration(IOptions<PayPalSettings> payPalSettings)
        {
            _payPalSettings = payPalSettings.Value;
        }

        public Dictionary<string, string> GetConfig()
        {
            return new Dictionary<string, string>
            {
                { "mode", _payPalSettings.Mode },
                { "connectionTimeout", _payPalSettings.ConnectionTimeout.ToString() },
                { "requestRetries", _payPalSettings.RequestRetries.ToString() },
                { "clientId", _payPalSettings.ClientId },
                { "clientSecret", _payPalSettings.ClientSecret }
            };
        }

        private string GetAccessToken()
        {
            // Getting access token from PayPal
            string accessToken = new OAuthTokenCredential(_payPalSettings.ClientId, _payPalSettings.ClientSecret, GetConfig()).GetAccessToken();
            return accessToken;
        }

        public APIContext GetAPIContext()
        {
            // Return APIContext object by invoking it with the access token
            APIContext apiContext = new APIContext(GetAccessToken());
            apiContext.Config = GetConfig();
            return apiContext;
        }
    }

}
