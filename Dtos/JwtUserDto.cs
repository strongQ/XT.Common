using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.Dtos
{
    public class JwtUserDto
    {
        public UserDto User { get; set; }

        public List<string> Roles { get; set; }



        public List<string> DataScopes { get; set; }
    }
}
