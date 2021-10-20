using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System;

namespace GameRental.Models
{
  public class ApplicationUser : IdentityUser
  {
    public virtual ICollection<Checkout> JoinEntities { get; set; }
  }
}