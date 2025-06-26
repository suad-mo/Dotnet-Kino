using Microsoft.AspNetCore.Identity;

namespace Kino.Core.Entities
{
    public class UserAccount : IdentityUser
    {
        public string? Ime { get; set; }

        public string? Prezime { get; set; }

        public bool IsVIP { get; set; }
    }
}
