using Dal.Interface;
using Dal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Services
{
    public class UserService : IUserService
    {
        private HttpClient _httpClient;

        public UserService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7186/api/");
        }

        public User Login(string idenfifiant, string password)
        {
            var login = new { Idenfifiant = idenfifiant, Password = password };
            string jsonToSend = JsonConvert.SerializeObject(login);
            HttpContent content = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
            User cu = new User();
            using (HttpResponseMessage response = _httpClient.PostAsync("auth/login", content).Result)
            {
                if (response.IsSuccessStatusCode)
                {
                    string recievedJson = response.Content.ReadAsStringAsync().Result;
                    cu = JsonConvert.DeserializeObject<User>(recievedJson);
                }
                else
                {
                    Console.WriteLine(response.StatusCode);
                }
            }
            return cu;
        }

        public void Register(NewUserModel user)
        {
            string jsonToSend = JsonConvert.SerializeObject(user);
            HttpContent content = new StringContent(jsonToSend, Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = _httpClient.PostAsync("auth/register", content).Result)
            {
                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ReasonPhrase);
            }
        }

        public User GetById(int id)
        {
            User user = new User();
            using(HttpResponseMessage response = _httpClient.GetAsync("auth/" + id).Result)
            {
                if(response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    user = JsonConvert.DeserializeObject<User>(json);
                }
                else { Console.WriteLine(response.ReasonPhrase); }
            }
            return user;
        }
    }
}
