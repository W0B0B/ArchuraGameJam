using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D),typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Veribles")]
    [SerializeField] float speed;   
    [SerializeField] float jumpForce;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float dashingPower = 24f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;
    

    bool isWallSliding;
    float wallSlidingSpeed=4;
    private bool canDash = true;
    private bool isDashing;


    bool isWallJumping;
    float wallJumpingDirection;
    float wallJumpingTime=0.2f;
    float wallJumpingCounter;
    float wallJumpingDuration=0.5f;
    [SerializeField] Vector2 wallJumpingPower=new Vector2(8,16);
    private Animator ani;
    private bool isFacingRight = true;

    Vector2 Dir;

    [Header("Layers")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer; 

    Collider2D _collider2D;
    Rigidbody2D _rigidbody2D;
    private void Start() {
        _collider2D=GetComponent<Collider2D>();
        _rigidbody2D=GetComponent<Rigidbody2D>();
        ani=GetComponent<Animator>();
    }
    private void Update() {
        ani.SetBool("isGround",IsGrounded());
        if (isDashing)
        {
            return;
        }
        Dir.x=Input.GetAxisRaw("Horizontal");
        
        if (Input.GetKeyDown(KeyCode.Space)&&IsGrounded())
        {
            Jump();
        }
        WallSlide();      
        WallJump();
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        } 
        if (!isWallJumping)
        {
            Flip();
        }
    }
    private void FixedUpdate() {
        if (isDashing)
        {
            return;
        }
        if (!isWallJumping)
        {
            _rigidbody2D.velocity=new Vector2(Dir.x*speed,_rigidbody2D.velocity.y);
            ani.SetFloat("isRunning",MathF.Abs(Dir.x));
        }
                   
    }


    
    void Jump()
    {
        ani.SetTrigger("Jump");
        _rigidbody2D.velocity=Vector2.up*jumpForce;
    }

    void WallSlide()
    {
        if (!IsGrounded()&&IsWalled()&&Dir.x!=0)
        {
            
            isWallSliding=true;
            _rigidbody2D.velocity=new Vector2(_rigidbody2D.velocity.x,Mathf.Clamp(_rigidbody2D.velocity.y,-wallSlidingSpeed,float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }
    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            
            isWallJumping = true;
            _rigidbody2D.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;
            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }   
    }
    
    private void StopWallJumping()
    {
        isWallJumping = false;
    }


    
    private void Flip()
    {
        if (isFacingRight && Dir.x < 0f || !isFacingRight && Dir.x > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }


    #region Dash Routine
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = _rigidbody2D.gravityScale;
        _rigidbody2D.gravityScale = 0f;
        _rigidbody2D.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        ani.SetTrigger("dash");
        yield return new WaitForSeconds(dashingTime);
        
        _rigidbody2D.gravityScale    = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    #endregion
    #region Checks 
    bool IsGrounded(){
        
        return Physics2D.OverlapCircle(groundCheck.position,0.2f,groundLayer);
    }
    bool IsWalled(){
        return Physics2D.OverlapCircle(wallCheck.position,0.2f, wallLayer);;         
    }
    #endregion
}
