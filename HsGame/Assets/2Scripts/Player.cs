using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeed;
    bool canAttack = true;

    bool isJump;
    bool fDown;
    bool jDown;
    bool isDodge;
    bool DodgeDown;



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
        Jump();
    }

    void Getinput()
    {
        haxis = Input.GetAxisRaw("Horizontal");
        vaxis = Input.GetAxisRaw("Vertical");
        fDown = Input.GetButtonDown("Fire1");
        jDown = Input.GetButtonDown("Jump");
        DodgeDown = Input.GetButtonDown("Dodge");

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

    void Jump()
    {
        if (jDown && !isJump && !isDodge)
        {
            rigid.AddForce(Vector3.up * 10,ForceMode.Impulse);
            isJump = true;
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
        }
    }
    void Turn() 
    {
        transform.LookAt(transform.position + moveVec);

    }
    void Dodge()
    {
        if (DodgeDown && !isJump)
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
        if (fDown && canAttack && !isJump ) 
        {
            weapon.Use();
            anim.SetTrigger("doAttack1");
            firedelay = 0;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            anim.SetBool("isJump", false);
            isJump = false;

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

