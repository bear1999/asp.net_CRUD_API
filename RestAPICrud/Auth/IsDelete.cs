using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPICrud.Auth
{
    public class IsDelete : IAuthorizationRequirement
    {
        public bool CheckIsDelete;
        public IsDelete(bool _isDelete)
        {
            CheckIsDelete = _isDelete;
        }
    }
}
