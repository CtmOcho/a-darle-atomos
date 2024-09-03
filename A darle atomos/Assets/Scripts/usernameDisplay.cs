using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class usernameDisplay : MonoBehaviour
{

    public TMP_Text username;

    // Start is called before the first frame update
    void Start()
    {
        username.text = SessionData.username;
    }

}
