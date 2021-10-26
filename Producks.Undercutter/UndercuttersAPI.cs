using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Producks.Undercutter.Models;
using Newtonsoft.Json;

namespace Producks.Undercutter
{
    public static class UndercuttersAPI
    {
        private static readonly HttpClient _client;
        private const string BaseUrl = "http://undercutters.azurewebsites.net/api";

        static UndercuttersAPI()
        {
            if (_client == null)
                _client = new HttpClient();
        }

        public static async Task<List<Category>> GetCategories()
        {
            var response = await _client.GetAsync($"{BaseUrl}/Category");
            if (!response.IsSuccessStatusCode)
                return null;

            var categories = JsonConvert.DeserializeObject<List<Category>>(await response.Content.ReadAsStringAsync());
            return categories;
        }

        public static async Task<List<Product>> GetProducts()
        {
            var response = await _client.GetAsync($"{BaseUrl}/Product");
            if (!response.IsSuccessStatusCode)
                return null;

            var products = JsonConvert.DeserializeObject<List<Product>>(await response.Content.ReadAsStringAsync());
            return products;
        }

        public static async Task<List<Brand>> GetBrands()
        {
            var response = await _client.GetAsync($"{BaseUrl}/Brand");
            if (!response.IsSuccessStatusCode)
                return null;

            var brands = JsonConvert.DeserializeObject<List<Brand>>(await response.Content.ReadAsStringAsync());
            return brands;
        }
    }
}
