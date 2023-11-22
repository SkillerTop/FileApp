using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;

public class FileController : Controller
{
    private readonly IWebHostEnvironment _hostingEnvironment;

    public FileController(IWebHostEnvironment hostingEnvironment)
    {
        _hostingEnvironment = hostingEnvironment;
    }

    public ActionResult DownloadFile()
    {
        return View();
    }

    [HttpPost]
    public ActionResult DownloadFile(string firstName, string lastName, string fileName)
    {
        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(fileName))
        {
            ModelState.AddModelError("", "Будь ласка, заповніть всі поля форми.");
            return View();
        }

        string fileContent = $"Ім'я: {firstName}\r\nПрізвище: {lastName}";

    
        string folderPath = Path.Combine(_hostingEnvironment.ContentRootPath, "Files");
        Directory.CreateDirectory(folderPath);

        string filePath = Path.Combine(folderPath, fileName + ".txt");

        System.IO.File.WriteAllText(filePath, fileContent, Encoding.UTF8);

        return File(filePath, "text/plain", fileName + ".txt");
    }
}
