﻿using OmniRiskAPI.Models;

namespace OmniRiskAPI.Services
{
    public class TwitterService
    {
        public Task<List<TweetModel>> GetTweetModels(string lat, string lng, double radius)
        {
            return Task.FromResult(new List<TweetModel>
            {
                new TweetModel
                {
                    Content = "Wypadek na ulicy grunwaldzkiej",
                    Url = "www.yourmom.zip"
                },
                new TweetModel
                {
                    Content = "Jacek Sutryk nie wygra kolejnych wyborow",
                    Url = "www.yourmom.zip"
                },
                new TweetModel
                {
                    Content = "Z psychiatryka uciekl grozny pacjent",
                    Url = "www.yourmom.zip"
                },
                new TweetModel
                {
                    Content = "Niemcy zamierzaja wprowadzic kolejne sankcje przeciwko Rosji",
                    Url = "www.yourmom.zip"
                }
            });
        }
    }
}
