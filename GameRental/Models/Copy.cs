using System.Collections.Generic;
using System;

namespace GameRental.Models
{
  public class Copy
  {
    public Copy()
    {
      this.RentalDate = DateTime.Now;
    }
    public int CopyId { get; set; }
    public int GameId { get; set; }
    public bool Rented { get; set; }
    public DateTime RentalDate { get; set; }
    public virtual Game Game { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<Checkout> JoinEntities { get; set; }
  }
}