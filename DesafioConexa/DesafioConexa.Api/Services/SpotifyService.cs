using System;
using DesafioConexa.Api.Enums;
using DesafioConexa.Api.Models;
using DesafioConexa.Api.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RestSharp;
using DesafioConexa.Api.Util;

namespace DesafioConexa.Api.Services
{
    public class SpotifyService : IStreamingService
    {
        private readonly RestClient _restClient;
        private readonly SpotifySettings _spotifySettings;
        private string _token;

        public SpotifyService(IOptions<SpotifySettings> spotifySettings)
        {
            _spotifySettings = spotifySettings.Value;
            _restClient = new RestClient(_spotifySettings.BaseUrl);
        }

        public PlayList GetPlaylistRecommendation(Category playListCategory)
        {
            Authenticate();
            var playListId = GetPlayListId(playListCategory);
            return GetPlayList(playListId);
        }

        private string GetPlayListId(Category playListCategory)
        {
            var request = new RestRequest("/browse/categories/{Category}/playlists", Method.GET);
            request.AddUrlSegment("Category", playListCategory.ToString().ToLower());
            request.AddQueryParameter("country", "BR");
            request.AddQueryParameter("offset", "0");
            request.AddQueryParameter("limit", "1");
            request.AddHeader("Authorization", $"Bearer {_token}");

            var response = _restClient.Execute(request);

            if (response.IsSuccessful)
            {
                return JObject.Parse(response.Content)["playlists"]["items"][0]["id"].ToString();
            }

            throw new Exception("Erro ao obter o ID da playlist!");
        }

        private PlayList GetPlayList(string playListId)
        {
            var playList = new PlayList();

            var request = new RestRequest("playlists/{PlayListId}/tracks", Method.GET);
            request.AddUrlSegment("PlayListId", playListId);
            request.AddQueryParameter("offset", "0");
            request.AddQueryParameter("limit", "100");

            request.AddHeader("Authorization", $"Bearer {_token}");

            var response = _restClient.Execute(request);

            if (response.IsSuccessful)
            {
                var trackList = JObject.Parse(response.Content)["items"];

                foreach (var track in trackList)
                {
                    var name = track["track"]["name"];

                    if (name != null)
                    {
                        playList.TrackList.Add(new Track(name.ToString()));
                    }
                }

                return playList;
            }

            throw new Exception("Erro ao obter a lista de musicas da playlist!");
        }

        private void Authenticate()
        {
            var client = new RestClient(_spotifySettings.AuthUrl);

            RestRequest request = new RestRequest { Method = Method.POST };

            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Accept", "application/json");
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("client_id", _spotifySettings.ClientId);
            request.AddParameter("client_secret", _spotifySettings.ClientSecret);

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                _token = JObject.Parse(response.Content)["access_token"].ToString();
                return;
            }

            throw new Exception("Erro ao obter token de acesso ao spotify!");
        }
    }
}
