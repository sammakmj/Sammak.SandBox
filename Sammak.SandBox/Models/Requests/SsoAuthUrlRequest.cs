namespace Sammak.SandBox.Models.Requests
{
    public class SsoAuthUriRequest
    {
        public string CallBackUrl { get; set; }
        public string State { get; set; }
    }
}