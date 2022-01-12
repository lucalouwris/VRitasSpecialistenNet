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
    [SerializeField] private Rigidbody player;
    [SerializeField] private GameStates states;
    
    private HttpClient client;
    private List<int> time = new List<int>();
    private List<float> velocity = new List<float>();
    private List<string> heartRate = new List<string>();
    private List<string> gameStates = new List<string>();

    public static float currPulse = 0;

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

            time.Add((int)Time.time);
            velocity.Add(player.velocity.magnitude);
            heartRate.Add(localData);
            gameStates.Add(states.getGameStates());
            
            return localData;
        }
    }
    
    private void OnDisable()
    {
        string csv = "";
        float average = 0;
        float peak = 0;

        for (int i = 0; i < heartRate.Count; i++)
        {
            float heartRateFloat = float.Parse(heartRate[i]);
            csv += $"{time[i]},{gameStates[i]},{velocity[i]},{heartRate[i]}\n";            
            average += heartRateFloat;
            if (heartRateFloat > peak)
            {
                peak = heartRateFloat;
            }
        }
        average = average/heartRate.Count;

        csv += $"Average,{average}\n";
        csv += $"Peak,{peak}\n";

        string date = DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute;
        File.WriteAllText(Application.dataPath+pathToCsv+$"heartrateData{date}.csv", csv);
    }
}
