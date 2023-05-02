using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public enum ResponseStatus
    {
        ConnectionError = 503,
        Ok = 200,
        Added = 201,
        Error = 500,
        Unauthorized = 401,
        BadRequest = 400,
        NotFound = 404
    }
}
