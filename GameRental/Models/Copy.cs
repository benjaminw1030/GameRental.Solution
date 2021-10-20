using System.Collections.Generic;

namespace GameRental.Models
{
  public class Copy
  {
    public int CopyId { get; set; }
    public int GameId { get; set; }
    public bool Rented { get; set; }
    public virtual Game Game { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<Checkout> JoinEntities { get; set; }
  }
}