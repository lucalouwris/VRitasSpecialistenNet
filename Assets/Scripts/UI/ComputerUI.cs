/*
    There are two different ways that messages can pop on the screen. 
    The first way is that messages pop up all over the screen in a random position in random intervals and sound. 
    The instantiating of the messages happens in the receiveNotif() function. 
    There can be a max of 10 messages on the screen. Message 11 deletes the first one and so on. 
    The more structured messages instantiate in the receiveNotifStructured() function. 
    This function does much of the same but put the message on the side of the screen with a maximum of 6 messages moving from top to bottom and gets deleted when out of screen.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerUI : MonoBehaviour
{
    [SerializeField] List<GameObject> messagePositions;
    [SerializeField] AudioClip[] clips;

    [SerializeField] GameObject notification;
    List<GameObject> messages = new List<GameObject>();
    [SerializeField] GameObject screen;
    [SerializeField] GameObject bg;
    [SerializeField] GameObject refuel;
    [SerializeField] GameObject onState;
    [SerializeField] GameObject offState;
    AudioSource audioSource;
    AudioClip clip;
    [SerializeField] int maxMessages = 10;
    [SerializeField] float volume = 0.5f;
    float interval = 0;
    bool active = false;
    [HideInInspector] public bool completed = false;

    private void Start()
    {
        interval = Random.Range(1f, 5f);
        audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if (interval > 0 && active)
        {
            interval -= Time.deltaTime;
        }
        else if (interval <= 0 && active)
        {
            interval = Random.Range(0f, 2f);
            receiveNotif();
        }
    }
    public void TurnOn() // Turn on computer.
    {
        offState.SetActive(false);
        onState.SetActive(true);
        active = true;
    }
    public void Wipe() // Clear the screen and complete the minigame.
    {
        active = false;
        foreach (GameObject message in messages)
        {
            Destroy(message.gameObject);
        }
        messages.Clear();
        completed = true;
    }
    void receiveNotif()
    {
        clip = clips[Random.Range(0, clips.Length)];
        audioSource.PlayOneShot(clip, volume);
        float width = Random.Range(-0.55f * transform.localScale.x, 0.55f * transform.localScale.x); // Random x on screen.
        float height = Random.Range(-0.4f * transform.localScale.y, 0.4f * transform.localScale.y); // Random y on screen.
        GameObject notif = Instantiate(notification, new Vector3(0, 0, 0), Quaternion.Euler(0, 180, 0), screen.transform.parent); // Instantiate a new message
        notif.transform.SetParent(screen.transform);
        Vector3 spawnPosition = new Vector3(bg.transform.position.x + width, bg.transform.position.y + height, screen.transform.position.z);
        notif.transform.position = spawnPosition;
        notif.transform.localScale = new Vector3(0.0025f, 0.0025f, 1);
        Image notifImg = notif.GetComponent<Image>();
        notifImg.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1), 1f); // Random color to the image of the message.

        for (int i = 0; i < messages.Count; i++)
        {
            if (messages.Count == maxMessages)
            {
                Destroy(messages[i].gameObject);
                messages.RemoveAt(i);
                refuel.SetActive(true);
            }
        }
        messages.Add(notif);
    } // Other type of messaging which is more structured
    void receiveNotifStructured()
    {
        Vector3 spawnPosition = new Vector3(messagePositions[0].transform.position.x, messagePositions[0].transform.position.y, messagePositions[0].transform.position.z);
        GameObject notif = Instantiate(notification, spawnPosition, Quaternion.Euler(0, 180, 0), messagePositions[0].transform.parent);
        notif.transform.SetParent(messagePositions[0].transform);
        Image notifImg = notif.GetComponent<Image>();
        notifImg.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1), 1f);

        notif.name = "0";
        if (messages.Count > 0)
        {
            for (int i = 0; i < messages.Count; i++)
            {
                int number = int.Parse(messages[i].name);
                if (number < messagePositions.Count - 1) // Move messages down the list.
                {
                    notif.transform.SetParent(messagePositions[number + 1].transform);
                    notif.transform.position = messagePositions[number + 1].transform.position;
                    number++;

                    notif.name = number.ToString();
                }
                else //If message has been pushed out of the last position on screen.
                {
                    messages.RemoveAt(i - 1);
                    Destroy(messages[i - 1]);
                }
            }
        }
        messages.Add(notif);
    }
}
