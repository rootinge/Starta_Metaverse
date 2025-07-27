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
        

        // ���������� �ٴڿ� ��Ҵ��� Ȯ��
        if(_rigidbody.velocity.y < 0)
        {
            Debug.DrawRay(transform.position, Vector2.down * 0.7f, Color.green);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.7f, LayerMask.GetMask("Ground"));
            if (hit.collider != null)
            {
                Debug.Log("�ٴڿ� ����");
                // �ٴڿ� ����� ��
                isJumping = false;
            }
            else
            {
                Debug.Log("�ٴڿ� ���� ����");
                // �������� ��
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

        // �ִϸ��̼� ���� ó��
        animationHandler.Death();

        DeleteMotion();
        

        // ��� �÷��̾��� Death �޼ҵ� ȣ��
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        {
            go.GetComponent<FireWaterController>().DeleteMotion();
        }
        Destroy(gameObject, .6f); // 1�� �Ŀ� ������Ʈ ����
    }

    private void DeleteMotion()
    {
        // ������ ����
        _rigidbody.velocity = Vector2.zero;
        // �߷� ����
        _rigidbody.bodyType = RigidbodyType2D.Static;

        // ���� ���� ����, Ű�Է� ����
        isDead = true;
    }

} 
