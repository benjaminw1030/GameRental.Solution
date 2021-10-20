using System.Collections.Generic;

namespace GameRental.Models
{
  public class Game
  {
    public Game()
    {
      this.JoinEntities = new HashSet<DeveloperGame>();
      this.Copies = new HashSet<Copy>();
    }

    public int GameId { get; set; }
    public string Title { get; set; }
    public virtual ICollection<DeveloperGame> JoinEntities { get; set; }
    public virtual ICollection<Copy> Copies { get; set; }
  }
}