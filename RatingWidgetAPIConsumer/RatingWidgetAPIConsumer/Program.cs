using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Net;
using System.Reflection;
using System.Web;
using System.IO;
using Newtonsoft.Json;


namespace RatingWidgetAPIConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            //Get count of ratings
            var numRatings = Helper.CallRWAPI("/v1/sites/" + Helper.RWId + "/ratings/count.json", "GET");

            //GET Snippets infor for SERP
            var snippetsJSON = Helper.CallRWAPI("/v1/sites/" + Helper.RWId + "/ratings/rich-snippets.json", "GET");
            RatingSnippets snippets = JsonConvert.DeserializeObject<RatingSnippets>(snippetsJSON);

            //GET Rating info by external ID from snippets
            foreach (var snippet in snippets.ratings)
            {
                var ratingJSON = Helper.CallRWAPI("/v1/sites/" + Helper.RWId + "/ratings/" + snippet.external_id + ".json", "GET", "?is_external=true");

                Rating ratingInfo = JsonConvert.DeserializeObject<Rating>(ratingJSON);

                Console.WriteLine("Retrieving " + ratingInfo.title);
            }

            //GET ALL the ratings and delete if necessary
            for (int i = 0; i < 30; i++)
            {
                var response = Helper.CallRWAPI("/v1/sites/" + Helper.RWId + "/ratings.json", "GET", "?count=50&offset=" + i * 10);

                Ratings listRatings = JsonConvert.DeserializeObject<Ratings>(response);

                foreach (var rating in listRatings.ratings)
                {
                    //DELETE RATINGS
                    //var responseDelete = Helper.CallRWAPI("/v1/sites/" + Helper.RWId + "/ratings/" + rating.id + ".json", "DELETE");

                }
            }

        }



    }
}
