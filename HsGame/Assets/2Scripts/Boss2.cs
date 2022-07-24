using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Boss2 : MonoBehaviour
{
    public Transform Target;
    public Transform RockSpawn;
    public NavMeshAgent nav;
    public bool canAttack;
    public bool AttackCoolDown;
    public bool isChase;
    public static Boss2 boss;
    public GameObject Rock;
    public GameObject MeleeArea;
    public GameObject RushArea;
    public GameObject JumpArea;
    public GameObject JumpWarning;
    public float MaxHp;
    public float curHp;
    bool isDead;
    Animator anim;
    Rigidbody rigid;
    SkinnedMeshRenderer[] meshs;
    private AudioSource audiosoruce;
    [SerializeField] private AudioClip[] clip;

    public void Start()
    {
        audiosoruce = GetComponent<AudioSource>();
        boss = this.GetComponent<Boss2>();
    }
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        meshs = GetComponentsInChildren<SkinnedMeshRenderer>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        SearchAttck();
    }
    void Update()
    {
        Chase();
        CanAttack();
    }
    void Chase()
    {
        if (!isDead)
        {
            if (isChase && !AttackCoolDown)
            {
                nav.SetDestination(Target.position);
                anim.SetBool("isWalk", true);
            }
            if (AttackCoolDown)
            {
                this.nav.velocity = Vector3.zero;
            }
            if (canAttack && AttackCoolDown)
            {
                anim.SetBool("isWalk", false);
                nav.isStopped = true;
            }
            else
            {
                nav.isStopped = false;
            }
        }
    }
    void CanAttack()
    {
        if (!isDead)
        {
            RaycastHit[] rayfindHits =
                Physics.SphereCastAll(transform.position, 15,
                    Vector3.up, 0f,
                     LayerMask.GetMask("Player"));
            if (rayfindHits.Length > 0)
            {
                canAttack = true;
                isChase = false;
            }
            else
            {
                canAttack = false;
                isChase = true;
            }
        }


    }
    void SearchAttck()
    {
        if (canAttack && !AttackCoolDown && !isDead)
        {
            StartCoroutine("Think");
        }
        Invoke("SearchAttck", 1f);
    }

    IEnumerator Think()
    {
        int ranAction = Random.Range(0, 4);
        switch (ranAction)
        {
            case 0:
                StartCoroutine("Attack1"); // 돌던지기
                break;
            case 1:
                StartCoroutine("Attack2"); // 근접공격
                break;
            case 2:
                StartCoroutine("Jump"); // 점프공격
                break;
            case 3:
                StartCoroutine("Rush"); // 돌진공격
                break;
        }
        yield return null;
    }
    IEnumerator Attack1()
    {
        transform.LookAt(Target);
        anim.SetTrigger("Attack1");
        AttackCoolDown = true;

        yield return new WaitForSeconds(1.1f);
        audiosoruce.clip = clip[0];
        audiosoruce.Play();
        RockSpawn.LookAt(Target);
        GameObject Rock2 = Instantiate(Rock, RockSpawn.position, RockSpawn.rotation);
        Rigidbody RockRigid = Rock2.GetComponent<Rigidbody>();
        RockRigid.velocity = RockSpawn.forward * 50;

        yield return new WaitForSeconds(2f);
        Destroy(Rock2);

        yield return new WaitForSeconds(1f);
        AttackCoolDown = false;
    }
    IEnumerator Attack2()
    {
        anim.SetTrigger("Attack2");
        AttackCoolDown = true;
        yield return new WaitForSeconds(1f);
        audiosoruce.clip = clip[1];
        audiosoruce.Play();
        MeleeArea.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        MeleeArea.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        AttackCoolDown = false;

    }

    IEnumerator Jump()
    {
        JumpWarning.SetActive(true);    
        AttackCoolDown = true;
        yield return new WaitForSeconds(1f);
        anim.SetTrigger("Jump");
        audiosoruce.clip = clip[2];
        audiosoruce.Play();
        yield return new WaitForSeconds(0.3f);
        JumpArea.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        JumpArea.SetActive(false);
        JumpWarning.SetActive(false);
        yield return new WaitForSeconds(1f);
        AttackCoolDown = false;

    }
    IEnumerator Rush()
    {
        transform.LookAt(Target);
        anim.SetTrigger("Rush");
        AttackCoolDown = true;
        audiosoruce.clip = clip[3];
        audiosoruce.Play();
        yield return new WaitForSeconds(1f);
        rigid.AddForce(transform.forward * 90, ForceMode.Impulse);
        RushArea.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        RushArea.SetActive(false);
        rigid.velocity = Vector3.zero;
        yield return new WaitForSeconds(3f);
        AttackCoolDown = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            Weapon weapon = other.GetComponent<Weapon>();
            curHp -= weapon.Damage;
            Vector3 reactVec = transform.position - other.transform.position;
            StartCoroutine(OnDamage(reactVec));
        }
        else if (other.tag == "MagicArea")
        {
            MagicArea magic = other.GetComponent<MagicArea>();
            Vector3 reactVec = transform.position - other.transform.position;
            curHp -= magic.Damage;
            StartCoroutine(OnDamage(reactVec));
        }

        else if (other.tag == "MagicArrow")
        {
            MagicArrow magic = other.GetComponent<MagicArrow>();
            Vector3 reactVec = transform.position - other.transform.position;
            curHp -= magic.Damage;
            StartCoroutine(OnDamage(reactVec));
        }
    }

    IEnumerator OnDamage(Vector3 reactVec)
    {
        audiosoruce.clip = clip[4];
        audiosoruce.Play();
        foreach (SkinnedMeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.red;
        }
        yield return new WaitForSeconds(0.1f);

        if (curHp > 0)
        {
            foreach (SkinnedMeshRenderer mesh in meshs)
            {
                mesh.material.color = Color.white;
            }

        }
        else
        {
            foreach (SkinnedMeshRenderer mesh in meshs)
            {
                mesh.material.color = Color.gray;
                gameObject.layer = 10;
                isDead = true;
                nav.enabled = false;
                anim.SetTrigger("Die");
                reactVec = reactVec.normalized;
                reactVec += Vector3.up;
                rigid.AddForce(reactVec, ForceMode.Impulse);
                curHp = 0;
            }
            Destroy(gameObject, 4);
        }
    }
    void FrezzeVelocity()
    {
        if (isChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }

    }

    void FixedUpdate()
    {
        FrezzeVelocity();
    }

}
