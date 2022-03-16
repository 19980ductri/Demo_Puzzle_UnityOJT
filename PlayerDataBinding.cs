using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataBinding : MonoBehaviour
{
    [HideInInspector]
    public ComboController combo;

    public Animator animator;

    private float speed = 0;

    public float Speed 
    { 
        get => speed; 
        set => speed = value; 
    }

    private bool dead;
    public bool Dead 
    { 
    
        get => dead;
        set
        {
            dead = value;
            if (dead == true)
            {
                animator.SetTrigger("Dead");                     
            }
        }
    }

    private bool isRun;
    public bool IsRun 
    {
        get 
        {
            return isRun;
        } 

        set
        {
            isRun = value;
            if (isRun == true)
            {
                animator.SetBool("isRun",true);
            }
            else
            {
                animator.SetBool("isRun", false);
            }
        } 
    }


    private bool idle;
    public bool Idle { get => idle; set => idle = value; }

    private bool jump;
    public bool Jump 
    {
        get => jump;
        set
        {
            jump = value;
            if (jump == true)
            {
                animator.SetBool("Jump", true);
            }
            else
            {
                animator.SetBool("Jump", false);
            }
        }
    }

    private bool airAttack;

    public bool AirAttack 
    { 
        get => airAttack;
        set
        {
            airAttack = value;
            if (airAttack == true)
            {
                animator.SetTrigger("AirAttack");
            }
        }

    }

    

    private bool hurt;
    public bool Hurt 
    { 
        get => hurt; 
        set
        {
            hurt = value;
            if (hurt == true)
            {
                animator.SetTrigger("Hurt");
                combo.comboCount = 0;
                combo.canAtk = false;
            }

        }
    }

    public void Start()
    {
        animator = GetComponent<Animator>();
        combo = GetComponent<ComboController>();
    }




}
