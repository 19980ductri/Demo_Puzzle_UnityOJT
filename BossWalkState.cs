using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class BossWalkState : FSMState
{
    [NonSerialized]
    public BossSystem bossSystem;
    public float rangeAttack = 3f;



    public  override void OnEnter()
    {
        bossSystem.Flip();

        if (Vector2.Distance(bossSystem.transform.position, bossSystem.transPlayer.position) <= rangeAttack)
        {
            bossSystem.agent.SetDestination(bossSystem.transform.position);
            bossSystem.bossDataBinding.Walk = false;
            bossSystem.GotoState(bossSystem.bossAttack);
        }
        else
        {
            bossSystem.agent.SetDestination(new Vector2(bossSystem.transPlayer.position.x, bossSystem.transform.position.y));
            bossSystem.bossDataBinding.Walk = true;
            
        }
        base.OnEnter();
    }

    public override void OnUpdate()
    {
        Debug.Log("walk");
        if (Vector2.Distance(bossSystem.transform.position, bossSystem.transPlayer.position) <= rangeAttack)
        {
            bossSystem.agent.SetDestination(bossSystem.transform.position);
            bossSystem.bossDataBinding.Walk = false;
            bossSystem.GotoState(bossSystem.bossAttack);
        }
        else
        {
            bossSystem.agent.SetDestination(new Vector2(bossSystem.transPlayer.position.x, bossSystem.transform.position.y));
            bossSystem.bossDataBinding.Walk = true;
        }
        
        base.OnUpdate();
    }

    public override void OnExit()
    {
        

        base.OnExit();
    }

}
