using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ContactInfo.Services
{
    public class GenericHttpClient<T>  where T : class
    {
        /// <summary>
        /// Get API call 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="URL"></param>
        /// <returns></returns
        /// Dev- Sushil Shipalkar
        /// Date- 23/7/2018
        public async Task<TR> APIGetAsync<TR>(string URL)
        {
            TR returnResult;

            using (var client = new HttpClient())
            {
                
                var response = Task.Run(async () => await client.GetStringAsync(URL)).Result;
                returnResult = JsonConvert.DeserializeObject<TR>(response);
            }
            return returnResult;
        }

        /// <summary>
        /// Get API List Call
        /// </summary>
        /// <typeparam name="?"></typeparam>
        /// <param name="URL"></param>
        /// <returns></returns>
        /// Dev- Sushil Shipalkar
        /// Date- 23/7/2018
        public async Task<List<T>> APIGetListAsync<T>(string URL)
        {
            List<T> returnResult;

            using (var client = new HttpClient())
            {               
                var response = Task.Run(async () => await client.GetStringAsync(URL)).Result;
                returnResult = JsonConvert.DeserializeObject<List<T>>(response);
            }
            return returnResult;
        }

        /// <summary>
        /// Post Data to API
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Model"></param>
        /// <param name="URL"></param>
        /// <returns></returns>
        /// Dev- Sushil Shipalkar
        /// Date- 23/7/2018
        public async Task<TR> APIPostAsync<TR>(T Model, string URL)
        {
            TR returnResult;

            using (var client = new HttpClient())
            {
               
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpContent content = new StringContent(JsonConvert.SerializeObject(Model), Encoding.UTF8, "application/json");

                returnResult = await client.PostAsync(URL, content).ContinueWith((postTask) =>
                {
                    return JsonConvert.DeserializeObject<TR>(postTask.Result.Content.ReadAsStringAsync().Result);
                });
                return returnResult;
            }

        }

        /// <summary>
        /// Post List to API
        /// </summary>
        /// <typeparam name="TR"></typeparam>
        /// <param name="Model"></param>
        /// <param name="URL"></param>
        /// <returns></returns
        //// Dev- Sushil Shipalkar
        //// Date- 23/7/2018
        public async Task<TR> APIPostListAsync<TR>(List<T> Model, string URL)
        {
            TR returnResult;

            using (var client = new HttpClient())
            {
               
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpContent content = new StringContent(JsonConvert.SerializeObject(Model), Encoding.UTF8, "application/json");

                returnResult = await client.PostAsync(URL, content).ContinueWith((postTask) =>
                {
                    return JsonConvert.DeserializeObject<TR>(postTask.Result.Content.ReadAsStringAsync().Result);
                });
                return returnResult;
            }

        }

        /// <summary>
        /// Get API call 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="URL"></param>
        /// <returns></returns
        /// Dev- Sushil Shipalkar
        /// Date- 23/7/2018
        public async Task<TR> APIDeleteAsync<TR>(string URL)
        {
            TR returnResult;

            using (var client = new HttpClient())
            {

                var response = Task.Run(async () => await client.DeleteAsync(URL)).Result;
              //  returnResult = JsonConvert.DeserializeObject<TR>(response);

                returnResult = await client.DeleteAsync(URL).ContinueWith((postTask) =>
                {
                    return JsonConvert.DeserializeObject<TR>(postTask.Result.Content.ReadAsStringAsync().Result);
                });
            }
            return returnResult;
        }

    }
}
