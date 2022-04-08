using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        if (Input.GetKey("q"))
        {
            target.Rotate(new Vector3(0, -1, 0));
        }
        if (Input.GetKey("e"))
        {
            target.Rotate(new Vector3(0, 1, 0));
        }
    }
    void LateUpdate()
    {
        if (Input.anyKeyDown && Input.GetKey("q"))
        {
            return;
        }
        if (Input.anyKeyDown && Input.GetKey("e"))
        {
            return;
        }
        if (Input.anyKeyDown)
        {
            target.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

}
