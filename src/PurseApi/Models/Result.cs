﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurseApi.Models
{
    public class Result<T>
    {
        public bool Success { get; private set; } = true;
        public T Value { get; private set; }
        public Error Error { get; private set; }


        public Result() { }
        public Result (T value) 
        {
            this.Value = value;
        }

        private void SetErrorCodeAndMessage(int code, string message)
        {
            this.Error = new Error
            {
                Code = code,
                Message = message
            };
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
        public int Code { get; set; }
        public string Message { get; set; }
    }

}
