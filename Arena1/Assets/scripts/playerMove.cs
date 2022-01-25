using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{

    private Rigidbody2D playerRigidbody2D;
    private Animator animator;
    public Button left, right, up;
    public SpriteRenderer spriteRenderer;
    public Sprite sprite1,sprite2,sprite3;
    public PolygonCollider2D polygonCollider2D;
    public new Transform transform;
    public AudioSource audioSource1;

   // GameObject a;



    public float posX;
    private float y;
    private bool isLeft = false, isRight = false, isUp = false;

    private readonly float moveSpeed = 2.0f;
    private readonly float launchSpeed = 6.0f;
    private bool isGrounded;
    private bool facingDirection;
    private float timer,timer2,timer1b;
    public float inputX;


    void Start()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
 
        left.onClick.AddListener(MoveLeft);
        right.onClick.AddListener(MoveRight);
        up.onClick.AddListener(Jump);
        animator.SetTrigger("stand");
       
        timer = 0;
        timer2 = 0;
        

    }

  
    void Update()
    {

        
       
       
        posX=playerRigidbody2D.position.x;

        if (posX > 10)
        {
            playerRigidbody2D.transform.position = new Vector2(-10, playerRigidbody2D.position.y);
        }

        if (posX < -10)
        {
            playerRigidbody2D.transform.position = new Vector2(10, playerRigidbody2D.position.y);
        }

        PlayerRotate();

        if (!isGrounded)
        {
            animator.SetTrigger("jump");
        }
        
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

        if (playerRigidbody2D.velocity.x < 1 && playerRigidbody2D.velocity.x > -1 && isGrounded)
        {

            animator.SetTrigger("stand");
            timer += Time.deltaTime;
            if (timer > 1)
            { 
                
                y = -0.01f;
                NewPoints(y);
               
                transform.position = new Vector2(posX, -3.6f);
                spriteRenderer.sprite = sprite1;
                audioSource1.Play();
  
            }

            if(timer > 2)
            {
                //a = new GameObject("start");

                y = -0.1f;
                NewPoints(y);
                spriteRenderer.sprite = sprite2;
                if (facingDirection)
                {
                    spriteRenderer.sprite = sprite3;
                    polygonCollider2D.enabled = false;
                }
            }

           
              

          


           




        }

        else
        {
            audioSource1.Pause();
          
            timer = 0;
            if(isGrounded)
            animator.SetTrigger("run");


        }



        
        Debug.Log(polygonCollider2D.points.GetValue(0));
    }

    void PlayerRotate()
    {
      
        inputX = playerRigidbody2D.velocity.x;

        if (inputX < 0 && !facingDirection)
        {
            timer2 += Time.deltaTime;
            if (timer2 > 0.2f)
            {
                playerRigidbody2D.transform.Rotate(0, 180, 0);
                facingDirection = true;
                timer2 = 0;
            }
        }
        else if (inputX > 0 && facingDirection)
        {
            timer1b += Time.deltaTime;
            if (timer1b > 0.2f)
            {
                playerRigidbody2D.transform.Rotate(0, 180, 0);
                facingDirection = false;
                timer1b = 0;
            }
        }
      





    }

    void NewPoints(float y)
    {


        Vector2[] vector2 = new Vector2[5];
        vector2[0] = new Vector2(posX+0.14f, y);
        vector2[1] = new Vector2(-4, 0.5f);
        vector2[2] = new Vector2(-4, -0.5f);
        vector2[3] = new Vector2(4, -0.5f);
        vector2[4] = new Vector2(4, 0.5f);

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

    private void MoveLeft()
    {
        isLeft = true;
    }

    private void MoveRight()
    {
        isRight = true;
    }

    private void Jump()
    {
        isUp = true;
    }
}
