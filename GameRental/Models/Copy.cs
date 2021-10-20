namespace GameRental.Models
{
  public class Copy
  {
    public int CopyId { get; set; }
    public int GameId { get; set; }
    public bool Rented { get; set; }
    public virtual Game Game { get; set; }
  }
}