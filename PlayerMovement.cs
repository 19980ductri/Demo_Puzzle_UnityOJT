using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Vector2 newVelocity;

    public PlayerDataBinding playerDataBinding;
    public PlayerInput playerInput;
    public Transform transPlayer;
    public Rigidbody2D rb;

    public float speed;
    public float jumpForce;


    public Transform groundCheckPos;
    public float groundCheckRad;
    public LayerMask Ground;
    public bool isGrounded;
    
    private bool isRight;

    public float hp = 10;
    public CapsuleCollider2D capsuleCollider;
    public Vector2 colliderSize;
    public Vector2 checkPos;

    [SerializeField]
    private float SlopeCheckDistance;

    private float slopeDownAngle;
    private float slopeDownAngleOld;
    private Vector2 slopeNormalPerp;
    private float slopeSideAngle;

    [SerializeField]
    private bool isOnSlope;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerDataBinding = GetComponent<PlayerDataBinding>();
        transPlayer = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        colliderSize = capsuleCollider.size;

    }


    void Update()
    {
        Move();
        Flip();
    }

    private void FixedUpdate()
    {
        GroundCheck();

        SlopeCheck();

    }

    public void Move()
    {
        
        if (isGrounded && !isOnSlope)
        {
            newVelocity.Set(speed * playerInput.dir.x, 0f);
            rb.velocity = newVelocity;
        }
        else if (isGrounded && isOnSlope)
        {
            newVelocity.Set(speed * slopeNormalPerp.x * -playerInput.dir.x, speed * slopeNormalPerp.y * -playerInput.dir.x);    
            rb.velocity = newVelocity;
        }
        else if (!isGrounded)
        {
            newVelocity.Set(speed * playerInput.dir.x, rb.velocity.y);
            rb.velocity = newVelocity;
        }
        
    }

    public void Jump()
    {
        if (!isGrounded)
        {
            return;
        }

        rb.AddForce(new Vector2(transform.position.x, jumpForce));
    }


    public void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRad, Ground);
        if (isGrounded == true)
        {
            playerDataBinding.Idle = true;
            playerDataBinding.Jump = false;
        }
        else
        {
            playerDataBinding.Idle = false;
            playerDataBinding.Jump = true;
        }

    }

    public void SlopeCheck()
    {
        //Debug.Log("ColliderSize.y: " + colliderSize.y);
        //checkPos = transform.position - new Vector3(0f, colliderSize.y / 12);
        
        SlopeCheckVertical(groundCheckPos.position);
        SlopeCheckHorizontal(groundCheckPos.position);
    }
    public void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, SlopeCheckDistance, Ground);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos,-transform.right, SlopeCheckDistance, Ground);

        if (slopeHitFront)
        {
            isOnSlope = true;
            slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
        }
        else if (slopeHitBack)
        {
            isOnSlope = true;
            slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            slopeSideAngle = 0.0f;
            isOnSlope = false;
        }
    }

    public void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, SlopeCheckDistance, Ground);
        if (hit)
        {
            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;
            
            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);
            if (slopeDownAngle != slopeDownAngleOld)
            {
                isOnSlope = true;
                slopeDownAngleOld = slopeDownAngle;
            }
            Debug.DrawRay(hit.point, slopeNormalPerp, Color.red);
            Debug.DrawRay(hit.point, hit.normal, Color.green);
        }
    }

    public void Flip()
    {
        if (playerInput.dir.x > 0 && isRight)
        {
            isRight = !isRight;
            Vector2 scale = transform.localScale;
            scale.x = -scale.x;
            transform.localScale = scale;
        }
        else if (playerInput.dir.x < 0 && !isRight)
        {
            isRight = !isRight;
            Vector2 scale = transform.localScale;
            scale.x = -scale.x;
            transform.localScale = scale;
        }
        
    }

    public void OnDrawGizmos()
    {
        
    }

}
