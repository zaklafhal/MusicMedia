using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMedia.Models.Dto
{
    public class UserInfo
    {
        public string Email { get; set; }
        public string Name { get; set; }

        public UserInfo()
        {

        }
        public UserInfo(ApplicationUser user)
        {
            Email = user.Email;
            Name = user.Name;
        }
        public override string ToString()
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            string userInfo = JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });
            return userInfo;
        }
    }
}
