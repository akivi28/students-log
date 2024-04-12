using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

public class ImageService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ImageService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> SaveImageAsync(IFormFile image, int studentId)
    {
        string folderPath = Path.Combine("wwwroot", "images", studentId.ToString());
        string fileName = $"photo_{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
        string filePath = Path.Combine(folderPath, fileName);

        Directory.CreateDirectory(folderPath);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await image.CopyToAsync(fileStream);
        }

        return $"/images/{studentId}/{fileName}";
    }
}
