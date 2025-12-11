using System.Net;

namespace BookDataScraper;

public class ImageService
{
    private readonly HttpClient _httpClient;
    private readonly string _storagePath;

    public ImageService(string storagePath)
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
        _storagePath = storagePath;
    }

    public async Task<string?> DownloadAndSaveImageAsync(string imageUrl, string isbn)
    {
        if (string.IsNullOrEmpty(imageUrl)) return null;

        try
        {
            var imageBytes = await _httpClient.GetByteArrayAsync(imageUrl);
            
            // Визначаємо розширення файлу (за замовчуванням .jpg, якщо URL дивний)
            string extension = Path.GetExtension(new Uri(imageUrl).AbsolutePath);
            if (string.IsNullOrEmpty(extension)) extension = ".jpg";

            string fileName = $"{isbn}_image{extension}";
            string fullPath = Path.Combine(_storagePath, fileName);

            // Створюємо папку, якщо її немає
            Directory.CreateDirectory(_storagePath);

            await File.WriteAllBytesAsync(fullPath, imageBytes);

            return $"/images/{fileName}";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Image Error] Failed to download {imageUrl}: {ex.Message}");
            return null;
        }
    }
}