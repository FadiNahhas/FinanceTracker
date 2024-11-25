using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalFinanceTracker.Models;

public class Transaction
{
    public int Id { get; set; }
    
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
    public decimal Amount { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    [StringLength(255, ErrorMessage = "Description must be less than 255 characters.")]
    public string Description { get; set; }
    
    [Required]
    public string UserId { get; set; }
    
    [ForeignKey("UserId")]
    public virtual ApplicationUser User { get; set; }
}