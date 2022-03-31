using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rigid;
    Animator anim;
    bool canAttack;
    bool fDown;
    float firedelay;
    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }




    void Update()
    {
        Getinput();
        Attack();
    }

    void Getinput()
    {
        fDown = Input.GetButton("Fire1");
    }

    void Attack()
    {


            if (fDown && canAttack)
        {
            firedelay += Time.deltaTime;
            if (firedelay > 2)
            {
                canAttack = true;
            }
            anim.SetTrigger("doAttack1");
            anim.SetTrigger("doAttack2");
            anim.SetTrigger("doAttack3");
        }
    }
}
