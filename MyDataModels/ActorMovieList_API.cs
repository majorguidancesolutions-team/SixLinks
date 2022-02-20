using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataModels
{
    public class ActorMovieList_API
    {
        public string id { get; set; }
        public string name { get; set; }
        public string role { get; set; }
        public string image { get; set; }
        public string summary { get; set; }
        public string birthDate { get; set; }
        public object deathDate { get; set; }
        public string awards { get; set; }
        public string height { get; set; }
        public List<KnownFor> knownFor { get; set; }
        public List<CastMovy> castMovies { get; set; }
        public string errorMessage { get; set; }
    }

    public class CastMovy
    {
        public string id { get; set; }
        public string role { get; set; }
        public string title { get; set; }
        public string year { get; set; }
        public string description { get; set; }
    }

    public class KnownFor
    {
        public string id { get; set; }
        public string title { get; set; }
        public string fullTitle { get; set; }
        public string year { get; set; }
        public string role { get; set; }
        public string image { get; set; }
    }
}