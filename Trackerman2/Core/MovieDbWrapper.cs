using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using Trackerman2.DomainModels;
using System.Net;
using System.Threading;

namespace Trackerman2.Core
{
    public class MovieDbWrapper
    {
        private string apiKey = "3e1002c7f6cd15096a5d73cf92e04566";
        private IRestRequest restRequest;
        private IRestClient client;
        private ApplicationDbContext db;

        public MovieDbWrapper(ApplicationDbContext db, IRestClient client)
        {
            this.client = client;
            this.db = db;
        }

        public Show FindShow(int id)
        {
            restRequest = new RestRequest();
            restRequest.AddQueryParameter("api_key", apiKey);
            restRequest.Resource = "/tv/{id}";
            restRequest.AddUrlSegment("id", id.ToString());

            IRestResponse restResponse = ThrottleRequest(restRequest);
            // If not any success codes return null, not an exception as API is expected to have empty results.
            if (!((int)restResponse.StatusCode >= 200 && (int)restResponse.StatusCode <= 299))
                return null;

            var json = JObject.Parse(restResponse.Content.ToString());
            Show response = ConvertJsonTo<Show>(json);

            return response;
        }

        private IRestResponse ThrottleRequest(IRestRequest request)
        {
            IRestResponse restResponse = client.Execute(request);
            while ((int)restResponse.StatusCode == 429)
            {
                Thread.Sleep(int.Parse(restResponse.Headers.FirstOrDefault(x => x.Name == "Retry-After").Value.ToString()) * 1000);
                restResponse = client.Execute(request);
            }

            return restResponse;
        }

        public Season FindSeasonWithEpisodes(int tvID, int seasonNumber)
        {
            restRequest = new RestRequest();
            restRequest.AddQueryParameter("api_key", apiKey);
            restRequest.Resource = "/tv/{id}/season/{seasonNumber}";
            restRequest.AddUrlSegment("id", tvID.ToString());
            restRequest.AddUrlSegment("seasonNumber", seasonNumber.ToString());

            IRestResponse restResponse = ThrottleRequest(restRequest);
            if (!((int)restResponse.StatusCode >= 200 && (int)restResponse.StatusCode <= 299))
                return null;

            var json = JObject.Parse(restResponse.Content.ToString());
            Season response = ConvertJsonTo<Season>(json);

            return response;
        }

        private T ConvertJsonTo<T>(JObject json)
        {   
            // Convert snake_case_variables to CamelCaseVariables.
            return JsonConvert.DeserializeObject<T>(json.ToString(), new JsonSerializerSettings { ContractResolver = new SnakeCasePropertyNamesContractResolver() });
        }

        private int GetLatestInsertedTvID()
        {
            restRequest = new RestRequest();
            restRequest.AddQueryParameter("api_key", apiKey);
            restRequest.Resource = "/tv/latest";

            IRestResponse restResponse = client.Execute(restRequest);
            var json = JObject.Parse(restResponse.Content.ToString());

            return json["id"].Value<int>();
        }

        private IEnumerable<int> GetChangedTvIDs()
        {
            restRequest = new RestRequest();
            restRequest.AddQueryParameter("api_key", apiKey);
            restRequest.Resource = "/tv/changes";

            IRestResponse restResponse = client.Execute(restRequest);
            var json = JObject.Parse(restResponse.Content.ToString());

            return new List<int>();
        }

        public void DailyUpdateTvShows()
        {

        }

        public void PopulateNewTvShows()
        {
            int latestLocalTvShowID = db.Shows.OrderByDescending(x => x.ID).Select(x => x.ID).FirstOrDefault();
            int latestTvShowID = GetLatestInsertedTvID();

            for (int i = latestLocalTvShowID + 1; i <= latestTvShowID; i++)
            {
                var show = FindShow(i);
                if (show == null)
                    continue;

                db.Shows.Add(show);
                foreach (var season in show.Seasons)
                {
                    if (!season.SeasonNumber.HasValue)
                        continue;

                    var responseSeason = FindSeasonWithEpisodes(show.ID, (int)season.SeasonNumber);
                    if (responseSeason == null)
                        continue;

                    season.Episodes = responseSeason.Episodes;
                    season.Name = responseSeason.Name;
                    season.Overview = responseSeason.Overview;
                }

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to save: ", ex.InnerException);
                }
                finally
                {
                    db = new ApplicationDbContext();
                }
            }
        }
    }
}