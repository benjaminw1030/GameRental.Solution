using System.Collections.Generic;
using System;

namespace GameRental.Models
{
  public class Game
  {
    public Game()
    {
      this.JoinEntities = new HashSet<DeveloperGame>(); 
    }

    public int GameId { get; set; }
    public string Title { get; set; }
    public int Copies { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<DeveloperGame> JoinEntities { get; set; }
  }
}