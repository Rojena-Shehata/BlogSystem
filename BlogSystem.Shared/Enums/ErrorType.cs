using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Shared.Enums
{
    public enum ErrorType
    {
        Failure = 0,
        NotFound = 1,
        Unauthorized = 2,
        Validation=3,
        Forbidden=4,
        InvalidCredentials=5
    }
}
