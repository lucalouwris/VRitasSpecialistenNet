using UnityEngine;

[System.Serializable]
public class HeartJson
{
    public string measured_at;
    public string[] data;

    public static HeartJson CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<HeartJson>(jsonString);
    }

    // Given JSON input:
    // {"name":"Dr Charles","lives":3,"health":0.8}
    // this example will return a PlayerInfo object with
    // name == "Dr Charles", lives == 3, and health == 0.8f.
}