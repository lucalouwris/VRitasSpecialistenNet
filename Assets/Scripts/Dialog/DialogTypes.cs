using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTypes : MonoBehaviour
{
    public Question[] question;
    public struct Question
    {
        public string qText;
        public List<string> answers;
        public int qGrade;
    }
}
