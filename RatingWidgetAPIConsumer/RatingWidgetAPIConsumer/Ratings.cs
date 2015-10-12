using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingWidgetAPIConsumer
{
    public class RatingSnippet
    {
        public string external_id { get; set; }
        public int approved_count { get; set; }
        public double avg_rate { get; set; }
    }

    public class RatingSnippets
    {
        public List<RatingSnippet> ratings { get; set; }
    }

    public class Rating
    {
        public string site_id { get; set; }
        public string external_id { get; set; }
        public int rate { get; set; }
        public int approved_count { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public string @class { get; set; }
        public string url { get; set; }
        public object img { get; set; }
        public int pending_count { get; set; }
        public int spam_count { get; set; }
        public int trash_count { get; set; }
        public string updated { get; set; }
        public string id { get; set; }
        public string created { get; set; }
        public double avg_rate { get; set; }
    }

    public class Ratings
    {
        public List<Rating> ratings { get; set; }
    }
}
