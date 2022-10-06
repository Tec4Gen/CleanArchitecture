namespace Rgs.Dms.Api.Infrastructure.Responses;

public class ValidationErrorResponse : BaseResponse
{
    public string ErrorMessage { get; set; } = default!;
}
