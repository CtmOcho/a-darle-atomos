using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public string user;
    public string pass;

    public string Stringify() 
    {
        return JsonUtility.ToJson(this);
    }

    public static UserData Parse(string json)
    {
        return JsonUtility.FromJson<UserData>(json);
    }
}
