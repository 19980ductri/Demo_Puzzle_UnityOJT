using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboController : MonoBehaviour
{
    [HideInInspector]
    public PlayerMovement playerMovement;
    [HideInInspector]
    public PlayerDataBinding playerDataBinding;
    [HideInInspector]
    public Animator ani;


    public int comboCount;
    public bool canAtk;
    public int playerDmg = 2 ;


    public Transform atkPos;
    public float atkRad;
    public LayerMask Enemy;

    public BossSystem boss;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerDataBinding = GetComponent<PlayerDataBinding>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && playerMovement.isGrounded == true)
        {
            Combo();
        }

        if (playerMovement.isGrounded == false)
        {
            canAtk = false;
        }

    }
    public void StartCombo()
    {
        canAtk = false;
        if (comboCount < (int)comboAni.COUNT)
        {
            comboCount++;
        }
        
    }
    public void FinishCombo()
    {
        canAtk = false;
        comboCount = 0;
    }

    public void Combo()
    {
        if (!canAtk)
        {
            canAtk = true;
            for (comboAni i = (comboAni)comboCount; i < comboAni.COUNT; i++)
            {
                ani.SetTrigger("" + i);
                return;
            }
        }
    }

 

    public void Hit()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(atkPos.position, atkRad, Enemy);

        foreach (Collider2D hitEnemy in hit)
        {
            boss.TakeDmg();
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(atkPos.position, atkRad);
    }

}

public enum comboAni //khai bao them animation combo o day, giong dialog manager
{   
    Attack1,
    Attack2,
    COUNT
}


