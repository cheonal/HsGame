using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeed;
    public int maxhealth;
    public int health;
    bool canAttack = true;
    public Camera followCamera;

    bool isJump;
    bool fDown;
    bool jDown;
    bool isDodge;
    bool DodgeDown;
    bool isDamage;



    float vaxis;
    float haxis;
    float firedelay;
    public Weapon weapon;


    Rigidbody rigid;
    Animator anim;
    MeshRenderer[] meshs;

    Vector3 moveVec;
    Vector3 DodgeVec;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        meshs = GetComponentsInChildren<MeshRenderer>();
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
            rigid.AddForce(Vector3.up * 8,ForceMode.Impulse);
            isJump = true;
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
        }
    }
    void Turn() 
    {
        transform.LookAt(transform.position + moveVec);

        if (fDown)
        {
            moveVec = Vector3.zero;
            Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayhit;
            if (Physics.Raycast(ray, out rayhit, 100))
            {
                Vector3 nextVect = rayhit.point - transform.position;
                nextVect.y = 0;
                transform.LookAt(transform.position + nextVect);
            }


        }

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
        canAttack = 1 < firedelay;
        if (fDown && canAttack && !isJump ) 
        {
            weapon.Use();
            anim.SetTrigger("doAttack1");
            firedelay = 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy Bullet")
        {
            if (!isDamage)
            {
                Bullet enemyBullet = other.GetComponent<Bullet>();
                health -= enemyBullet.damage;
                StartCoroutine(OnDamage());
            }

        }
    }

    IEnumerator OnDamage()
    {
        isDamage = true;
        foreach(MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.gray;
        }
        yield return new WaitForSeconds(1f);

        isDamage = false;
        foreach (MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.white;
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

