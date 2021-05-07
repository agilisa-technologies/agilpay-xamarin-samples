namespace agilpay_xamarin_forms_sample.Services
{
    internal interface IConfigService
    {
        string ClientId { get; set; }
        string ClientSecret { get; set; }
        string UserId { get; set; }
        string Identification { get; set; }
    }
}