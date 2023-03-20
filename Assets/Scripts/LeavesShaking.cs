using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeavesShaking : MonoBehaviour
{
    public float xRot;

    public bool isAdded = true;

    public float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        xRot = transform.localRotation.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAdded)
        {
            xRot -= Time.deltaTime * speed;
        }
        else
        {
            xRot += Time.deltaTime * speed;
        }

        Vector3 newRot = transform.position;

        newRot.z = xRot;

        transform.position = newRot;
    }
}
