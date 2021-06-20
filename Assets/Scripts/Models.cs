using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Sets paswword, type and username as serializable
/// </summary>
[Serializable]
public class RegisterMessage
{
    public string type;
    public string username;
    public string password;
}
