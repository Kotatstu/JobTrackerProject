using Microsoft.EntityFrameworkCore;
using SQLitePCL;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseDeveloperExceptionPage();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//==============================================================================================================================
//==============================================================================================================================
//==============================================================================================================================
//Minimals APIs approuchs:

//POST: ()"Endpoint", async (which database, the json body)
app.MapPost("applications", async (AppDbContext db, JobApplication appData) =>
{
    //Add json body have the type of JobApplication to database
    db.JobApplications.Add(appData);

    //Save changes when differ anything with the database
    await db.SaveChangesAsync();

    return Results.Created($"/applications/{appData.Id}", appData);
});

//GET: Endpoins:applications/status/{status}, inject a defined status into the API
app.MapGet("applications/status/{status}", async (AppDbContext db, ApplicationStatus status) =>
{
    return await db.JobApplications
    .Where(a => a.Status == status)
    .ToListAsync();
});

//PATCH: Endpoint: applications/{id}/status, find the match id and update it status to newStatus
app.MapPatch("applications/{id}/status", async (AppDbContext db, int id, ApplicationStatus newStatus) =>
{
    var apply = await db.JobApplications.FindAsync(id);

    if(apply is null)
        return Results.NotFound();

    apply.Status = newStatus;

    await db.SaveChangesAsync();

    return Results.Ok(apply);

});

//==============================================================================================================================
//==============================================================================================================================
//==============================================================================================================================

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
