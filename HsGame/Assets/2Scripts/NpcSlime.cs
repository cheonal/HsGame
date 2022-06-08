using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSlime : MonoBehaviour
{
   // bool isTalk;
    void NpcStart()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag== "Player")
        {
            //isTalk = true;
        }
    }
}
