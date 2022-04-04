using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeed;
    bool canAttack = true;


    bool fDown;
    bool jDown;
    bool isDodge;


    float vaxis;
    float haxis;
    float firedelay;
    public Weapon weapon;


    Rigidbody rigid;
    Animator anim;
    Vector3 moveVec;
    Vector3 DodgeVec;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }




    void Update()
    {
        Move();
        Getinput();
        Attack();
        Turn();
        Dodge();
    }

    void Getinput()
    {
        haxis = Input.GetAxisRaw("Horizontal");
        vaxis = Input.GetAxisRaw("Vertical");
        fDown = Input.GetButtonDown("Fire1");
        jDown = Input.GetButtonDown("Jump");
    }

    void Move()
    {
        moveVec = new Vector3(haxis, 0, vaxis).normalized;

        if (isDodge)
        {
            moveVec = DodgeVec;
        }

        transform.position += moveVec * moveSpeed * Time.deltaTime;
        anim.SetBool("isRun", moveVec != Vector3.zero);
    }
    void Turn()
    {
        transform.LookAt(transform.position + moveVec);
    }
    void Dodge()
    {
        if (jDown && canAttack)
        {

            DodgeVec = moveVec;
            moveSpeed *= 2;
            isDodge = true;
            anim.SetTrigger("doDodge");

           Invoke("Dodgeout", 0.2f);
        }
    }
    void Dodgeout()
    {
        moveSpeed *= 0.5f;
        isDodge = false;
    }
    
    void Attack()
    {
        firedelay += Time.deltaTime;
        canAttack = 1.5 < firedelay;
        if (fDown && canAttack)
        {
            weapon.Use();
            anim.SetTrigger("doAttack1");
            firedelay = 0;
        }
    }

    void FrezzeRotaiton()
    {
        rigid.angularVelocity = Vector3.zero;
    }

    void FixedUpdate()
    {
        FrezzeRotaiton();
    }


}

