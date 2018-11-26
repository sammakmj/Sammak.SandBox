namespace Sammak.SandBox.Models.TokenString
{
    public class TokenStringModel : ModelBase<TokenStringModel>
    {
        public string TokenString { get; set; }

        public TokenStringModel(string value)
        {
            TokenString = value;
        }

        public static explicit operator TokenStringModel(string tokenString)
        {
            return new TokenStringModel(tokenString);
        }

        public static explicit operator string (TokenStringModel model)
        {
            return model.TokenString;
        }
    }
}
