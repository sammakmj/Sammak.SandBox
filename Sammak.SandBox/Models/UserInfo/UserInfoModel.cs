using Sammak.SandBox.Models.UserIdentity;
using System.Linq;

namespace Sammak.SandBox.Models.UserInfo
{
    public class UserInfoModel : ModelBase<UserInfoModel>
    {
        public UserIdentityModel UserIdentity { get; set; }
        public string UserName { get; private set; }
        public string Domain { get; private set; }
        public string EmailAddress { get; private set; }

        static UserInfoModel()
        {
        }

        public UserInfoModel(string userIdentity)
        {
            UserIdentity = (UserIdentityModel)userIdentity;
            ExtractUserNameAndDomain();
        }

        private void ExtractUserNameAndDomain()
        {
            var identity = (string)UserIdentity; // explicit cast
            if (string.IsNullOrEmpty(identity))
                return;
            if(identity.Contains("@"))
            {
                // the full text contains @ so it must be an email with two parts;
                // an email address may contain multiple @ character on the first part.  
                //the domain part is the string after the last occurance of the @ character.
                // the first part is assumed to be the user name and the second must be the domain part that may contain '.' character
                string[] split = identity.Split('@');
                string firstPart = string.Join("@", split.Take(split.Length - 1));
                string lastPart = split.Last();

                EmailAddress = identity;
                UserName = firstPart;
                Domain = ExtractDomain(lastPart);
            }
            else
            {
                // the text is in the <domain part>\<username> format
                var parts = identity.Split('\\');
                Domain = ExtractDomain(parts[0]);
                UserName = parts[1];
            }
        }

        private string ExtractDomain(string text)
        {
            // the domain name is anything befor the first '.' character
            return text.Split('.')[0];
        }
    }

}
