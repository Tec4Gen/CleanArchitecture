using Microsoft.AspNetCore.Mvc;
using Rgs.Dms.Api.Infrastructure;
using Rgs.Dms.Api.Infrastructure.Rest;
using Rgs.Dms.Integration.EmailSender;
using Rgs.Dms.Integration.MailTools;

namespace Rgs.Dms.Api.Controllers;

[ApiVersion(RestApiHelper.ApiCurrentVersion)]
[Route(DmsRestApiRoutes.DmsRoutePrefix)]
public class TestIntegrationController : ControllerBase
{
    public IWebHostEnvironment HostingEnv { get; }

    public TestIntegrationController(Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnv)
    {
        HostingEnv = hostingEnv;
    }
    [HttpGet]
    [Route("mailkit")]
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<IActionResult> MailKit()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        TestConnectionMailKit.connection();
        
        return Ok();
    }

    [HttpGet]
    [Route("mailsender")]
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<IActionResult> MailSender()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        var html = EmailSender.TestSendBody();

        return Ok(html);
    }

    [HttpGet]
    [Route("env")]
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<IActionResult> Env()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        return Ok(HostingEnv.EnvironmentName);
    }
}

