using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;

namespace AnimeStockWebProject.Extensions
{
    public static class ClaimsExtension
    {
        public static Guid GetId(this ClaimsPrincipal user)
        {
            return Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        public static string GetProfilePicturePath(this ClaimsPrincipal user)
        {
            Claim claim = user.Claims.FirstOrDefault(u => u.Type == "ProfilePicturePath");
            return claim?.Value ?? string.Empty;
        }
    }
}
