using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class RedEnemy : Enemy
{ 
    public override void Attack()
    {
        base.Attack();
        EffectManager.Init.RedEnemyExplosion(transform.position);
        target.transform.GetComponent<Player>()?.GetDamage(target.transform.GetComponent<NetworkIdentity>().connectionToClient,damage);
        Dead();
    }
}
