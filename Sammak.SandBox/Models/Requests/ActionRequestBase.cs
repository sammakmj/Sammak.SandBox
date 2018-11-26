namespace Sammak.SandBox.Models.Requests
{
    public abstract class ActionRequestBase
    {

        protected string NormalizeParam(string param)
        {
            return param?.Trim().ToLowerInvariant();
        }
    }
}