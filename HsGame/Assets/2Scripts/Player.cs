using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxhealth;
    public int health;
    public float rotateSensitivity;
    public float moveSpeed;
    bool canAttack = true;
    bool canDodge = true;
    bool isJump;
    bool fDown;
    bool jDown;
    bool isDodge;
    bool DodgeDown;
    bool isDamage;
    float firedelay;
    float dodgedelay;
    public Weapon weapon;

    Rigidbody rigid;
    Animator anim;
    MeshRenderer[] meshs;
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
        Jump();
        Camera();
    }

    void Getinput()
    {
        fDown = Input.GetButtonDown("Fire1");
        jDown = Input.GetButtonDown("Jump");
        DodgeDown = Input.GetButtonDown("Dodge");
    }

    void Camera()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngel = cameraArm.rotation.eulerAngles;

        if (Input.GetKeyDown("q"))
        {
            rotateSensitivity -= 0.1f;
        }
        if (Input.GetKeyDown("e"))
        {
            rotateSensitivity += 0.1f;
        }
        cameraArm.rotation = Quaternion.Euler(camAngel.x , camAngel.y + mouseDelta.x * rotateSensitivity, camAngel.z);
    }
    void Move()
    {
        Vector3 moveVec = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        bool isMove = moveVec.magnitude != 0;
        anim.SetBool("isRun", isMove);
        lookforward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
        lookright = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
        movedir = lookforward * moveVec.y + lookright * moveVec.x;
        if (isMove)
        {
            characterBody.forward = movedir;
        }

        if (!canAttack)
        {
            movedir = Vector3.zero;
        }
      
        transform.position += movedir * moveSpeed * Time.deltaTime;
    }
    void Jump()
    {
        if (jDown && !isDodge)
        {
            rigid.AddForce(Vector3.up * 5, ForceMode.Impulse);
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
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
    void Attack()
    {
        firedelay += Time.deltaTime;
        canAttack = 0.6 < firedelay;
        if (fDown && canAttack && !isJump)
        {
            characterBody.forward = new Vector3(cameraArm.forward.x,0,cameraArm.forward.z);
            weapon.Use();
            anim.SetTrigger("doAttack1");
            firedelay = 0;
        }
    }

    void Dodge()
    {
        dodgedelay += Time.deltaTime;
        canDodge = 5 < dodgedelay;
        if (DodgeDown && !isJump && canDodge)
        {
            moveSpeed *= 2;
            isDodge = true;
            anim.SetTrigger("doDodge");
            dodgedelay = 0;
            Invoke("Dodgeout", 0.5f);

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

