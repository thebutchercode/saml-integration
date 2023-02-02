using ITfoxtec.Identity.Saml2;
using ITfoxtec.Identity.Saml2.Schemas.Metadata;
using ITfoxtec.Identity.Saml2.MvcCore.Configuration;

namespace SamlIntegration.Auth;

public static class SamlExtensions
{
    public static void ConfigureSaml2(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRazorPages();

        services.Configure<Saml2Configuration>(configuration.GetSection("Saml2"));

        services.Configure<Saml2Configuration>(saml2Configuration =>
        {
            var entityDescriptor = new EntityDescriptor();
            entityDescriptor.ReadIdPSsoDescriptorFromFile("metadata.xml");
            //entityDescriptor.ReadIdPSsoDescriptorFromUrl(new Uri(configuration["Saml2:IdPMetadata"]));
            if (entityDescriptor.IdPSsoDescriptor != null)
            {
                saml2Configuration.SingleSignOnDestination = entityDescriptor.IdPSsoDescriptor.SingleSignOnServices.First().Location;
                saml2Configuration.SignatureValidationCertificates.AddRange(entityDescriptor.IdPSsoDescriptor.SigningCertificates);
            }
            else
            {
                throw new Exception("IdPSsoDescriptor not loaded from metadata.");
            }
        });

        services.AddSaml2();  
    }
}