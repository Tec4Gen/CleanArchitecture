using RazorEngineCore;
using Rgs.Dms.Api.ViewModel;

namespace Rgs.Dms.Integration.EmailSender
{
    public static class EmailSender
    {
        public static string TestSendBody()
        {  
            var emailModel = new TestModelEmail
            {
               TestField = "TestMessage"
            };

            var subject = $"EmptyTheme";

            IRazorEngine razorEngine = new RazorEngine();
            IRazorEngineCompiledTemplate template = razorEngine.Compile(File.ReadAllText(@"..\Rgs.Dms.Api\Razor\TemplateEmail.cshtml"));

            return template.Run(emailModel);
        }
    }
}