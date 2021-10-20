namespace GameRental.Models
{
  public class Checkout
  {
    public int CheckoutId { get; set; }
    public string UserId { get; set; }
    public int CopyId { get; set; }
    public virtual Copy Copy { get; set; }
    public virtual ApplicationUser User { get; set; }
  }
}