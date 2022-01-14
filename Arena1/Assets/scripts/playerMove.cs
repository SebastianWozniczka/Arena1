using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMove : MonoBehaviour
{

    private Rigidbody2D playerRigidbody2D;
    private Animator animator;
    public Button left, right, up;
    public SpriteRenderer spriteRenderer;
    public Sprite sprite1,sprite2;
    public PolygonCollider2D polygonCollider2D;
    public Transform transform;
   


    private bool isLeft = false, isRight = false, isUp = false;

    private float moveSpeed = 2.0f;
    private float launchSpeed = 6.0f;
    private bool isGrounded;
    private bool facingDirection;
    private float timer,timer2;
    public float inputX;


    void Start()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        left.onClick.AddListener(moveLeft);
        right.onClick.AddListener(moveRight);
        up.onClick.AddListener(jump);
        animator.SetTrigger("run");
        

        timer = 0;
        timer2 = 0;
        

    }

  
    void Update()
    {
       

        playerRotate();


        if (isLeft)
        {
            playerRigidbody2D.AddForce(Vector2.left * moveSpeed, ForceMode2D.Impulse);
            isLeft = false;
        }

        if (isRight)
        {
            playerRigidbody2D.AddForce(Vector2.right * moveSpeed, ForceMode2D.Impulse);
            isRight = false;
        }

        if(isUp)
        {
            if(isGrounded)
            playerRigidbody2D.AddForce(new Vector2(playerRigidbody2D.velocity.x, launchSpeed), ForceMode2D.Impulse);
            isUp = false;
        }

        if (playerRigidbody2D.velocity.x == 0)
        {

          
            timer += Time.deltaTime;
            if (timer > 2)
            {
                NewPoints();
                spriteRenderer.sprite = sprite1;

                timer = 0;
            }
        }
        else
        {
            timer = 0;
           
 
        }



        
        Debug.Log(polygonCollider2D.points.GetValue(0));
    }

    void playerRotate()
    {
      
        inputX = playerRigidbody2D.velocity.x;

        if (inputX < 0 && !facingDirection)
        {
            timer2 += Time.deltaTime;
            if (timer2 > 0.1f)
            {
                playerRigidbody2D.transform.Rotate(0, 180, 0);
                facingDirection = true;
                timer2 = 0;
            }
        }
        else if (inputX > 0 && facingDirection)
        {
            timer2 += Time.deltaTime;
            if (timer2 > 0.1f)
            {
                playerRigidbody2D.transform.Rotate(0, 180, 0);
                facingDirection = false;
                timer2 = 0;
            }
        }
      





    }

    void NewPoints()
    {


        Vector2[] vector2 = new Vector2[10];
        vector2[0] = new Vector2(0.36f, 0.4f);
        vector2[1] = new Vector2(0.26f, 0.21f);
        vector2[2] = new Vector2(0.09f, 0.12f);
        vector2[3] = new Vector2(-0.12f, -0.12f);
        vector2[4] = new Vector2(-0.33f, 0.25f);
        vector2[5] = new Vector2(-0.4f, 0.46f);
        vector2[6] = new Vector2(-4, 0.5f);
        vector2[7] = new Vector2(-4, -0.5f);
        vector2[8] = new Vector2(4, -0.5f);
        vector2[9] = new Vector2(4, 0.5f);

        polygonCollider2D.SetPath(0, vector2);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    private void moveLeft()
    {
        isLeft = true;
    }

    private void moveRight()
    {
        isRight = true;
    }

    private void jump()
    {
        isUp = true;
    }
}
