using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Comeinda.Data.Tables
{
    [Display(Name = "Пользователи")]
    public class CastomUser : IdentityUser
    {
        public IEnumerable<EventTable> Events { get; set; }
    }
}
