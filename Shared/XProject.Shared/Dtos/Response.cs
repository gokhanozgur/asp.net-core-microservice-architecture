﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace XProject.Shared.Dtos
{
    public class Response<T>
    {

        public T Data { get; private set; }

        // Do not add response data.
        [JsonIgnore]
        public int StatusCode { get; private set; }

        [JsonIgnore]
        public bool IsSuccessful { get; private set; }

        public List<string> Errors { get; set; }


        // Static factory methods.

        /*public static Response<T> Success(T data, int statusCode)
        {
            return new Response<T> { Data = data, StatusCode = statusCode, IsSuccessful = true };
        }*/

        /*public static Response<T> Fail(List<string> errors, int statusCode)
        {
            return new Response<T>
            {
                Errors = errors,
                StatusCode = statusCode,
                IsSuccessful = false
            };
        }*/

        public static Response<T> Success(T data, int statusCode) => new Response<T> { Data = data, StatusCode = statusCode, IsSuccessful = true };

        public static Response<T> Success(int statusCode) => new Response<T> { Data = default(T), StatusCode = statusCode, IsSuccessful = true };

        public static Response<T> Fail(List<string> errors, int statusCode) => new Response<T>
        {
            Errors = errors,
            StatusCode = statusCode,
            IsSuccessful = false
        };

        public static Response<T> Fail(string error, int statusCode) => new Response<T> { 
            Errors = new List<string>() { error }, 
            StatusCode = statusCode, 
            IsSuccessful = false 
        };

    }
}
