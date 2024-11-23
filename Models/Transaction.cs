namespace PersonalFinanceTracker.Models;

public class Transaction
{
    public int Id { get; set; }
    
    public float Amount { get; set; }
    
    public DateTime Date { get; set; }
    
    public string Description { get; set; }
    
    public string UserId { get; set; }
    
    public virtual ApplicationUser User { get; set; }
}