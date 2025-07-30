using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XT.Common.Extensions
{
    public static class JwtDecoder
    {
        // 将 JWT 的 exp (Unix 时间戳) 转换为 DateTime
        public static DateTime GetExpirationDate(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            try
            {
                var jwtToken = handler.ReadJwtToken(token);
            
                var expClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type.Equals("exp"));

                if (expClaim == null)
                {
                    // 如果没有过期声明，则认为它永不过期或立即过期，具体取决于您的策略
                    return DateTime.MaxValue;
                }

                long expValue = long.Parse(expClaim.Value);
                return DateTimeOffset.FromUnixTimeSeconds(expValue).UtcDateTime;
            }
            catch (Exception)
            {
                // 如果 token 无效或无法解析，则认为它已过期
                return DateTime.MinValue;
            }
        }
    }
}
