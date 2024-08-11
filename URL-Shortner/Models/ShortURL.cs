namespace URL_Shortner.Models;

public class ShortURL
{
    public int Id { get; set; }
    public string OriginalURL { get; set; }
    public string ShortenedURL { get; set; }
    
}