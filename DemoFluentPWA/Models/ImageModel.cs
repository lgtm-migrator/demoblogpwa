using System.ComponentModel.DataAnnotations;

namespace DemoFluentPWA.Models
{
    public class ImageModel
    {
        [Display(Name = "URL della foto")]
        public string Url { get; set; }

        public string Preview { get; set; }

        [Display(Name ="Testo alternativo")]
        public string AltText { get; set; }
    }
}
