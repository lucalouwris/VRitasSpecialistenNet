using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GetConfig", menuName = "ScriptableObjects/GetConfig", order = 1)]
public class ConfigFile : ScriptableObject
{
    public string Url;
    public string Token;
}
