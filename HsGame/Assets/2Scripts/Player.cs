using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxhealth;
    public int health;
    public GameObject[] Weapons;

    public float rotateSensitivity;
    public float moveSpeed;

    bool canAttack = true;
    bool canDodge = true;
    bool isJump;
    bool fDown;
    bool skill1Down;
    bool skill2Down;
    bool skill3Down;
    bool skill4Down;
    bool skillRDown;
    bool sDown1;
    bool sDown2;
    bool jDown;
    bool isDodge;
    bool DodgeDown;
    bool isDamage;
    bool isSwap;
    int hasweaponindex = 0;
    float firedelay;
    float dodgedelay;
    public Weapon Sword;
    public Weapon Wand;

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
        Swap();
        Camera();
    }

    void Getinput()
    {
        fDown = Input.GetButtonDown("Fire1");
        jDown = Input.GetButtonDown("Jump");
        DodgeDown = Input.GetButtonDown("Dodge");
        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");
        skill1Down = Input.GetButtonDown("Skill1");
        skill2Down = Input.GetButtonDown("Skill2");
        skill3Down = Input.GetButtonDown("Skill3");
        skill4Down = Input.GetButtonDown("Skill4");
        skillRDown = Input.GetButtonDown("SkillR");

    }

    void Camera()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngel = cameraArm.rotation.eulerAngles;

        if (Input.GetKeyDown("["))
        {
            rotateSensitivity -= 0.1f;
        }
        if (Input.GetKeyDown("]"))
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
        if (isMove && canAttack)
        {
            characterBody.forward = movedir;
        }

        if (!canAttack || isSwap)
        {
            movedir = Vector3.zero;
        }
      
        transform.position += movedir * moveSpeed * Time.deltaTime;
    }
    void Jump()
    {
        if (jDown && !isDodge && !isSwap && !isJump && canAttack)
        {
            rigid.AddForce(Vector3.up * 5, ForceMode.Impulse);
            isJump = true;
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
    void Swap()
    {
        if (sDown1 && !isSwap && canAttack) // 소드
        {
            anim.SetTrigger("doSwap");
            if(hasweaponindex == 1)
            {
                Weapons[1].SetActive(false);
                Weapons[0].SetActive(false);
                hasweaponindex = 0;
                isSwap = true;
                Invoke("Swapout", 0.5f);
            }
            else
            {
                Weapons[1].SetActive(false);
                Weapons[0].SetActive(true);
                hasweaponindex = 1;
                isSwap = true;
                Invoke("Swapout", 0.5f);
            }
        }
        if (sDown2 && !isSwap && canAttack) //완드
        {
            anim.SetTrigger("doSwap");
            if (hasweaponindex == 2)
            {
                Weapons[1].SetActive(false);
                Weapons[0].SetActive(false);
                hasweaponindex = 0;
                isSwap = true;
                Invoke("Swapout", 0.5f);
            }
            else
            {

                Weapons[0].SetActive(false);
                Weapons[1].SetActive(true);
                hasweaponindex = 2;
                isSwap = true;
                Invoke("Swapout", 0.5f);
            }
        }
    }

    void Swapout()
    {
        isSwap = false;
    }
    void Attack()
    {
        firedelay += Time.deltaTime;
        canAttack = 0.7 < firedelay;
        if (fDown && canAttack && !isJump && hasweaponindex==1 && !isSwap)
        {
            characterBody.forward = new Vector3(cameraArm.forward.x,0,cameraArm.forward.z);
            Sword.Swing();
            anim.SetTrigger("doAttack1");
            firedelay = 0;
        }
        else if ((skill1Down || skill2Down  || skill3Down || skill4Down || skillRDown) 
            && canAttack && !isJump && hasweaponindex == 2 && !isSwap)
        {
            characterBody.forward = new Vector3(cameraArm.forward.x, 0, cameraArm.forward.z);
            anim.SetTrigger("doShot");
            firedelay = 0;  
            if (skill1Down)
            {
                Wand.Magic1();
            }
            if (skill2Down)
            {
                Wand.Magic2();
            }
            if (skill3Down)
            {
                Wand.Magic3();
            }
            if (skill4Down)
            {
                Wand.Magic4();
            }
            if (skillRDown)
            {
                Wand.MagicR();
            }

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

