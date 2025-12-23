using BlogSystem.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Shared.CommonResult
{
    public class Error
    {
        private Error(string code, string description, ErrorType type)
        {
            Code = code;
            Description = description;
            Type = type;
        }

        public string Code { get;  }
        public string Description { get; }
        public ErrorType Type { get;  }

        //Methods Factory
        public static Error Failure(string code="Error.Failure",string description= "General Failure Has Occurred")
                    =>new Error(code, description, ErrorType.Failure);
        public static Error NotFound(string code= "Error.NotFound", string description= "Requested Resource Was Not Found")
                    =>new Error(code, description, ErrorType.NotFound);
        public static Error Unauthorized(string code= "Error.Unauthorized", string description= "Authentication Is Required")
                    =>new Error(code, description, ErrorType.Unauthorized);
        public static Error Validation(string code= "Error.Validation", string description = "Validation Failed")
                    =>new Error(code, description, ErrorType.Validation);
        public static Error Forbidden(string code= "Error.Forbidden", string description= "Access Is Forbidden")
                    =>new Error(code, description, ErrorType.Forbidden);
        public static Error InvalidCredentials(string code= "Error.InvalidCredentials", string description= "Invalid Credentials Provided")
                    =>new Error(code, description, ErrorType.InvalidCredentials);

    }
}
