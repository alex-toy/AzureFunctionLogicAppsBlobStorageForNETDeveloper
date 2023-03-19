using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using AzureFunctionTangyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace AzureFunctionTangyWeb.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    static readonly HttpClient client = new HttpClient();
    private readonly BlobServiceClient _blobServiceClient;
    public HomeController(ILogger<HomeController> logger, BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(SalesRequest salesRequest, IFormFile file)
    {
        salesRequest.Id = Guid.NewGuid().ToString();

        using (var content = new StringContent(JsonConvert.SerializeObject(salesRequest),
            System.Text.Encoding.UTF8, "application/json"))
        {
            string url = "http://localhost:7071/api/OnSalesUploadWriteToQueue";
            HttpResponseMessage response = await client.PostAsync(url, content);
            string returnValue = response.Content.ReadAsStringAsync().Result;
        }

        if (file == null) return RedirectToAction(nameof(Index));

        await UploadFileToBlob(salesRequest, file);

        return View();
    }

    private async Task UploadFileToBlob(SalesRequest salesRequest, IFormFile file)
    {
        var fileName = salesRequest.Id + Path.GetExtension(file.FileName);
        BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient("functionsalesrep");
        var blobClient = blobContainerClient.GetBlobClient(fileName);

        var httpHeaders = new BlobHttpHeaders
        {
            ContentType = file.ContentType
        };

        await blobClient.UploadAsync(file.OpenReadStream(), httpHeaders);
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
}