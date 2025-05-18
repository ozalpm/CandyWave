using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : NetworkBehaviour,IDamagable
{
    public Animator animator;

    public float seeDistance;
    public float attackCooldown;
    public float attackDistance;
    public float speed;
    public float health;
    public float damage;

    public NavMeshAgent ai;
    public Player target;
    
    void Start()
    {
        if (!isServer)
        {
            return;
        }
        StartCoroutine(SetTarget());
        ai.stoppingDistance = attackDistance;
        ai.speed = speed;
    }

    public float lastAttack;
    
    void Update()
    {
        if (!isServer)
        {
            return;
        }

        if (target!=null)
        {
            ai.SetDestination(target.transform.position);
            animator.SetFloat("Speed",ai.velocity.magnitude);
            if (Vector3.Distance(target.transform.position,transform.position)<=attackDistance)
            {
                if (lastAttack+attackCooldown<Time.time)
                {
                    lastAttack = Time.time;
                    Attack();
                }
            }
            
        }
        else
        {
            
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
                        RaycastHit hit;
                        Ray ray = new Ray(transform.position, (GetNearest().transform.position+Vector3.up/2 - transform.position).normalized);
                        if (Physics.Raycast(ray,out hit))
                        {
                            if (hit.transform.GetComponent<Player>())
                            {
                                target = GetNearest();
                            }
                        }
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
        Debug.Log(players.Count);
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

    public int puffEffectId;
    public void GetDamage(float damage)
    {
        health -= damage;
        if (health<=0)
        {
            EffectManager.Init.Puff(transform.position,puffEffectId);
            Dead();
        }
    }

    public void Dead()
    {
        NetworkServer.Destroy(gameObject);
    }

    public virtual void Attack()
    {
        
    }
}
