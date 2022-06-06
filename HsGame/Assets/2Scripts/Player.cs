using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxhealth = 100;
    public float curhealth = 100;
    public float maxmana = 100;
    public float curmana = 100;
    public float maxexp = 100;
    public float curexp = 0;
    public int PlayerLv =1;
    public GameObject[] Weapons;
    public static Player player;
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
    bool skillRcast;
    bool skillRUp;

    public bool skill1coolDown;
    public bool skill2coolDown;
    public bool skill3coolDown;
    public bool skill4coolDown;
    public bool skillRcoolDown;


    bool iswallF;
    bool iswallR;
    bool iswallL;
    bool isborder;
    bool sDown1;
    bool sDown2;
    bool jDown;
    bool isDodge;
    bool DodgeDown;
    bool isDamage;
    bool isSwap;
    public bool isCasting;
    int hasweaponindex = 0;
    float firedelay;
    float dodgedelay;
    float skillRcasting;
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

    public void Start()
    {
        player = this.GetComponent<Player>();
       
    }
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
        skillRcast = Input.GetButton("SkillR");
        skillRUp = Input.GetButtonUp("SkillR");

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
        if (isMove && canAttack && !isCasting)
        {
            characterBody.forward = movedir;
        }

        if (!canAttack || isSwap || isCasting)
        {
            movedir = Vector3.zero;
        }
        if (!iswallF && !iswallL && !iswallR)
        {
            transform.position += movedir * moveSpeed * Time.deltaTime;
        }
      
        
    }
    void Jump()
    {
        if (jDown && !isDodge && !isSwap && !isJump && canAttack && !isCasting)
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
                curhealth -= enemyBullet.damage;
                StartCoroutine(OnDamage());
            }
        }
    }
    void Swap()
    {
        if (sDown1 && !isSwap && canAttack && !isCasting) // 소드
        {
            anim.SetTrigger("doSwap");
            if(hasweaponindex == 1) // 소드를 들고있을때
            {
                Weapons[0].SetActive(false);
                hasweaponindex = 0;
                isSwap = true;
                Invoke("Swapout", 0.5f);
            }
            else //빈손,완드를 들고있을때
            {
                Weapons[1].SetActive(false);
                Weapons[0].SetActive(true);
                hasweaponindex = 1;
                isSwap = true;
                Invoke("Swapout", 0.5f);
            }
        }
        if (sDown2 && !isSwap && canAttack && !isCasting) //완드
        {
            anim.SetTrigger("doSwap");
            if (hasweaponindex == 2) // 완드를 들고있을때
            {
                Weapons[1].SetActive(false);
                hasweaponindex = 0;
                isSwap = true;
                Invoke("Swapout", 0.5f);
            }
            else // 빈손,소드를 들고있을때
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
    public void Attack()
    {
        firedelay += Time.deltaTime;
        canAttack = 0.7 < firedelay;
        if (fDown && canAttack && !isJump && hasweaponindex == 1 && !isSwap)
        {
            characterBody.forward = new Vector3(cameraArm.forward.x, 0, cameraArm.forward.z);
            Sword.Swing();
            anim.SetTrigger("doAttack1");
            firedelay = 0;
        }
        else if ((skill1Down || skill2Down || skill3Down || skill4Down)
            && canAttack && !isJump && hasweaponindex == 2 && !isSwap)
        {
            characterBody.forward = new Vector3(cameraArm.forward.x, 0, cameraArm.forward.z);    
            if (skill1Down && !skill1coolDown && curmana > 0)
            {
                curmana -= 10;
                skill1coolDown = true;
                anim.SetTrigger("doShot");
                Wand.Magic1();
                Invoke("Skill1cool",5f);
                firedelay = -0.2f;
            }
            if (skill2Down && !skill2coolDown && curmana > 0)
            {
                curmana -= 10;
                skill2coolDown = true;
                anim.SetTrigger("doShot");
                Wand.Magic2();
                Invoke("Skill2cool", 2f);
                firedelay = -0.2f;
            }
            if (skill3Down && !skill3coolDown && curmana > 0)
            {
                curmana -= 10;
                skill3coolDown = true;
                anim.SetTrigger("doShot");
                Wand.Magic3();
                Invoke("Skill3cool", 3f);
                firedelay = -0.2f;
            }
            if (skill4Down && !skill4coolDown && curmana > 0)
            {
                curmana -= 10;
                skill4coolDown = true;
                anim.SetTrigger("doShot");
                Wand.Magic4();
                Invoke("Skill4cool", 1f);
                firedelay = -0.2f;
            }
        }
        else if (skillRDown && canAttack && !isJump && hasweaponindex == 2 && !isSwap && !skillRcoolDown && curmana > 0)
        {
            characterBody.forward = new Vector3(cameraArm.forward.x, 0, cameraArm.forward.z);
            Wand.MagicRstart();
            Wand.MagicRUp();
        }
        if (skillRcast && hasweaponindex == 2 && !isJump && !skillRcoolDown && curmana > 0)
        {
            isCasting = true;
            anim.SetTrigger("doShot");
            skillRcasting += Time.deltaTime;
            if(skillRcasting >= 3)
            {
                skillRUp = true;
            }
        }
        if (skillRUp && !skillRcoolDown && curmana > 0)
        {
            Wand.MagicRUp();
            Invoke("Magicout", 0.7f);
            if (skillRcasting >= 1)
            {
                curmana -= 20;
                Wand.MagicRsucces();
                Invoke("SkillRcool", 10f);
                skillRcoolDown = true;
            }
            firedelay = -0.2f;
            skillRcasting = 0;
            isCasting = false;
        }
    }
    void Skill1cool()
    {
        skill1coolDown = false;
    }
    void Skill2cool()
    {
        skill2coolDown = false;
    }
    void Skill3cool()
    {
        skill3coolDown = false;
    }
    void Skill4cool()
    {
        skill4coolDown = false;
    }
    void SkillRcool()
    {
        skillRcoolDown = false;
    }
    void Magicout()
    {
        isCasting = false;
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
    void StopToWall()
    {
        iswallR = Physics.Raycast(characterBody.position, characterBody.forward, 1, LayerMask.GetMask("Wall"));
        iswallL = Physics.Raycast(characterBody.position + characterBody.right * 2, characterBody.forward, 2, LayerMask.GetMask("Wall"));
        iswallF = Physics.Raycast(characterBody.position + characterBody.right * -2, characterBody.forward, 2, LayerMask.GetMask("Wall"));
        Debug.DrawRay(characterBody.position, characterBody.forward  * 1 ,Color.green);
        Debug.DrawRay(characterBody.position + characterBody.right* 2, characterBody.forward * 2 ,Color.green);
        Debug.DrawRay(characterBody.position + characterBody.right*-2, characterBody.forward * 2 ,Color.green);
    }
    void FrezzeRotaiton()
    {
        rigid.angularVelocity = Vector3.zero;
    }

    void FixedUpdate()
    {
        FrezzeRotaiton();
        StopToWall();
    }


}

