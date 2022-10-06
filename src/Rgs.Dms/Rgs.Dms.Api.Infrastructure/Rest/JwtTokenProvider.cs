namespace Rgs.Dms.Api.Infrastructure.Rest
{
    public static class JwtTokenProvider
    {
        public static List<string> Issuers => new List<string>()
        {
            "LkDms"
        };
    }
}
