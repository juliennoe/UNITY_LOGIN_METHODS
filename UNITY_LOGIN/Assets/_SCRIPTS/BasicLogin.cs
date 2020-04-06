using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicLogin : MonoBehaviour
{
    public InputField pseudo;
    public InputField password;


    void Update()
    {
        Debug.Log("enter your loggin");

        if (Input.GetKeyDown("return"))
        {
            if (pseudo.text == "julien" && password.text == "230190")
            {
                Debug.Log("Loggin valide");
            }
            else
            {
                Debug.Log("Loggin failed");
            }
        
        }

    }
}
