using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace CodeFirst.Options
{
    /// <summary>
    /// All options for the application.
    /// </summary>
    public class ApplicationOptions
    {
        public CacheProfileOptions CacheProfiles { get; set; }

        public CompressionOptions Compression { get; set; }

        public ForwardedHeadersOptions ForwardedHeaders { get; set; }

        public KestrelServerOptions Kestrel { get; set; }
    }
}
