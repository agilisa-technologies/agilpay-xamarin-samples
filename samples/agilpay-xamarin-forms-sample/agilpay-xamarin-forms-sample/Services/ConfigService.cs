using System;
using System.Collections.Generic;
using System.Text;

namespace agilpay_xamarin_forms_sample.Services
{
    class ConfigService : IConfigService
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set;  }

        public string UserId { get; set; }

        public string Identification { get; set; }
    }
}
