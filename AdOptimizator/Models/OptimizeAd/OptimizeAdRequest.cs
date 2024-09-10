using System.ComponentModel.DataAnnotations;

namespace AdOptimizer.Models.OptimizeAd
{
    public class OptimizeAdRequest
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public List<string> Keywords { get; set; }

        [Required]
        public string Platform { get; set; }
    }
}
