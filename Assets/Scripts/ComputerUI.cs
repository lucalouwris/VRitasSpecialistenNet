using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerUI : MonoBehaviour
{
    [SerializeField] List<GameObject> messagePositions;
    [SerializeField] GameObject notification;
    List<GameObject> messages = new List<GameObject>();

    private void Start()
    {
        receiveNotif();
        receiveNotif();
        receiveNotif();
        receiveNotif();
        receiveNotif();
        receiveNotif();
        receiveNotif();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void receiveNotif()
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
