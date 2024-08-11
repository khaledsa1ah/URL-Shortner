using Microsoft.EntityFrameworkCore;
using URL_Shortner.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<URLDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("shorturl", async (URLDTO url, URLDBContext db, HttpContext ctx) =>
{
    var existingShortURL = await db.ShortURLs
        .FirstOrDefaultAsync(s => s.OriginalURL == url.OriginalURL);

    if (existingShortURL != null)
    {
        var fullShortenedUrl = $"{ctx.Request.Scheme}://{ctx.Request.Host}/{existingShortURL.ShortenedURL}";
        return Results.Ok(new
        {
            Message = "This URL has already been shortened.",
            ShortenedURL = fullShortenedUrl
        });
    }

    var shortURL = new ShortURL
    {
        OriginalURL = url.OriginalURL,
        ShortenedURL = Guid.NewGuid().ToString()[..6]
    };

    db.ShortURLs.Add(shortURL);
    await db.SaveChangesAsync();

    var fullNewShortenedUrl = $"{ctx.Request.Scheme}://{ctx.Request.Host}/{shortURL.ShortenedURL}";
    return Results.Created(fullNewShortenedUrl, 
        new ShortURLDTO { ShortenedURL = fullNewShortenedUrl });
});


app.MapFallback(async (URLDBContext dbContext, HttpContext ctx) =>
{
    var path = ctx.Request.Path.Value?.Trim('/');
    var urlMatch = await dbContext.ShortURLs.FirstOrDefaultAsync(s => s.ShortenedURL == path);
    
    return urlMatch == null ? Results.NotFound() : Results.Redirect(urlMatch.OriginalURL);
});

app.Run();