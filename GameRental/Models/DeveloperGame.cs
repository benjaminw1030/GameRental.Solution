namespace GameRental.Models
{
  public class DeveloperGame
  {
    public int DeveloperGameId { get; set; }
    public int GameId { get; set; }
    public int DeveloperId { get; set; }
    public virtual Game Game { get; set; }
    public virtual Developer Developer { get; set; }
  }
}