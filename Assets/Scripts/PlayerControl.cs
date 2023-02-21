using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

using UnityEngine.SceneManagement;

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

    Animator myAnim;

    //New

    bool SecondJump = false;

    public float jumpTimes = 2f;

    public float score = 0;

    bool GameWin = false;

    bool GameLose = false;

    bool BeforeGame = true;

    bool InGame = false;

    //Sound Effect

    public AudioSource mySource;

    public AudioClip jumpClip;

    public AudioClip collectItem;

    //Text

    public GameObject Instruction;

    public GameObject Win;

    public GameObject Lose;

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
            mySource.PlayOneShot(jumpClip);
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

        if (Input.GetButtonDown("Jump") && BeforeGame)
        {
            Instruction.SetActive(false);
            InGame = true;
            BeforeGame = false;
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

        //Notice that the score will not be saved, try to solve it with game manager
        if (score >= 2)
        {
            GameWin = true;
        }

        if (GameWin)
        {
            Win.SetActive(true);
        }
        else if (GameLose)
        {
            Lose.SetActive(true);
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

    private void OnCollisionEnter2D(Collision2D somecollision)
    {
        Debug.Log(somecollision.gameObject.name);

        if (somecollision.gameObject.name == "obj_gate")
        {
            SceneManager.LoadScene(1);
        }

        if (somecollision.gameObject.name == "obj_fruit")
        {
            mySource.PlayOneShot(collectItem);
            Destroy(somecollision.gameObject);
            score++;
        }

        if (somecollision.gameObject.name == "obj_bound")
        {
            GameLose = true;
        }

    }
}
