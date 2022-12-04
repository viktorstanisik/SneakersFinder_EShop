using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop_Client_Services.Services.Token
{
    public interface ITokenService
    {
        string GenerateJwtToken(LoginModel model, int id);

    }
}
