using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoWIUTGallery.ViewModels
{
    public class GalleryImageViewModel : EditImageViewModel
    {
        public string Name { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
