
namespace Sammak.SandBox.Models.Sso
{
    public class SsoAuthUriRequest : ModelBase<SsoAuthUriRequest>
    {
        public string CallBackUrl { get; set; }
        public string State { get; set; }
    }
}
