using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Unity.Mathematics;
using UnityEngine;

public class EffectManager : NetworkBehaviour
{
    public static EffectManager Init;

    public GameObject redEnemyExpEffect,flyEnemyExpEffect;
    public GameObject[] puffEffects;
    private void Awake()
    {
        Init = this;
    }

    [ClientRpc]
    public void Puff(Vector3 pos,int puffId)
    {
        Instantiate(puffEffects[puffId], pos, quaternion.identity);
    }
    
    [ClientRpc]
    public void RedEnemyExplosion(Vector3 pos)
    {
        Instantiate(redEnemyExpEffect, pos, quaternion.identity);
    }
    
    [ClientRpc]
    public void FlyEnemyExplosion(Vector3 pos)
    {
        Instantiate(flyEnemyExpEffect, pos, quaternion.identity);
    }
}
