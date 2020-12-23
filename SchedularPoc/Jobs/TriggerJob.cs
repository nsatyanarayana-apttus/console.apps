using Quartz;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace SchedularPoc.Jobs
{
    public class TriggerJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            string url = "https://localhost:44392/api/demo";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body.
                    string b = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine(b);
                    Debug.WriteLine(b);
                }
                else
                {
                    Debug.WriteLine(response.StatusCode.ToString()+"  "+ response.ReasonPhrase);
                }
            }

        }
    }
}
