using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!NetworkManager.singleton)
        {
            SceneManager.LoadScene(0);
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
