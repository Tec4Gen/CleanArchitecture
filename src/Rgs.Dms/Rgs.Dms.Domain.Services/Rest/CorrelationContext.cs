namespace Rgs.Dms.Domain.Services.Rest
{
    public static class CorrelationContext
    {
        private static readonly AsyncLocal<string> _correlationId = new AsyncLocal<string>();

        public static string CorrelationId
        {
            get
            {
                return _correlationId.Value ?? String.Empty;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(nameof(_correlationId), "Correlation id cannot be null or empty");
                }

                if (!string.IsNullOrWhiteSpace(_correlationId.Value))
                {
                    throw new InvalidOperationException("Correlation id is already set");
                }

                _correlationId.Value = value;
            }
        }
    }
}
