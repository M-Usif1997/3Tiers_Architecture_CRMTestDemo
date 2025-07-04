using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services.TokenProvider.Contract
{
    public interface ITokenProvider
    {
        string? GetBearerToken();
    }
}
