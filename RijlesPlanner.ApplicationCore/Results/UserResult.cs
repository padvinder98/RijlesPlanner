using System;
using System.Collections.Generic;

namespace RijlesPlanner.ApplicationCore.Results
{
    public class UserResult
    {
        public bool IsSucceedded { get; private set; } = true;
        public List<Error> Errors { get; }

        public UserResult()
        {
            Errors = new List<Error>();
        }

        public void AddUserResultError(Error error)
        {
            Errors.Add(error);
        }

        public void SetFailed()
        {
            IsSucceedded = false;
        }
    }

    public class Error
    {
        public string Description { get; }

        public Error(string description)
        {
            Description = description;
        }
    }
}
