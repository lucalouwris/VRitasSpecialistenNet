using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UnityEngine;

public class ReadHeartrate : MonoBehaviour
{
    [SerializeField] private ConfigFile configFile;
    [SerializeField] private float interval = 0.5f;
    [SerializeField] private string pathToCsv;
    
    private HttpClient client;
    private List<float> time = new List<float>();
    private List<string> heartRate = new List<string>();

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
            
            time.Add((int)Time.time);
            heartRate.Add(split[1]);
            Debug.Log($"Received at:{split[0]}, Heartrate:{split[1]}");
            
            return localData;
        }
    }
    
    private void OnDisable()
    {
        string csv = "";

        for (int i = 0; i < heartRate.Count; i++)
        {
            csv += $"{time[i]},{heartRate[i]}\n";
        }

        string date = DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute;
        File.WriteAllText(Application.dataPath+pathToCsv+$"heartrateData{date}.csv", csv);
    }
}
