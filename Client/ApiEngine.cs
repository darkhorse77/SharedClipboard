using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    /// <summary>
    /// Класс для отправки запросов к серверу
    /// </summary>
    public static class ApiEngine
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<string> CallGetAsync(string url)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                return await response.Content.ReadAsStringAsync();
            }
            catch { throw; } // todo: реализовать обработку исключений
        }

        public static async Task CallPutAsync(string url)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsync(url, null).ConfigureAwait(false);
            }
            catch { throw; } 
        }
    }
}