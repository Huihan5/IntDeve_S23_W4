using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehavior : MonoBehaviour
{

    public float cloudXPos;

    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        cloudXPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = transform.position;
        
        //if(cloudXPos < 35)
        //{
            //cloudXPos = Time.deltaTime*1 + cloudXPos;
        //}
        //else if(cloudXPos > 0)
        //{
            //cloudXPos = -Time.deltaTime*1 + cloudXPos;
        //}

        if(newPos.x < 35)
        {
            newPos.x += Time.deltaTime * speed;
        }

        Vector3 position = transform.position;

        transform.position = newPos;
    }
}
