using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PlatformGoDown : MonoBehaviour
{
    private float yPos;

    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        yPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        Vector3 newPos = transform.position;

        newPos.y = yPos;

        transform.position = newPos;
    }

    private void OnCollisionEnter2D(Collision2D somecollision)
    {
        if (somecollision.gameObject.name == "obj_player")
        {
            yPos -= Time.deltaTime * speed;
        }
    }
}