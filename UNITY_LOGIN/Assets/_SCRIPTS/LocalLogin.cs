using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using System.IO;

public class LocalLogin : MonoBehaviour
{
    public GameObject username;
    public GameObject password;

    private string username_s;
    private string password_s;
    private string[] lines;
    private string decryptedPass;

    void Update()
    {
        // interaction a l'aide de la touche tab pour passer de champ en champ
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }
            username_s = username.GetComponent<InputField>().text;
        }

        // interaction de la touche entrer pour valider une fois les champs complets
        if (Input.GetKeyDown(KeyCode.Return))
        {
            username_s = username.GetComponent<InputField>().text;
            password_s = password.GetComponent<InputField>().text;

            if (username_s != "" && password_s != "")
            {
                LoginButton();
            }
        }
    }

    // fonction permettant de vérifier si les identifiants sont cohérents avec le fichier texte
    public void LoginButton()  
    {
        username_s = username.GetComponent<InputField>().text;
        password_s = password.GetComponent<InputField>().text;

        bool username_b = false;
        bool password_b = false;

        // si le champ username n'est pas vide alors...
        if(username_s != "")
        {
            if (System.IO.File.Exists(@"/Users/noe/Documents/_PROJETS_UNITY/UNITY_LOGIN_METHODS/UNITY_LOGIN/Assets/"+ username_s +".txt"))
            {
                username_b = true;
                lines = System.IO.File.ReadAllLines(@"/Users/noe/Documents/_PROJETS_UNITY/UNITY_LOGIN_METHODS/UNITY_LOGIN/Assets/" + username_s + ".txt");
            }
            else
            {
                Debug.LogWarning("Le nom semble invalide !");
            }
        }
        else
        {
            Debug.LogWarning("le champ nom semble vide !");
        }

        // si le champ password n'est pas vide alors...
        if(password_s != "")
        {
 
            if (System.IO.File.Exists(@"/Users/noe/Documents/_PROJETS_UNITY/UNITY_LOGIN_METHODS/UNITY_LOGIN/Assets/" + username_s +".txt"))
            {
                int i = 1;

                // encodage du passe word dans le document texte
                foreach (char c in lines[2])
                {
                    i++;
                    char Decrypted = (char)(c / i);
                    decryptedPass += Decrypted.ToString();
                }
            }
            else
            {
                Debug.LogWarning("Le mot de passe n'est pas valide !");
            }

            if (password_s == decryptedPass)
            {
                password_b = true;
            }
            else
            {
                Debug.LogWarning("Le mot de passe n'est pas valide !");
            }
        }
        else
        {
            Debug.LogWarning("Le champ de mot de passe semble vide !");
        }

        // si les deux champs sont valide alors...
        if(username_b == true && password_b == true)
        {
            Debug.Log("Succes login / open start menu");
            username.GetComponent<InputField>().text = "";
            password.GetComponent<InputField>().text = "";
        }
    }
}