using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMedia.Models.Dto
{
    public class Token
    {
        public string Access_Token { get; set; }

        public Token(string access_token)
        {
            Access_Token = access_token;
        }
    }
}
