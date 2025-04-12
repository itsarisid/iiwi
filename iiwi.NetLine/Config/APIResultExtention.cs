using DotNetCore.Results;
using Microsoft.AspNetCore.Mvc;

namespace iiwi.NetLine.Config;

public static class APIResultExtention
{
    public static HttpResponse ApiResult<T>(this Result<T> result)
    {
        //return new ObjectResult(result.HasMessage ? result.Message : ((object)result.Value))
        //{
        //    StatusCode = (int)result.Status
        //};

        return HttpResponse
        {
            StatusCode = (int)result.Status,
            ContentType = "application/json",
            Body = result.HasMessage ? result.Message : ((object)result.Value)
        };
    }
}
