namespace Rgs.Dms.Api.Infrastructure.Responses;

public abstract class BaseResponse
{
    public TechData TechData { get; set; } = default!;
}
