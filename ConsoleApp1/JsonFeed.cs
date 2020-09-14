using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class JsonFeed
    {
        static string _url = "";

        public JsonFeed() { }
        public JsonFeed(string endpoint)
        {
            _url = endpoint;
        }
        
		public static string[] GetRandomJokes(string firstname, string lastname, string category, int results)
		{
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(_url);
			string url = "jokes/random";
			if (category != null)
			{
				if (url.Contains('?'))
					url += "&";
				else url += "?";
				url += "category=";
				url += category;
			}

            string[] allJokes = new string[results];
            for (int i = 0; i < results; i++)
            {
                string joke = Task.FromResult(client.GetStringAsync(url).Result).Result;

                if (firstname != null && lastname != null)
                {
                    int index = joke.IndexOf("Chuck Norris");
                    string firstPart = joke.Substring(0, index);
                    string secondPart = joke.Substring(0 + index + "Chuck Norris".Length, joke.Length - (index + "Chuck Norris".Length));
                    joke = firstPart + " " + firstname + " " + lastname + secondPart;
                }
                allJokes[i] = JsonConvert.DeserializeObject<dynamic>(joke).value;
            }
            return allJokes;
        }

        /// <summary>
        /// returns an object that contains name and surname
        /// </summary>
        /// <param name="client2"></param>
        /// <returns></returns>
		public static dynamic Getnames()
		{
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(_url);
			var result = client.GetStringAsync("").Result;
			return JsonConvert.DeserializeObject<dynamic>(result);
		}

		public static string[] GetCategories()
		{
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(_url);

			return new string[] { Task.FromResult(client.GetStringAsync("jokes/categories").Result).Result };
		}
    }
}
