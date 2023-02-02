using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SamlIntegration.Views.Home;

[Authorize]
public class ClaimsModel : PageModel
{
    private readonly ILogger<ClaimsModel> _logger;

    public ClaimsModel(ILogger<ClaimsModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}