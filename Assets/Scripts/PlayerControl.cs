using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    float horizontalMove;

    public float speed;

    Rigidbody2D myBody;

    bool Grounded = false;

    public float castDist = 0.2f;

    public float gravityScale = 5f;

    public float gravityFall = 40f;

    public float jumpLimit = 2f;

    bool Jump = false;

    bool SecondJump = false;

    public float jumpTimes = 2f;

    Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        Debug.Log(horizontalMove);

        if (Input.GetButtonDown("Jump") && Grounded)
        {
            Jump = true;
        }

        //New!
        if (Input.GetButtonDown("Jump") && !Grounded && jumpTimes > 0)
        {
            SecondJump = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpTimes --;
        }

        if (Grounded)
        {
            jumpTimes = 2;
        }

        if (jumpTimes < 0)
        {
            jumpTimes = 0;
        }

        if (horizontalMove > 0.2f || horizontalMove < -0.2f)
        {
            myAnim.SetBool("Walking", true);
        }
        else
        {
            myAnim.SetBool("Walking", false);
        }
    }

    void FixedUpdate()
    {
        float moveSpeed = horizontalMove * speed;
        myBody.velocity = new Vector3(moveSpeed, myBody.velocity.y, 0);

        if (Jump)
        {
            myBody.AddForce(Vector2.up * jumpLimit, ForceMode2D.Impulse);
            Jump = false;
        }

        //New!
        if (SecondJump)
        {
            myBody.AddForce(Vector2.up * jumpLimit, ForceMode2D.Impulse);
            Jump = false;
            SecondJump = false;
        }

        if(myBody.velocity.y > 0)
        {
            myBody.gravityScale = gravityScale;
        }
        else if(myBody.velocity.y < 0)
        {
            myBody.gravityScale = gravityFall;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, castDist);

        Debug.DrawRay(transform.position, Vector2.down * castDist, Color.red);

        if(hit.collider != null && hit.transform.name == "obj_ground")
        {
            Grounded = true;
            //Debug.Log("Grounded");
        }
        else
        {
            Grounded = false;
        }

        myBody.velocity = new Vector3(moveSpeed, myBody.velocity.y, 0);
    }
}
