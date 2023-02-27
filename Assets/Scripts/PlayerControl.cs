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

    public float jumpLimit = 1f;

    bool Jump = false;

    Animator myAnim;

    //New

    bool SecondJump = false;

    public float jumpTimes = 2f;

    public float score = 0;

    bool GameWin = false;

    bool GameLose = false;

    bool BeforeGame = false;

    bool InGame = true;

    //Sound Effect

    public AudioSource mySource;

    public AudioClip jumpClip;

    public AudioClip collectItem;

    //Text

    public GameObject Instruction;

    public GameObject Win;

    public GameObject Lose;

    //NewNew!

    SpriteRenderer myRend;

    public float sceneNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //horizontalMove = Input.GetAxis("Horizontal");
        //Debug.Log(horizontalMove);

        if (sceneNumber == 1)
        {
            InGame = true;
            BeforeGame = false;
        }

        if (Input.GetButtonDown("Jump") && BeforeGame)
        {
            Instruction.SetActive(false);
            InGame = true;
            BeforeGame = false;
        }

        if (InGame)
        {
            horizontalMove = Input.GetAxis("Horizontal");
            Debug.Log(horizontalMove);

            if (Input.GetButtonDown("Jump"))
            {
                //mySource.PlayOneShot(jumpClip);
                jumpTimes--;

                if (Grounded)
                {
                    Jump = true;
                    mySource.PlayOneShot(jumpClip);
                    Debug.Log("First Jump");
                }

                //New!
                if (!Grounded && jumpTimes >= 0)
                {
                    Debug.Log("Second Jump");
                    mySource.PlayOneShot(jumpClip);
                    SecondJump = true;
                }
            }
        }

        if (Grounded)
        {
            jumpTimes = 1;
        }

        if (jumpTimes < 0)
        {
            jumpTimes = 0;
        }

        if (horizontalMove > 0.2f)
        {
            myAnim.SetBool("Walking", true);
            myRend.flipX = false;
        }
        else if (horizontalMove < -0.2f)
        {
            myAnim.SetBool("Walking", true);
            myRend.flipX = true;
        }
        else
        {
            myAnim.SetBool("Walking", false);
        }

        if (GameWin)
        {
            Win.SetActive(true);
        }
        else if (GameLose)
        {
            Lose.SetActive(true);
        }

        if (GameLose && Input.GetButtonDown("Jump") && !GameWin)
        {
            SceneManager.LoadScene(1);
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
            Debug.Log("Grounded");
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

        if (somecollision.gameObject.name == "obj_gate" && score >=1)
        {
            SceneManager.LoadScene(2);
        }

        if (somecollision.gameObject.name == "obj_final" && score >= 3)
        {
            GameWin = true;
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
            //jumpTimes = 0;
        }

    }
}
