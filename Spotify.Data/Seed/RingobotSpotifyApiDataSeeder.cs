using GraphQLInDotNet.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using SpotifyApi.NetCore;
using SpotifyApi.NetCore.Authorization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.Data.Seed
{
    public class RingobotSpotifyApiDataSeeder : ISeeder
    {
        private HttpClient httpClient;
        private AccountsService apiService;

        public RingobotSpotifyApiDataSeeder()
        {
            this.httpClient = new HttpClient();
            this.apiService = new AccountsService(httpClient, new SpotifyConfig());
        }

        public  async Task SeedDataAsync(IDataContext dataContext)
        {
            var search = new SearchApi(this.httpClient, this.apiService);

            var bandsFile = Files.FIlesResource.bands;
            var bands = bandsFile.Split(',', System.StringSplitOptions.RemoveEmptyEntries);

            foreach (var band in bands)
            {
                var bandData = await search.Search(query: band, "artist");
            }
        }

        private class SpotifyConfig : IConfiguration
        {
            public string this[string key] {
                get
                {
                    switch(key)
                    {
                        case nameof(SpotifyApiClientId): return SpotifyApiClientId;
                        case nameof(SpotifyApiClientSecret): return SpotifyApiClientSecret;
                        default: return null;
                    }
                }
                set
                {
                    throw new NotImplementedException();
                }
            }
            public string SpotifyApiClientId => "759318169b66436ba800082fc88e0efb";
            public string SpotifyApiClientSecret => "92349ad8387845caa08b0ccef265e77c";

            public IEnumerable<IConfigurationSection> GetChildren()
            {
                throw new NotImplementedException();
            }

            public IChangeToken GetReloadToken()
            {
                throw new NotImplementedException();
            }

            public IConfigurationSection GetSection(string key)
            {
                throw new NotImplementedException();
            }
        }
    }
}
