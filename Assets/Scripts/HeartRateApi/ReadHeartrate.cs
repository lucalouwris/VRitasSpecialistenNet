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

    [SerializeField] private string time;
    [SerializeField] private string heartRate;
    
    private void Start()
    {
        Invoke(nameof(CallGet),interval);
        client = new HttpClient();
    }

    public void CallGet()
    {
        Get(configFile.Url);
        Invoke(nameof(CallGet),interval);
    }

    private async Task<string> Get(string uri)
    {
        using (var request = new HttpRequestMessage(HttpMethod.Get, configFile.Url))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", configFile.Token);
            var response = await client.SendAsync(request);
            string localData = await response.Content.ReadAsStringAsync();

            Debug.Log(localData);
            
            localData = localData.Replace("{\"measured_at\":", "");
            localData = localData.Replace("\"data\":{\"heart_rate\":", "");
            localData = localData.Replace("}}", "");

            string[] split = localData.Split(',');
            
            Debug.Log($"Received at:{split[0]}, Heartrate:{split[1]}");
            
            return localData;
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
