using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWaterController : BaseController
{
    public enum PlayerType
    {
        Mushroom,
        Slime
    }
    bool startFlipX;

    public PlayerType playerName;

    [SerializeField]
    private bool isJumping = false;

    protected override void Start()
    {
        base.Start();
        startFlipX = characterRenderer.flipX;
    }

    protected override void Update()
    {
        HandleAction();
    }
    protected override void HandleAction()
    {
        float horiziontal = 0;

        if (playerName == PlayerType.Mushroom)
        {
            horiziontal = Input.GetAxisRaw("Arrow Horizontal");
        }
        else if(playerName == PlayerType.Slime)
        {
            horiziontal = Input.GetAxisRaw("Key Horizontal");
        }

        movementDirection = new Vector2(horiziontal, 0).normalized;

        Jump();

    }

    protected override void Movment(Vector2 direction)
    {
        direction = direction * statHandler.Speed;
        var v = _rigidbody.velocity;
        v.x = direction.x;
        _rigidbody.velocity = v;
        animationHandler.Move(direction);

        if(direction.x != 0)
        {
            bool isLeft = direction.x < 0 ? !startFlipX : startFlipX;

            characterRenderer.flipX = isLeft;
        }
        

        // ¶³¾îÁö´ÂÁö ¹Ù´Ú¿¡ ´ê¾Ò´ÂÁö È®ÀÎ
        if(_rigidbody.velocity.y < 0)
        {
            Debug.DrawRay(transform.position, Vector2.down * 0.7f, Color.green);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.7f, LayerMask.GetMask("Ground"));
            if (hit.collider != null)
            {
                Debug.Log("¹Ù´Ú¿¡ ´êÀ½");
                // ¹Ù´Ú¿¡ ´ê¾ÒÀ» ¶§
                isJumping = false;
            }
            else
            {
                Debug.Log("¹Ù´Ú¿¡ ´êÁö ¾ÊÀ½");
                // ¶³¾îÁö´Â Áß
                isJumping = true;
            }
        }
    }

    private void Jump()
    {
        if(!isJumping)
        {
            if (playerName == PlayerType.Mushroom)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    var v = _rigidbody.velocity;
                    v.y = 0;
                    _rigidbody.velocity = v;
                    _rigidbody.AddForce(Vector2.up * statHandler.JumpForce, ForceMode2D.Impulse);
                    isJumping = true;
                }
            }
            else if (playerName == PlayerType.Slime)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    var v = _rigidbody.velocity;
                    v.y = 0;
                    _rigidbody.velocity = v;
                    _rigidbody.AddForce(Vector2.up * statHandler.JumpForce, ForceMode2D.Impulse);
                    isJumping = true;
                }
            }
        }
    }

    public void Die()
    {
        
    }

} 
