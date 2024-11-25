using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceTracker.Data;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Controllers;

public class HomeController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;

    public HomeController(UserManager<ApplicationUser> user_manager, ApplicationDbContext context)
    {
        _userManager = user_manager;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return Redirect("/Identity/Account/Login");
        }
        
        var transactions = _context.Transactions
            .Where(t => t.UserId == user.Id)
            .ToList();

        var transactionsTotal = transactions.Sum(t => t.Amount);
        var budget = user.Budget + (float)transactionsTotal;
        
        ViewBag.IsBudgetSet = user.Budget != null;
        ViewBag.Budget = budget;
        ViewBag.Transactions = transactions;

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    
    [HttpGet]
    public IActionResult SetBudget()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> SetBudget(float budget)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return Json(new { success = false, message = "User not logged in." });
        }
        
        user.Budget = budget;
        var result = await _userManager.UpdateAsync(user);
        
        if (result.Succeeded)
        {
            return Json(new { success = true });
        }
        
        return Json(new { success = false, message = "Failed to update budget." });
    }

    [HttpPost]
    public async Task<IActionResult> AddTransaction(decimal amount, string description, DateTime date)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return Json(new { success = false, message = "User not logged in." });
        }
        
        var transaction = new Transaction
        {
            Amount = amount,
            Description = description,
            Date = date,
            UserId = user.Id
        };
        
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        
        return Json(new { success = true });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}