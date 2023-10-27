using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Response
{
    public class LoginResponse
    {
        public long loginStatus { get; set; }
    }

    public class Response<T> where T : class
    {


        public Response()
        {
            this.status = ResponseTypeContants.FAIL;
            this.apiStatus = ApiStatusConstants.NOT_COMPLETED;
            this.responseMsg = ResponseTypeContants.FAIL;
        }


        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
      
        }
        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
        public string status { get; set; }
        public string apiStatus { get; set; }

        public string responseMsg { get; set; }
        [JsonProperty(PropertyName = "data")]
  
        public ErrorList ErrorList
        {
            get;
            set;
        }

        public object waiverPercentage { get; set; }
        public void SetError(string errorMessage, string errorCode)
        {
            this.status = "ERROR";
            if (this.ErrorList == null)
            {
                this.ErrorList = new ErrorList();
            }
            this.ErrorList.Add(new Error()
            {
                ErrorCode = errorCode,
                ErrorMessage = errorMessage,
                ErrorType = "ERROR"
            });
        }
        public void SetErrorList(ErrorList errorList)
        {
            this.status = "ERROR";
            if (this.ErrorList == null)
            {
                this.ErrorList = new ErrorList();
            }
            this.ErrorList.AddRange(errorList);
        }
        public void SetProcessingError(string errorMessage)
        {
            this.SetError(errorMessage, "PROCESSING_ERROR");
        }
        public void SetRecordNotFoundError(string errorMessage)
        {
            this.SetError(errorMessage, "RECORD_NOT_FOUND");
        }
        public void SetValidationError(string errorMessage)
        {
            this.SetError(errorMessage, "VALIDATION_ERROR");
        }
    }
    public class ResponseTypeContants
    {
        public static string SUCCESS = "SUCCESS";

        public static string ERROR = "ERROR";

        public static string FAIL = "FAIL";

        public static string SUCCESS_WITH_ERROR = "SUCCESS_WITH_ERROR";

        public ResponseTypeContants()
        {
        }
    }

    public class ApiStatusConstants
    {
        public static string COMPLETED;

        public static string NOT_COMPLETED;

        static ApiStatusConstants()
        {
            ApiStatusConstants.COMPLETED = "COMPLETED";
            ApiStatusConstants.NOT_COMPLETED = "NOT_COMPLETED";
        }

        public ApiStatusConstants()
        {
        }
    }

    public class ErrorList : List<Error>
    {
        public ErrorList()
        {
        }
    }

    public class Error
    {
        public string ErrorCode
        {
            get;
            set;
        }

        public string ErrorMessage
        {
            get;
            set;
        }

        public string ErrorType
        {
            get;
            set;
        }

        public Error()
        {
        }
    }
}
