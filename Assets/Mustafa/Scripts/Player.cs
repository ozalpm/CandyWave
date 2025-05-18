using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Hertzole.GoldPlayer;
using Mirror;
using Unity.Mathematics;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public GoldPlayerGraphics graphics;
    public GameObject cameraObject;
    public GoldPlayerController goldPlayerController;
    public GoldPlayerAnimator goldPlayerAnimator;

    public GameObject gunObject;

    public Camera cam;
    public Transform bulletExitPos, bulletExitPosOther;

    [SyncVar(hook = nameof(OnIdChanged))] public int Id;

    public float fireCooldown;
    private float lastFire;

    public float damage;

    public GameObject impactEffect;
    public GameObject bulletObject;

    public Animator gunAnim;
    public CharacterController ch;
    private void OnEnable()
    {
        GameManager.GameStateChanged += GameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.GameStateChanged -= GameStateChanged;
    }

    public void GameStateChanged(GameManager.GameState gameState)
    {
        if (gameState==GameManager.GameState.Lobby)
        {
            LobbySet();
        }
        else
        {
            GameSet();
        }
    }

    public void OnIdChanged(int oldId,int newId)
    {
        if (isLocalPlayer)
        {
            if (GameManager.Init.gameState==GameManager.GameState.Lobby)
            {
                transform.position=GameManager.Init.GetLobbyPos(newId).position;
                transform.rotation=GameManager.Init.GetLobbyPos(newId).rotation;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameStateChanged(GameManager.Init.gameState);
        GameManager.Init.GetId(this);
    }

    public void LobbySet()
    {
        graphics.UpdateGraphics(false);
        goldPlayerController.canMove = false;
        cameraObject.SetActive(false);
        goldPlayerAnimator.enabled = isLocalPlayer;
        gunObject.SetActive(false);
    }

    public void GameSet()
    {
        graphics.UpdateGraphics(isLocalPlayer);
        goldPlayerController.canMove = isLocalPlayer;
        cameraObject.SetActive(isLocalPlayer);
        goldPlayerAnimator.enabled = isLocalPlayer;
        gunObject.SetActive(isLocalPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (GameManager.Init.gameState==GameManager.GameState.Lobby)
        {
            return;
        }

        gunAnim.SetBool("Run",ch.velocity.magnitude>2);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire();
            gunAnim.SetTrigger("Fire");
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            if (lastFire+fireCooldown<Time.time)
            {
                lastFire = Time.time;
                Fire();
                gunAnim.SetTrigger("Fire");
            }
        }
    }

    [Command]
    public void Fire()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit))
        {
            if (hit.transform.GetComponent<IDamagable>()!=null)
            {
                hit.transform.GetComponent<IDamagable>().GetDamage(damage);
            }
            FireClient(hit.point);
        }
    }

    [ClientRpc]
    public void FireClient(Vector3 impactPos)
    {
        Vector3 exitPos = isLocalPlayer ? bulletExitPos.position : bulletExitPosOther.position;
        Instantiate(bulletObject, exitPos, Quaternion.identity).GetComponent<Rigidbody>().velocity =
            (impactPos-exitPos).normalized*100;
        Instantiate(impactEffect, impactPos, quaternion.identity);
    }

    [TargetRpc]
    public void GetDamage(NetworkConnectionToClient target,float damage)
    {
        cam.transform.DOShakeRotation(1, damage);
    }
}
