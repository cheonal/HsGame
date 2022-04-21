using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ccamera : MonoBehaviour
{
    public Transform player;


    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, 4.5f, player.transform.position.z);
    }
}
