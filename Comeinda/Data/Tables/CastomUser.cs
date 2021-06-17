using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Comeinda.Data.Tables
{
    public class CastomUser : IdentityUser
    {
        public IEnumerable<EventTable> Events { get; set; }
    }
}
