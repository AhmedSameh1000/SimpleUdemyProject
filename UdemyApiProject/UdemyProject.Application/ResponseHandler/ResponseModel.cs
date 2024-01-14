using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UdemyProject.Application.ResponseHandler
{
    public class ResponseModel<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public object Meta { get; set; }

        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }

        public T Data { get; set; }

        public ResponseModel()
        {
        }

        public ResponseModel(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public ResponseModel(string message)
        {
            Succeeded = false;
            Message = message;
        }

        public ResponseModel(string message, bool succeeded)
        {
            Succeeded = succeeded;
            Message = message;
        }
    }
}