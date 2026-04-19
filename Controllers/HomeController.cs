using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using JobTrackerProject.Models;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.ActionConstraints;


namespace JobTrackerProject.Controllers;

public class HomeController : Controller
{
    private readonly  AppDbContext _context;

    //Constructor
    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var list = _context.JobApplications.ToList();
        return View(list);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    //GET: Show the view of create page
    public IActionResult Create()
    {
        return View();
    }

    //POST:
    [HttpPost]
    public async Task<IActionResult> Create(JobApplication job)
    {
        if(ModelState.IsValid)
        {
            await _context.JobApplications.AddAsync(job);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        return View(job);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var job = await _context.JobApplications.FindAsync(id);

        return View(job);
    }

    [HttpPost]
    public IActionResult Edit(JobApplication job)
    {
        _context.JobApplications.Update(job);
        _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var job = await _context.JobApplications.FindAsync(id);
        if(job == null)
            return NotFound();

        _context.JobApplications.Remove(job);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}
