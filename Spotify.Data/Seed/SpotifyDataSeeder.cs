﻿using GraphQLInDotNet.Data;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;
using System.IO;
using System.Threading;

namespace Spotify.Data.Seed
{
    public class SpotifyDataSeeder : ISeeder
    {
        private readonly AuthorizationCodeAuth authorizationCodeAuth;
        private SpotifyWebAPI api;
        private readonly ManualResetEventSlim authReceivedEvent = new ManualResetEventSlim(false);

        public SpotifyDataSeeder()
        {
            authorizationCodeAuth = new AuthorizationCodeAuth(
                "759318169b66436ba800082fc88e0efb",
                "92349ad8387845caa08b0ccef265e77c",
                "https://developer.spotify.com/dashboard/applications/759318169b66436ba800082fc88e0efb",
                "https://developer.spotify.com/dashboard/applications/759318169b66436ba800082fc88e0efb"); 
        }

        public void SeedData(IDataContext dataContext)
        {
            authorizationCodeAuth.Start();
            authorizationCodeAuth.AuthReceived += AuthorizationCodeAuth_AuthReceived;
            authReceivedEvent.Wait();
            var bandsFile = File.ReadAllText("../Files/bands.txt");
            var bands = bandsFile.Split(',', System.StringSplitOptions.RemoveEmptyEntries);

            foreach(var band in bands)
            {
                var bandData = api.SearchItems(band, SearchType.Artist);
            }
        }

        private void AuthorizationCodeAuth_AuthReceived(object sender, AuthorizationCode payload)
        {
            api = new SpotifyWebAPI
            {
                TokenType = "Bearer",
                AccessToken = payload.Code
            };
            authReceivedEvent.Set();
        }
    }
}
