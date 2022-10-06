namespace Rgs.Dms.Api.Infrastructure.Responses;

public class ResponseWithData<T> : BaseResponse where T : class
{
    public T Data { get; set; } = default!;
}