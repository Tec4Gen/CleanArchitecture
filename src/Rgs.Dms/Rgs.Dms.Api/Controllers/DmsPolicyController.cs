using Microsoft.AspNetCore.Mvc;
using Rgs.Dms.Api.Infrastructure;
using Rgs.Dms.Api.Infrastructure.BackgroundTask;
using Rgs.Dms.Api.Infrastructure.Responses;
using Rgs.Dms.Api.Infrastructure.Rest;
using Rgs.Dms.Domain.ExcelGenerator;
using Rgs.Dms.Domain.PassbookGenerator;
using Rgs.Dms.Domain.Policy;

namespace Rgs.Dms.Api.Controllers;

[ApiVersion(RestApiHelper.ApiCurrentVersion)]
[Route(DmsRestApiRoutes.DmsRoutePrefix)]
public class DmsPolicyController : ControllerBase
{
    private readonly IBackgroundTaskQueue _backgroundTaskQueue;

    public DmsPolicyController(IBackgroundTaskQueue backgroundTaskQueue)
    {
        _backgroundTaskQueue = backgroundTaskQueue;
    }
    /// <summary>
    /// Возвращает информацию о полисе ДМС, включая данные застрахованного, программы страхования, кураторов полиса.
    /// </summary>
    /// GET: /api/rest/v{version:apiVersion}/lk/profiles/my/dms/policies/{policyID}
    [HttpGet]
    [Route("policies/{policyID}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseWithData<Policy>))]
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<ActionResult<BaseResponse>> GetDmsPolicy(int policyID)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        if (policyID == 0)
        {
            return NotFound(new ValidationErrorResponse
            {
                ErrorMessage = "Ашипка",
                
            });
        }

        return new ResponseWithData<Policy>
        {
            TechData = new TechData(),
            Data = new Policy()
        };
    }

    /// <summary>
    /// Возвращает csv файл (список застрахованных) для выгрузки.
    /// </summary>
    /// GET: /api/rest/v{version:apiVersion}/lk/profiles/my/dms/contracts/{contractID}/policies/download/insured
    [HttpGet]
    [Route("contracts/{dmsContractID}/policies/download/insured")]
    //[SiteAuthorize(UserRole.DmsAdmin, UserRole.DmsHr, UserRole.DmsSupervisor)]
    public async Task<IActionResult> GetInsuredListCsv()
    {

        var wb = await ExelGen.BuildExcelFile(1);

        using (MemoryStream stream = new MemoryStream())
        {
            wb.SaveAs(stream);
            stream.Seek(0, SeekOrigin.Begin);

            return File(
                fileContents: stream.ToArray(),
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",

                fileDownloadName: "ERSheet.xlsx");
        }
    
    }

    /// <summary>
    /// Возвращает passbook файл для полиса ДМС.
    /// Скачивать passbook застрахованный может из своего ЛК, или по ссылке из QR кода.
    /// </summary>
    /// GET: /api/rest/v{version:apiVersion}/lk/profiles/my/dms/policies/{policyID}/passbook
    [HttpGet]
    [Route("policies/{policyID}/passbook")]
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<IActionResult> DownloadDmsPolicyPassbook()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        var a = PassGen.PasGen();

        return File(
                fileContents: a,
                contentType: "application/vnd.apple.pkpass",

                fileDownloadName: "RgsDmsPolicy.pkpass");
    }


    [HttpGet]
    [Route("firEndForget")]
    public async Task<IActionResult> Logger()
    {
        _backgroundTaskQueue.EnqueueTask(async (cancellationToken) =>
        {
            try
            {
                // Do something expensive
                await Task.Delay(20000);

                using (var stream = new StreamWriter("Result.txt"))
                {
                    stream.WriteLine("Записалось");
                }

                await Task.Delay(20000);

                using (var stream = new StreamWriter("Result1.txt"))
                {
                    stream.WriteLine("Не Записалось");
                }
            }
            catch (Exception ex)
            {

            }
        });

        return Ok("Типа залогировали");
    }
}

