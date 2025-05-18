using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class FlyEnemy : NetworkBehaviour,IDamagable
{

    public float seeDistance;
    public float attackDistance;
    public float speed;
    public float damage;
    public Player target;

    private void Start()
    {
        StartCoroutine(SetTarget());
    }

    private void Update()
    {
        if (!isServer)
        {
            return;
        }

        if (target==null)
        {
            return;
        }

        RaycastHit hit;
        Ray ray = new Ray(transform.position, (target.transform.position+Vector3.up/2 - transform.position).normalized);
        if (Physics.Raycast(ray,out hit))
        {
            if (hit.transform.GetComponent<Player>())
            {
                transform.position += (target.transform.position - transform.position).normalized*speed*Time.deltaTime;
            }

            if (Vector3.Distance(transform.position,hit.transform.position)<=attackDistance)
            {
                hit.transform.GetComponent<Player>().GetDamage(hit.transform.GetComponent<NetworkIdentity>().connectionToClient,damage);
                EffectManager.Init.FlyEnemyExplosion(transform.position);
                Dead();
            }
        }
    }
    
    public IEnumerator SetTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            
            if (GameManager.Init.gameState==GameManager.GameState.Game)
            {
                if (target==null)
                {
                    if (Vector3.Distance(GetNearest().transform.position,transform.position)<seeDistance)
                    {
                        target = GetNearest();
                    }
                }
                else
                {
                    target = GetNearest();
                }
            }
        }
    }
    
    public Player GetNearest()
    {
        List<Player> players = GameManager.Init.Players();
        Player nearest = null;
        float nearestDistance=float.MaxValue;
        foreach (var player in players)
        {
            if (Vector3.Distance(player.transform.position,transform.position)<nearestDistance)
            {
                nearest = player;
                nearestDistance = Vector3.Distance(player.transform.position, transform.position);
            }
        }

        return nearest;
    }

    public void GetDamage(float damage)
    {
        Dead();
    }

    public void Dead()
    {
        NetworkServer.Destroy(gameObject);
    }
}
