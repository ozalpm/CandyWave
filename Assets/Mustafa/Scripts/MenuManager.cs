using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public InputField ipText;
    // Start is called before the first frame update
    void Start()
    {
        if (!NetworkManager.singleton)
        {
            SceneManager.LoadScene(0);
            return;
        }
    }

    public void Host()
    {
        NetworkManager.singleton.StartHost();
    }

    public void Connect()
    {
        NetworkManager.singleton.networkAddress = ipText.text;
        NetworkManager.singleton.StartClient();
    }
}
