using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager manager;
    public EnemySpawn enemySpawn;
    public ChickenSpawn ChickenSpawn;
    public PlayerStateUi State;
    public static Player player;
    public Weapon Sword;
    public Weapon Wand;
    public Transform cameraArm;
    public Transform characterBody;
    public Camera followCamera;
    public GameObject item3On;
    public GameObject HitEffect;

    public bool skill1coolDown;
    public bool skill2coolDown;
    public bool skill3coolDown;
    public bool skill4coolDown;
    public bool skillRcoolDown;
    public bool isCasting;
    public bool isPortal;
    public bool isState;
    public bool isDead;
    public float maxhealth;
    public float curhealth;
    public float maxmana;
    public float curmana;
    public float maxexp = 100;
    public float curexp = 0;
    public float firedelay;
    public float moveSpeed;
    public int LvPoint;
    public int PlayerLv = 1;

    [SerializeField] GameObject[] Weapons;
    [SerializeField] float rotateSensitivity;
    [SerializeField] AudioClip[] clip;
    private AudioSource audiosoruce;

    RaycastHit rayhit;
    GameObject scanObject;
    Rigidbody rigid;
    Animator anim;
    MeshRenderer[] meshs;
    Vector3 lookforward;
    Vector3 lookright;
    Vector3 movedir;

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
    bool StateDown;
    bool TalkDown;
    bool iswallF;
    bool iswallR;
    bool iswallL;
    bool sDown1;
    bool sDown2;
    bool jDown;
    bool isDodge;
    bool DodgeDown;
    bool isDamage;
    bool isSwap;
    bool Item1;
    bool Item2;
    bool Item3;
    bool Item4;
    int hasweaponindex = 0;
    float dodgedelay;
    float skillRcasting;

    public void Start()
    {
        player = this.GetComponent<Player>();
    }
    void Awake()
    {
        audiosoruce = GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        meshs = GetComponentsInChildren<MeshRenderer>();
    }

    void Update()
    {
        StopToWall();
        Move();
        Getinput();
        Attack();
        Dodge();
        Jump();
        Swap();
        Camera();
        Talk();
        StateOn();
        UseItem();
        StateControll();
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
        TalkDown = Input.GetButtonDown("Talk");
        StateDown = Input.GetButtonDown("State");
        Item1 = Input.GetButtonDown("Item1");
        Item2 = Input.GetButtonDown("Item2");
        Item3 = Input.GetButtonDown("Item3");
        Item4 = Input.GetButtonDown("Item4");

    }
    void Camera()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngel = cameraArm.rotation.eulerAngles;

        if (manager.isTalk)
        {
            mouseDelta = new Vector2(0, 0);
        }
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
        if (!canAttack || isSwap || isCasting || manager.isTalk && !isDead)
        {
            movedir = Vector3.zero;
        }
        if (manager.isTalk && !isDead)
        {
            anim.SetBool("isRun", false);
        }
        if (isMove && canAttack && !isCasting && !manager.isTalk && !isSwap && !isDead)
        {
            characterBody.forward = movedir;
        }
        if (!iswallF && !iswallL && !iswallR && !isDead)
        {
            transform.position += movedir * moveSpeed * Time.deltaTime;
        }

      
        
    }
    void Jump()
    {
        if (jDown && !isDodge && !isSwap && !isJump && canAttack && !isCasting && !manager.isTalk)
        {
            rigid.AddForce(Vector3.up * 5, ForceMode.Impulse);
            isJump = true;
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
        }
    }
    void UseItem()
    {
        if (Item1)
        {
            State.Item1Use();
        }
        if (Item2)
        {
            State.Item2Use();
        }
        if (Item3)
        {
            State.Item3Use();
        }
        if (Item4)
        {
            State.Item4Use();
        }
    }

    void StateOn()
    {
        if (StateDown)
        {
            if (!isState)
            {
                State.StateCall();
                isState = true;
            }
            else
            {
                State.StateExit();
                isState = false;
            }
        }
    }
    void StateControll()
    {
        if (curhealth < 0)
        {
            curhealth = 0;
        }
        if (curmana < 0)
        {   
            curmana = 0;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy Bullet")
        {
            if (!isDamage && !isDead)
            {
                Bullet enemyBullet = other.GetComponent<Bullet>();
                curhealth -= enemyBullet.damage;
                StartCoroutine(OnDamage());
            }
        }
       if (other.tag == "Portal")
        {
            if(other.name == "MetalEnemySpawn")
            {
                isPortal = true;
                HitEffect.SetActive(true);
            }
            if (other.name == "ChickenSpawn")
            {
                ChickenSpawn.ChickenStart();
            }
            if( other.name == "BossPortal")
            {
                manager.BossRoomSpawn();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Portal")
        {
            if (other.name == "MetalEnemySpawn")
            {
                isPortal = false;
                enemySpawn.enemyEnd();
            }
            else if (other.name == "ChickenSpawn")
            {
                ChickenSpawn.chickenEnd();
            }

        }
    }
    IEnumerator OnDamage()
    {
        if (curhealth <= 0)
        {
            audiosoruce.clip = clip[3];
            audiosoruce.Play();
            anim.SetTrigger("doDie");
            isDead = true;
        }
        isDamage = true;
        GameObject HitEffect1 = Instantiate(HitEffect, transform.position+Vector3.up*2, transform.rotation);
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
    void Swap()
    {
        if (sDown1 && !isSwap && canAttack && !isCasting && !manager.isTalk) // 소드
        {
            anim.SetTrigger("doSwap");
            audiosoruce.clip = clip[0];
            audiosoruce.Play();
            if (hasweaponindex == 1) // 소드를 들고있을때
            {
                Weapons[0].SetActive(false);
                hasweaponindex = 0;
                isSwap = true;
                manager.IconSword.color = new Color(1, 1, 1);
                manager.IconHand.color = new Color(0, 0, 0);
                Invoke("Swapout", 0.5f);
            }
            else //빈손,완드를 들고있을때
            {
                Weapons[1].SetActive(false);
                Weapons[0].SetActive(true);
                hasweaponindex = 1;
                isSwap = true;
                manager.IconWand.color = new Color(1, 1, 1);
                manager.IconSword.color = new Color(0, 0, 0);
                manager.IconHand.color = new Color(1, 1, 1);
                Invoke("Swapout", 0.5f);
            }
        }
        if (sDown2 && !isSwap && canAttack && !isCasting && !manager.isTalk) //완드
        {
            anim.SetTrigger("doSwap");
            audiosoruce.clip = clip[0];
            audiosoruce.Play();
            if (hasweaponindex == 2) // 완드를 들고있을때
            {
                Weapons[1].SetActive(false);
                hasweaponindex = 0;
                isSwap = true;
                manager.IconWand.color = new Color(1, 1, 1);
                manager.IconHand.color = new Color(0, 0, 0);
                Invoke("Swapout", 0.5f);
            }
            else // 빈손,소드를 들고있을때
            {
                Weapons[0].SetActive(false);
                Weapons[1].SetActive(true);
                hasweaponindex = 2;
                isSwap = true;
                manager.IconWand.color = new Color(0, 0, 0);
                manager.IconSword.color = new Color(1, 1, 1);
                manager.IconHand.color = new Color(1, 1, 1);
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
        if (fDown && canAttack && !isJump && hasweaponindex == 1 && !isSwap && !manager.isTalk && !isDead && !isState)
        {
            audiosoruce.clip = clip[1];
            audiosoruce.Play();

            characterBody.forward = new Vector3(cameraArm.forward.x, 0, cameraArm.forward.z);
            Sword.Swing();
            anim.SetTrigger("doAttack1");
            firedelay = 0;
        }
        else if ((skill1Down || skill2Down || skill3Down || skill4Down)
            && canAttack && !isJump && hasweaponindex == 2 && !isSwap && !manager.isTalk && !isDead && !isState)
        {
            characterBody.forward = new Vector3(cameraArm.forward.x, 0, cameraArm.forward.z);    
            if (skill1Down && !skill1coolDown && curmana > 0)
            {
                curmana -= 20;
                skill1coolDown = true;
                anim.SetTrigger("doShot");
                Wand.Magic1();
                Invoke("Skill1cool",5f);
                firedelay = -0.2f;
            }
            if (skill2Down && !skill2coolDown && curmana > 0)
            {
                curmana -= 5;
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
                curmana -= 5;
                skill4coolDown = true;
                anim.SetTrigger("doShot");
                Wand.Magic4();
                Invoke("Skill4cool", 1f);
                firedelay = -0.2f;
            }
        }
        else if (skillRDown && canAttack && !isJump && hasweaponindex == 2 && !isSwap && !skillRcoolDown && curmana > 0 && !manager.isTalk && !isDead &&!isState)
        {
            characterBody.forward = new Vector3(cameraArm.forward.x, 0, cameraArm.forward.z);
            Wand.MagicRstart();
            Wand.MagicRUp();
        }
        if (skillRcast && hasweaponindex == 2 && !isJump && !skillRcoolDown && curmana > 0 && !manager.isTalk && !isDead)
        {
            isCasting = true;
            anim.SetTrigger("doShot");
            skillRcasting += Time.deltaTime;
            if(skillRcasting >= 3)
            {
                skillRUp = true;
            }
        }
        if (skillRUp && !skillRcoolDown && curmana > 0 && !isDead)
        {
            Wand.MagicRUp();
            Invoke("Magicout", 0.7f);
            if (skillRcasting >= 1)
            {
                audiosoruce.clip = clip[2];
                audiosoruce.Play();
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            anim.SetBool("isJump", false);
            isJump = false;
        }
    }
    public void ReBirth()
    {
        curhealth = maxhealth;
        curmana = maxmana;
        transform.position = new Vector3(-6, 1, 12);
        isDead = false;
        anim.SetTrigger("ReBirth");
    }
    void Dodgeout()
    {
        moveSpeed *= 0.5f;
        isDodge = false;
    }
    void StopToWall()
    {
        iswallR = Physics.Raycast(characterBody.position, characterBody.forward, 1, LayerMask.GetMask("Wall"));
    }
    void Talk()
    {

        if (TalkDown && scanObject != null && hasweaponindex==0)
        {
            manager.Action(scanObject);
        }
        
    }
    void Scan()
    {
        Physics.Raycast(cameraArm.position, cameraArm.forward,out rayhit, 8f, LayerMask.GetMask("Npc"));
        if (rayhit.collider != null)
        {
            scanObject = rayhit.collider.gameObject;
        }
        else
        {
            scanObject = null;
        }
    }
    void FrezzeRotaiton()
    {
        rigid.angularVelocity = Vector3.zero;
    }

    void FixedUpdate()
    {
        FrezzeRotaiton();
        Scan();
    }


}

