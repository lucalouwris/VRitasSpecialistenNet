using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriter : MonoBehaviour
{

    [SerializeField]private float typeWriterSpeed = 50f;

    public bool isRunning { get;  private set; }

    private readonly Dictionary<HashSet<char>, float> punctuations = new Dictionary<HashSet<char>, float>()
    {
        {new HashSet<char>(){'.', ',', '!',}, 0.6f},
        {new HashSet<char>(){',', '-', ';', ':'}, 0.3f},
    };

    public Coroutine Run(string textToType, TMP_Text textLabel)
    {
       return StartCoroutine(this.TypeText(textToType, textLabel));

    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        this.isRunning = true;
        textLabel.text = string.Empty;

        float t = 0;
        int charIndex = 0;

        while(charIndex < textToType.Length)
        {
            int lastCharIndex = charIndex;

            t += Time.deltaTime * this.typeWriterSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            for(int i = lastCharIndex; i < charIndex; i++)
            {

                bool isLast = i > textToType.Length - 1;

                textLabel.text = textToType.Substring(0, charIndex);

                if (this.IsPunctuation(textToType[i], out float waitTime) && !isLast && !this.IsPunctuation(textToType[i + 1], out _))
                {
                    yield return new WaitForSeconds(waitTime);
                }
            }


            yield return null;
        }

        this.isRunning = false;
        textLabel.text = textToType;
    }

    private bool IsPunctuation(char charachter, out float waitTime)
    {
        foreach (KeyValuePair<HashSet<char>, float> punctuationCategory in this.punctuations)
        {
            if (punctuationCategory.Key.Contains(charachter))
            {
                waitTime = punctuationCategory.Value;
                return true;
            }
        }

        waitTime = default;
        return false;
    }
}
