using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BossIdleState : FSMState
{
    public float timer = 2;
    [NonSerialized]

    public BossSystem bossSystem;
    private float rangeAttack = 3f;

    
    public override void OnEnter()
    {
        
        
        base.OnEnter();
    }

    public override void OnUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (bossSystem.transPlayer != null)
            {
                if (Vector2.Distance(bossSystem.transform.position, bossSystem.transPlayer.position) <= rangeAttack)
                {
                    bossSystem.GotoState(bossSystem.bossAttack);
                }
                else
                {

                    bossSystem.GotoState(bossSystem.bossWalk);

                }
            }
            else
            {
                OnEnter();
            }
        }

        base.OnUpdate();
    }


    public override void OnExit()
    {
        timer = 2;
        bossSystem.bossDataBinding.Idle = false;
        bossSystem.agent.maxSpeed = 3;
        base.OnExit();
    }
}
