using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMove : MonoBehaviour
{

    private Rigidbody2D playerRigidbody2D;
    public Button left, right, up;
    public PolygonCollider2D polygonCollider2D;

    private bool isLeft = false, isRight = false, isUp = false;

    private float moveSpeed = 2.0f;
    private float launchSpeed = 6.0f;
    private bool isGrounded;


    void Start()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();

        left.onClick.AddListener(moveLeft);
        right.onClick.AddListener(moveRight);
        up.onClick.AddListener(jump);
    }

  
    void Update()
    {
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


        Vector2[] vector2=new Vector2[5];
        vector2[0] = new Vector2(- 0.02255917f, 0.05214572f);
        vector2[1] = new Vector2(-4, 0.5f);
        vector2[2] = new Vector2(-4, -0.5f);
        vector2[3] = new Vector2(4, -0.5f);
        vector2[4] = new Vector2(4, 0.5f);

        polygonCollider2D.SetPath(0,vector2);
        Debug.Log(polygonCollider2D.points.GetValue(0));
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
