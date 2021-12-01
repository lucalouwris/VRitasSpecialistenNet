using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UnityEngine;

public class ReadHeartrate : MonoBehaviour
{
    [SerializeField] private ConfigFile configFile;
    [SerializeField] private float interval = 0.5f;
    private HttpClient client;
    private void Start()
    {
        Invoke(nameof(CallGet),interval);
        client = new HttpClient();
        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", configFile.Token);
        //client.DefaultRequestHeaders.Add("Authorization", configFile.Token);
        //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
    }

    public void CallGet()
    {
        Debug.Log($"Data returned: {Get(configFile.Url)}");
        Invoke(nameof(CallGet),interval);
    }

    private async Task<string> Get(string uri)
    {
        using (var request = new HttpRequestMessage(HttpMethod.Get, configFile.Url))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", configFile.Token);
            var response = await client.SendAsync(request);
            string data = await response.Content.ReadAsStringAsync();
            Debug.Log($"data from task: {data}");
            return data;
        }
        //var result = client.GetStreamAsync(configFile.Url);
        //result.Result.ToString();

        // var response = await client.SendAsync(configFile.Url);
        // return response.con.ToString();
    }
    
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
