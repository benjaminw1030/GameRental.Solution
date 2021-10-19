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
    public bool Rented { get; set; }
    public virtual ICollection<DeveloperGame> JoinEntities { get; set; }
  }
}