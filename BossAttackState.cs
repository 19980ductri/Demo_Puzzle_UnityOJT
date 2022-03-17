using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BossAttackState : FSMState
{
    public float timer = 1;
    [NonSerialized]

    public BossSystem bossSystem;
    

    public override void OnEnter()
    {
        bossSystem.Flip();

        bossSystem.bossDataBinding.Attack = true;

        base.OnEnter();
    }

    public override void OnUpdate()
    {
        Debug.Log("attack");

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            bossSystem.GotoState(bossSystem.bossIdle);
        }
        
        base.OnUpdate();
    }

    public override void OnExit()
    {
        timer = 1;
        base.OnExit();
    }

    

}