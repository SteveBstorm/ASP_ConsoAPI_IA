using Dal.Interface;
using Dal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Services
{
    public class MessageService : IMessageService
    {
        private HttpClient _httpClient;

        public MessageService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7186/api/");
        }

        public IEnumerable<Message> GetAll()
        {
            IEnumerable<Message> messages = new List<Message>();
            using (HttpResponseMessage response = _httpClient.GetAsync("message").Result)
            {
                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    messages = JsonConvert.DeserializeObject<IEnumerable<Message>>(json);
                }
            }
            return messages;
        }

        public void Post(string content, string Token)
        {
            var newmessage = new { Content = content };
            string jsonmessage = JsonConvert.SerializeObject(newmessage);
            HttpContent messageContent = new StringContent(jsonmessage, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            using (HttpResponseMessage response = _httpClient.PostAsync("message", messageContent).Result)
            {
                try
                {
                    if (!response.IsSuccessStatusCode) throw new Exception(response.ReasonPhrase);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }
    }
}
