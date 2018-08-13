using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InternshipApplicationTest.WinformsUI
{
    class WebClient<T> where T : class
    {
        private HttpClient client;

        /// <summary>
        /// Constructor for HttpClient
        /// </summary>
        /// <param name="baseURL">Everything until the controller name</param>
        public WebClient(string baseURL)
        {
            var controllerName = GetControllerName();
            client = new HttpClient { BaseAddress = new Uri($"{baseURL}{controllerName}") };
        }

        public async Task<K> GetAsync<K>(string query)
        {
            var queryOperator = String.IsNullOrEmpty(query) ? "" : "?";
            var response = await client.GetAsync($"{queryOperator}{query}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<K>();
            }
            else
            {
                return default(K);
            }
        }

        public async Task<HttpResponseMessage> PostAsync<K>(K entity)
        {
            return await client.PostAsJsonAsync<K>($"", entity);
        }

        private static string GetControllerName()
        {
            var typeName = typeof(T).Name;
            var words = Regex.Matches(typeName, "(^[a-z]+|[A-Z]+(?![a-z])|[A-Z][a-z]+)")
             .OfType<Match>()
             .Select(m => m.Value)
             .ToArray();
            var controllerName = string.Join("", words.Take(words.Length - 1));
            return controllerName;
        }

        //Normal test flow:
        // var webclient = new WebClient<TestModel>("http://localhost:52044/api/");
        // var data1 = await webclient.GetEntityAsync<TestModel>($"applicantId={1}&internshipId={1}");
        // @ check answers and calculate score (need to get all answers of the questions in the test)
        // var response = await webclient.PostEntityAsync<ApplicantInternshipModel>(applicantInternship1);
        // var passed = response.StatusCode == HttpStatusCode.Accepted;
    }
}
