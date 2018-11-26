namespace Sammak.SandBox.Models.UserIdentity
{
    /// <summary>
    /// This model class is in essense a single string object.  Being purposed for UserIdentityModel, it will have its own 
    /// validator.  The object of this class can explicitly be converted to and from string
    /// </summary>
    public class UserIdentityModel : ModelBase<UserIdentityModel>
    {
        //private string _value { get; set; }
        public string UserIdentityString { get; set; }

        public UserIdentityModel(string value)
        {
            UserIdentityString = value;
        }

        public static explicit operator UserIdentityModel(string userIdentityString)
        {
            return new UserIdentityModel(userIdentityString);
        }

        public static explicit operator string(UserIdentityModel model)
        {
            return model.UserIdentityString;
        }

    }

}
