using System;
using System.ComponentModel.DataAnnotations;

namespace DemoWIUTGallery.Models
{
    public class Gallery
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string PathFile { get; set; }

        [Required]
        public DateTime AddedDate { get; set; }
    }
}
