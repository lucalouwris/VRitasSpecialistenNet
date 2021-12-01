using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerUI : MonoBehaviour
{
    [SerializeField] List<GameObject> messagePositions;

    [SerializeField] GameObject notification;
    List<GameObject> messages = new List<GameObject>();
    [SerializeField] GameObject screen;
    [SerializeField] GameObject bg;
    [SerializeField] int maxMessages = 25;
    float interval = 0;

    private void Start()
    {
        interval = Random.Range(1f,5f);
    }
    // Update is called once per frame
    void Update()
    {
        if (interval > 0)
        {
            interval -= Time.deltaTime;
        }
        else
        {
            interval = Random.Range(0f, 3f);
            receiveNotif();
        }
    }
    void receiveNotif()
    {
        GameObject notif = Instantiate(notification, new Vector3(0,0,0), Quaternion.Euler(0, 180, 0), screen.transform.parent);
        notif.transform.SetParent(screen.transform);
        Vector3 spawnPosition = new Vector3(bg.transform.position.x + Random.Range(-0.6f, 0.6f), bg.transform.position.y + Random.Range(-0.4f, 0.4f), screen.transform.position.z);
        notif.transform.position = spawnPosition;
        notif.transform.localScale = new Vector3(0.0025f, 0.0025f, 1);
        Image notifImg = notif.GetComponent<Image>();
        notifImg.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1), 1f);

            for (int i = 0; i < messages.Count; i++)
            {
                if(messages.Count == maxMessages)
                {
                    messages.RemoveAt(i);
                    Destroy(messages[i]);
                }
            }
            messages.Add(notif);
    } 
    void receiveNotifStructured()
    {
        Vector3 spawnPosition = new Vector3(messagePositions[0].transform.position.x, messagePositions[0].transform.position.y, messagePositions[0].transform.position.z);
        GameObject notif = Instantiate(notification, spawnPosition, Quaternion.Euler(0, 180, 0), messagePositions[0].transform.parent);
        notif.transform.SetParent(messagePositions[0].transform);
        Image notifImg = notif.GetComponent<Image>();
        notifImg.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1), 1f);

        notif.name = "0";
        if(messages.Count > 0)
        {
            for (int i = 0; i < messages.Count; i++)
            {
                int number = int.Parse(messages[i].name);
                if (number < messagePositions.Count-1)
                {
                    notif.transform.SetParent(messagePositions[number+1].transform);
                    notif.transform.position = messagePositions[number+1].transform.position;
                    number++;
                    
                    notif.name = number.ToString();
                }
                else
                {
                    messages.RemoveAt(i-1);
                    Destroy(messages[i-1]);
                }
            }
        }
        messages.Add(notif);
    } 
}
