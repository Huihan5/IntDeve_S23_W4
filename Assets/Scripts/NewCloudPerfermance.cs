using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCloudPerfermance : MonoBehaviour
{

    public float xPos;

    public bool isAdded = true;

    public float speed = 2f;

    public float leftBound = -2f;

    public float rightBound = 27.5f;

    public float xLeftSpeed;

    public float xRightSpeed;

    // Start is called before the first frame update
    void Start()
    {
        xPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (xPos < leftBound && isAdded)
        {
            isAdded = false;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        if (xPos > rightBound && !isAdded)
        {
            isAdded = true;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        if (isAdded)
        {
            xPos -= Time.deltaTime * speed;
            //transform.localScale.x = Vector3 
        }
        else
        {
            xPos += Time.deltaTime * speed;
        }

        Vector3 newPos = transform.position;

        newPos.x = xPos;

        transform.position = newPos;
    }
}
