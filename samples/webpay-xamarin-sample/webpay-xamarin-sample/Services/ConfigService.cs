using System;
using System.Collections.Generic;
using System.Text;

namespace webpay_xamarin_sample.Services
{
    class ConfigService : IConfigService
    {
        public string ClientId { get; set; }

        public string UserId { get; set; }

        public string Identification { get; set; }
    }
}
