using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurseApi.Models
{

    public class Result
    {
        public bool Success { get; private set; } = true;
        public Error Error { get; private set; }

        public void SetErrorCodeAndMessage(int code, string message)
        {
            this.Success = false;
            this.Error = new Error(code, message);
        }

        public void SetValidationError(string key, string value)
        {
            this.Error.ValidationErrors[key] = value;
        }
    }

    public class Result<T>: Result
    {
        public T Value { get; private set; }

        public Result() { }
        public Result (T value) 
        {
            this.Value = value;
        }

        public Result<T> SetServerError(string message)
        {
            SetErrorCodeAndMessage(500, message);
            return this;
        }

        public Result<T> SetUnprocessable(string message)
        {
            SetErrorCodeAndMessage(422, message);
            return this;
        }

        public Result<T> SetNotFound(string message)
        {
            SetErrorCodeAndMessage(404, message);
            return this;
        }

    }

    public class Error
    {
        public int Code { get; }
        public string Message { get; }
        public Dictionary<string, string> ValidationErrors { get; }

        public Error(int code, string message)
        {
            Code = code;
            Message = message;
            ValidationErrors = new Dictionary<string, string>();
        }
    }

}
