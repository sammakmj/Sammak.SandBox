namespace Sammak.SandBox.Models.Requests
{
    public class SsoTokenRequest
    {
        public string Code { get; set; }

        public string RedirectUrl { get; set; }

    }
}