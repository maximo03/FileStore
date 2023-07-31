using FileStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FileStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _env;
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public ActionResult Index()
        {
            List<UploadModel> items = GetFiles();
            return View(items);
        }

        public async Task<IActionResult> upload(UploadModel upload)
        {
            var fileName = System.IO.Path.Combine(_env.WebRootPath,"FileStore", upload.MyFile.FileName);
            await upload.MyFile.CopyToAsync(new System.IO.FileStream(fileName, System.IO.FileMode.Create));
            
            //var items = GetFiles();

            return RedirectToAction("Index");
        }


        private List<UploadModel> GetFiles()
        {
            var dir = new System.IO.DirectoryInfo(_env.WebRootPath + "/FileStore");
            System.IO.FileInfo[] fileNames = dir.GetFiles("*.*");
            List<UploadModel> items = new List<UploadModel>();
            foreach (var file in fileNames)
            {
                items.Add(new UploadModel { MyFile = null, path = file.Name });
            }
            return items;
        }

        public FileResult Download(string fileName)
        {
            var FileVirtualPath = "~/FileStore/" + fileName;
            return File(FileVirtualPath,"application/force-download",Path.GetFileName(FileVirtualPath));
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}