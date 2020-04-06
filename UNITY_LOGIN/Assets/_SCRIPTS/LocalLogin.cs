using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;

public class LocalLogin : MonoBehaviour
{

    public GameObject username;
    public GameObject password;
    public GameObject email;
    public GameObject confPassword;

    private string username_s;
    private string email_s;
    private string password_s;
    private string confPassword_s;
    private string form_s;

    private string[] characters = {
    "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
    "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
    "_", "-", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};

    private bool emailValid = false;


    void Update()
    {
        // recuparation des strings des input field
        username_s = username.GetComponent<InputField>().text;
        email_s = email.GetComponent<InputField>().text;
        password_s = password.GetComponent<InputField>().text;
        confPassword_s = confPassword.GetComponent<InputField>().text;

        // interaction a l'aide de la touche tab pour passer de champ en champ
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                email.GetComponent<InputField>().Select();
            }
            if (email.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }
            if (password.GetComponent<InputField>().isFocused)
            {
                confPassword.GetComponent<InputField>().Select();
            }
           
        }

        // interaction de la touche entrer pour valider une fois les champs complets
        if (Input.GetKeyDown(KeyCode.Return))
        {

            if(username_s != "" &&  email_s != "" && password_s != "" && confPassword_s != "")
            {
                RegisterButton();
            }
        }
    }

    // fonction permettant de vérifier si les adresses contiennent des caractères valides en début et fin d'adresse mail
    private void EmailValidation()
    {
        bool startWith = false;
        bool endWith = false;

        for(int i = 0; i< characters.Length; i++)
        {
            if (email_s.StartsWith(characters[i]))
            {
                startWith = true;
            }
        }

        for (int i = 0; i < characters.Length; i++)
        {
            if (email_s.EndsWith(characters[i]))
            {
                endWith = true;
            }
        }

        if(startWith == true && endWith == true)
        {
            emailValid = true;
        }
        else
        {
            emailValid = false;
            Debug.LogWarning("vérifier le début et la fin de votre adresse email");
        }
    }

    // fonction permettant d'enregistrer les informations des différents champs
    public void RegisterButton()
    {
        // Boolean permettant de valider la qualité des données avant de les enregistrer
        bool username_b = false;
        bool email_b = false;
        bool password_b = false;
        bool confPassword_b  = false;

        if (username_s != "")
        {
            if (!System.IO.File.Exists(@"/Users/noe/Documents/_PROJETS_UNITY/UNITY_VR_TOOLS/UNITY_LOGIN/Assets/" + username_s + ".txt"))
            {
                username_b = true;
            }
            else 
            {
                Debug.LogWarning("Ce nom est déjà pris !");
            }
        }
        else
        {
            Debug.LogWarning("Le champ nom est vide !");
        }

        // si le champ email n'est pas vide alors...
        if (email_s != "")
        {
            EmailValidation();

            if (emailValid == true)
            {
                if (email_s.Contains("@") && email_s.Contains("."))
                {
                    email_b = true;
                }
                else
                {
                    Debug.LogWarning("Votre email semble incorrect !");
                }
            }
        }
        else
        {
            Debug.LogWarning("Le champ Email semble vide !");
        }

        // si le champ password email n'est pas vide alors...
        if (password_s != "")
            {
                if(password_s.Length > 5)
                {
                password_b = true;
                }
                else
                {
                Debug.LogWarning("Votre mot de passe doit comprendre 6 caractères minium !");
                }
        }
        else
        {
            Debug.LogWarning("Le champ mot de passe semble vide !");
        }

        // si le champ confPassword n'est pas vide alors...
        if (confPassword_s != "")
        {
            if(confPassword_s  == password_s)
            {
                confPassword_b = true;
            }
            else
            {
                Debug.LogWarning("Les mots de passes de correspondent pas !");
            }
        } 
        else
        {
            Debug.LogWarning("Le champ confirmer mot de passe semble vide !");
        }

        // si les champs sont correctes alors les booleans passent à true alors...
        if (username_b == true && password_b == true && email_b == true && confPassword_b == true)
        {
            bool Clear = true;
            int i = 1;

            // encodage du passe word dans le document texte
            foreach(char c in password_s)
            {
                if(Clear == true)
                {
                    password_s = "";
                    Clear = false;
                }
                i++;
                char Encrypted = (char)(c * i);
                password_s += Encrypted.ToString();
            }

            // enregistrement des données dans le style username > email > password
            form_s = (username_s + "/" + email_s + "/" + password_s);

            // stockage des informations dans le fichier texte via le chemin indiqué, sous le nom de l'utilisateur via username et sous la forme form_s
            System.IO.File.WriteAllText(@"/Users/noe/Documents/_PROJETS_UNITY/UNITY_VR_TOOLS/UNITY_LOGIN/Assets/" + username_s + ".txt", form_s);
          
            // reset des différents champs après enregistrement
            username.GetComponent<InputField>().text = "";
            email.GetComponent<InputField>().text = "";
            password.GetComponent<InputField>().text = "";
            confPassword.GetComponent<InputField>().text = "";

            Debug.Log("Registration Complete");
        }
    }


}
