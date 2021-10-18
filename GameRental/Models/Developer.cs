using System.Collections.Generic;

namespace GameRental.Models
{
  public class Developer
  {
    public Developer()
    {
      this.JoinEntities = new HashSet<DeveloperGame>();
    }

    public int DeveloperId { get; set; }
    public string Name { get; set; }
    public virtual ICollection<DeveloperGame> JoinEntities { get; set; }
  }
}