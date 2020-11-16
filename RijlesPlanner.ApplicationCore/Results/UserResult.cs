using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RijlesPlanner.ApplicationCore.Results
{
    public class UserResult
    {
        public bool IsSucceed { get; }
        public string Error { get; }

        public UserResult(bool isSucceed)
        {
            IsSucceed = isSucceed;
        }

        public UserResult(bool isSucceed, string error)
        {
            IsSucceed = isSucceed;
            Error = error;
        }
    }
}
