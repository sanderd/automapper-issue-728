using System.Collections;
using System.Collections.Generic;

namespace FailureMap.ApiModel
{
    public class ApiUser
    {
        public string Username { get; set; }

        public IEnumerable<ApiDocument> Documents { get; set; } 
    }
}