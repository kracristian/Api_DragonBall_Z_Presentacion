using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Api_DragonBall_Z.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api_DragonBall_Z.Servicios
{
    public class DragonBallService : IDragonBallService
    {
        private readonly HttpClient _httpClient;

        public DragonBallService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<DragonBallResponse> GetCharacterPage(int page = 1, int limit = 12)
        {
            var response = await _httpClient.GetAsync($"https://dragonball-api.com/api/characters?page={page}&limit={limit}");

            if (response.IsSuccessStatusCode)
            {
                var characterJson = await response.Content.ReadAsStringAsync();
                var characterResponse = JsonConvert.DeserializeObject<DragonBallResponse>(characterJson);
                return characterResponse;
            }

            return null;
        }

        [HttpGet("{id}")]
        public Character ObtenerPersonaje(int id)
        {
            try
            {
                var response = _httpClient.GetAsync($"https://dragonball-api.com/api/characters/{id}").Result;

                if (response.IsSuccessStatusCode)
                {
                    // Leer y deserializar el contenido JSON de la respuesta
                    var characterJson = response.Content.ReadAsStringAsync().Result;
                    var character = JsonConvert.DeserializeObject<Character>(characterJson);
                    return character;
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw new Exception("Error al obtener el personaje");
                }
            }
            catch (HttpRequestException)
            {
                // Manejar el error de conexión de alguna otra manera si es necesario
                throw new HttpRequestException("Error al conectarse al servicio");
            }
        }

        public async Task<List<Character>> SearchCharacters(string search)
        {
            var response = await _httpClient.GetAsync($"https://dragonball-api.com/api/characters?name={search}");

            if (response.IsSuccessStatusCode)
            {
                var characterJson = await response.Content.ReadAsStringAsync();
                var characterResponse = JsonConvert.DeserializeObject<List<Character>>(characterJson);
                return characterResponse;
            }

            return null;
        }
    }
}
