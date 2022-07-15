using Microsoft.AspNetCore.Http;

namespace DemoWIUTGallery.ViewModels
{
    public class UploadImageViewModel
    {
        public IFormFile Picture { get; set; }
    }
}
