namespace AdOptimizator.Models.OptimizeAd
{
    public class OptimizeAdRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public List<string> Keywords { get; set; }

        public string Platform { get; set; }
    }
}
