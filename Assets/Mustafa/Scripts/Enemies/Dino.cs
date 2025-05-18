using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Dino : Enemy
{
    public override void Attack()
    {
        base.Attack();
        animator.SetTrigger("Attack");
        target.transform.GetComponent<Player>()?.GetDamage(target.transform.GetComponent<NetworkIdentity>().connectionToClient,damage);
    }
}
