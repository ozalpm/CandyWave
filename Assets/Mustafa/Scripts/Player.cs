using System.Collections;
using System.Collections.Generic;
using Hertzole.GoldPlayer;
using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public GoldPlayerGraphics graphics;

    public GameObject cameraObject;

    public GoldPlayerController goldPlayerController;
    // Start is called before the first frame update
    void Start()
    {
        graphics.UpdateGraphics(isLocalPlayer);
        goldPlayerController.canMove = isLocalPlayer;
        cameraObject.SetActive(isLocalPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
