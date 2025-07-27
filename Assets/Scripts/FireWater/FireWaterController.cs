using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
    Mushroom,
    Slime
}

public class FireWaterController : BaseController
{


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
        

        // 떨어지는지 바닥에 닿았는지 확인
        if(_rigidbody.velocity.y < 0)
        {
            Debug.DrawRay(transform.position, Vector2.down * 0.7f, Color.green);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.7f, LayerMask.GetMask("Ground"));
            if (hit.collider != null)
            {
                Debug.Log("바닥에 닿음");
                // 바닥에 닿았을 때
                isJumping = false;
            }
            else
            {
                Debug.Log("바닥에 닿지 않음");
                // 떨어지는 중
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
    public override void Death()
    {
        if (isDead) return;

        // 애니메이션 죽음 처리
        animationHandler.Death();

        DeleteMotion();
        

        // 모든 플레이어의 Death 메소드 호출
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        {
            go.GetComponent<FireWaterController>().DeleteMotion();
        }
        Destroy(gameObject, .6f); // 1초 후에 오브젝트 삭제
    }

    private void DeleteMotion()
    {
        // 움직임 삭제
        _rigidbody.velocity = Vector2.zero;
        // 중력 삭제
        _rigidbody.bodyType = RigidbodyType2D.Static;

        // 죽음 상태 설정, 키입력 삭제
        isDead = true;
    }

} 
