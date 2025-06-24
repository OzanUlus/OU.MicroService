using Refit;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

namespace OU.Microservice.Shared
{
    public class ServiceResult
    {
        [JsonIgnore]
        public HttpStatusCode Status { get; set; }
        public ProblemDetails? Fail { get; set; }

        [JsonIgnore]
        public bool IsSuccess => Fail is null;
        [JsonIgnore]
        public bool IsFail => !IsSuccess;

        //Static factory methods
        public static ServiceResult SuccessAsNoContent() 
        {
           return new ServiceResult { Status = HttpStatusCode.NoContent };
        }

        public static ServiceResult ErrorAsNotFound() 
        {
           return new ServiceResult 
           {
               Status = HttpStatusCode.NotFound,
               Fail = new ProblemDetails
               {
                   Title = "NotFound",
                   Detail = "The requested resource was not found"
               }
           };
        }

        public static ServiceResult Error(ProblemDetails problemDetails, HttpStatusCode httpStatus)
        {
            return new ServiceResult
            {
                Status = httpStatus,
                Fail = problemDetails
            };
        }

        public static ServiceResult Error(string title, string description, HttpStatusCode httpStatus)
        {
            return new ServiceResult
            {
                Status = httpStatus,
                Fail = new ProblemDetails()
                {
                    Title = title,
                    Detail = description,
                    Status = httpStatus.GetHashCode()

                }

            };
        }

        public static ServiceResult Error(string title, HttpStatusCode httpStatus)
        {
            return new ServiceResult
            {
                Status = httpStatus,
                Fail = new ProblemDetails()
                {
                    Title = title,
                    Status = httpStatus.GetHashCode()

                }

            };
        }

        public static ServiceResult ErrorFromProblemDetails(ApiException apiException)
        {
            if (string.IsNullOrEmpty(apiException.Content))
            {
                return new ServiceResult()
                {
                    Fail = new Microsoft.AspNetCore.Mvc.ProblemDetails()
                    {
                        Title = apiException.Message
                    },
                    Status = apiException.StatusCode
                };
            }

            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(apiException.Content, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });

            return new ServiceResult()
            {
                Fail = problemDetails,
                Status = apiException.StatusCode
            };
        }

        public static ServiceResult ErrorFromValidation(IDictionary<string, object?> errors)
        {
            return new ServiceResult
            {
                Status = HttpStatusCode.BadRequest,
                Fail = new ProblemDetails()
                {
                    Title = "Validation errors occured.",
                    Detail = "Please check the errors property for more details.",
                    Extensions = errors,
                    Status = HttpStatusCode.BadRequest.GetHashCode()

                }

            };
        }

    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; }
       [JsonIgnore] public string? UrlAsCreated { get; set; }

        public static ServiceResult<T> SuccessAsOk(T data)
        {
            return new ServiceResult<T> 
            { 
                Status = HttpStatusCode.OK,
                Data= data
            };
        }

        public static ServiceResult<T> SuccessAsCreated(T data, string url)
        {
            return new ServiceResult<T>
            {
                Status = HttpStatusCode.Created,
                Data = data,
                UrlAsCreated = url
            };
        }

        

        public new static ServiceResult<T> Error(ProblemDetails problemDetails, HttpStatusCode httpStatus)
        {
                return new ServiceResult<T>
                {
                    Status = httpStatus,
                    Fail = problemDetails
                };
        }

        public new static ServiceResult<T> Error(string title, string description, HttpStatusCode httpStatus)
        {
            return new ServiceResult<T>
            {
                Status = httpStatus,
                Fail = new ProblemDetails()
                {
                    Title = title,
                    Detail = description,
                    Status = httpStatus.GetHashCode()

                }

            };
        }

        public new static ServiceResult<T> Error(string title, HttpStatusCode httpStatus)
        {
            return new ServiceResult<T>
            {
                Status = httpStatus,
                Fail = new ProblemDetails()
                {
                    Title = title,
                    Status = httpStatus.GetHashCode()

                }

            };
        }

        public new static ServiceResult<T> ErrorFromProblemDetails(ApiException apiException)
        {
            if (string.IsNullOrEmpty(apiException.Content))
            {
                return new ServiceResult<T>()
                {
                    Fail = new Microsoft.AspNetCore.Mvc.ProblemDetails()
                    {
                        Title = apiException.Message
                    },
                    Status = apiException.StatusCode
                };
            }

            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(apiException.Content, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });

            return new ServiceResult<T>()
            {
                Fail = problemDetails,
                Status = apiException.StatusCode
            };
        }

        public new static ServiceResult<T> ErrorFromValidation(IDictionary<string,object?> errors)
        {
            return new ServiceResult<T>
            {
                Status = HttpStatusCode.BadRequest,
                Fail = new ProblemDetails()
                {
                    Title = "Validation errors occured.",
                    Detail = "Please check the errors property for more details.",
                    Extensions = errors,
                    Status = HttpStatusCode.BadRequest.GetHashCode()

                }

            };
        }
    }
}
