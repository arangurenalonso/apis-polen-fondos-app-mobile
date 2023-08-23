namespace Infrastructure.Options.ApiExternas
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    public class ApiExternaOptionsSetup : IConfigureOptions<ApisExternasOption>
    {
        private const string SectionName = "ApisExternas";
        private readonly IConfiguration _configuration;

        public ApiExternaOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(ApisExternasOption options)
        {
            _configuration.GetSection(SectionName).Bind(options);
        }
    }
}
