using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 dir;
    public PlayerDataBinding playerDataBiding;
    public PlayerMovement playerMovement;
    public ComboController combo;

    void Start()
    {
        playerDataBiding = GetComponent<PlayerDataBinding>();
        playerMovement = GetComponent<PlayerMovement>();
        combo = GetComponent<ComboController>();
    }

    // Update is called once per frame  
    void Update()
    {
        dir.x = Input.GetAxisRaw("Horizontal");

        if (dir.x != 0)
        {
            playerDataBiding.IsRun = true;
        }
        else
        {
            playerDataBiding.IsRun = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerMovement.Jump();
            combo.canAtk = false;
            combo.comboCount = 0;
        }


        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    playerDataBiding.Hurt = true;
        //}
        
        
    }
}
