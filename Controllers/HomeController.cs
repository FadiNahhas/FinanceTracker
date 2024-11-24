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
        
        var transactions = user.Budget == null ? null : _context.Transactions.Where(t => t.UserId == user.Id).ToList();
        
        ViewBag.IsBudgetSet = user.Budget != null;
        ViewBag.Budget = user.Budget;
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

        if (user != null)
        {
            user.Budget = budget;
            await _userManager.UpdateAsync(user);

            return Json(new { success = true });
        }

        return Json(new { success = false });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}