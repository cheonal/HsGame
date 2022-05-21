using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Test : MonoBehaviour
{
    public int maxhealth;
    public int health;
    public float moveSpeed;
    bool canAttack = true;
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
    Vector3 lookforward;
    Vector3 lookright;
    Vector3 movedir;

    public Transform cameraArm;
    public Transform characterBody;
    public Camera followCamera;

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
        Dodge();
        Camera();
    }

    void Getinput()
    {
        haxis = Input.GetAxisRaw("Horizontal");
        vaxis = Input.GetAxisRaw("Vertical");
        fDown = Input.GetButtonDown("Fire1");
        jDown = Input.GetButtonDown("Jump");
        DodgeDown = Input.GetButtonDown("Dodge");

    }

    void Camera()
    {
        if (Input.GetKey("q"))
        {
            cameraArm.Rotate(new Vector3(0, -1, 0));
            characterBody.Rotate(new Vector3(0, -1, 0));
        }
        if (Input.GetKey("e"))
        {
            cameraArm.Rotate(new Vector3(0, 1, 0));
            characterBody.Rotate(new Vector3(0, 1, 0));
        }
    }
    void Move()
    {
        Vector3 moveVec = new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        bool isMove = moveVec.magnitude != 0;
        anim.SetBool("isRun", isMove);
        lookforward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
        lookright = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
        movedir = lookforward * moveVec.y + lookright * moveVec.x;

        if (isDodge)
        {
            moveVec = DodgeVec;
        }
   
        if (isMove)
        {
            characterBody.forward = movedir;

        }

        if (!canAttack)
        {
            movedir = Vector3.zero;
        }
        if (jDown)
        {
            rigid.AddForce(Vector3.up * 5, ForceMode.Impulse);
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
        }
        transform.position += movedir * moveSpeed * Time.deltaTime;
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
    void Attack()
    {
        firedelay += Time.deltaTime;
        canAttack = 0.6 < firedelay;
        if (fDown && canAttack && !isJump)
        {
           // weapon.Use();
            anim.SetTrigger("doAttack1");
            firedelay = 0;
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

    IEnumerator OnDamage()
    {
        isDamage = true;
        foreach (MeshRenderer mesh in meshs)
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
        if (collision.gameObject.tag == "Floor")
        {
            anim.SetBool("isJump", false);
            isJump = false;

        }
    }
    void Dodgeout()
    {
        moveSpeed *= 0.5f;
        isDodge = false;
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

