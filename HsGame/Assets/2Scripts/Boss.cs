using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Boss : MonoBehaviour
{
    public float maxHealth;
    public float curHealth;
    public Transform Target;
    public BoxCollider meleeArea;
    public bool isChase;
    public bool isAttack;
    public bool isSearch;
    public bool isDead;
    public Image hpbar;
    MeshRenderer[] meshs;
    public NavMeshAgent nav;
    Rigidbody rigid;
    Animator anim;
    public bool isLook;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();       
        meshs = GetComponentsInChildren<MeshRenderer>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }
    void ChaseStart()
    {
        RaycastHit[] rayfindHits =
         Physics.SphereCastAll(transform.position, 100,
                              Vector3.up, 0,
                               LayerMask.GetMask("Player"));
        if (rayfindHits.Length > 0)
        {
            isSearch = true;
        }
        else
        {
            isSearch = false;
        }

        if (isSearch == true)
        {
            isChase = true;
            anim.SetBool("isWalk", true);
        }
        else
        {
            anim.SetBool("isWalk", false);
        }
    }

    void Update()
    {
        if (nav.enabled && isSearch)
        {
            nav.SetDestination(Target.position);
            nav.isStopped = !isSearch;
        }
        if (isAttack)
        {
            this.nav.velocity = Vector3.zero;
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
        ChaseStart();
        Targeting();
        FrezzeVelocity();
    }
    void Targeting()
    {
        if (!isDead)
        {
            float targetRadius = 1.5f;
            float targetRange = 15f;

            RaycastHit[] rayHits =
                      Physics.SphereCastAll(transform.position, targetRadius,
                                            transform.forward, targetRange,
                                            LayerMask.GetMask("Player"));

            if (rayHits.Length > 0 && !isAttack)
            {
                StartCoroutine(Think());
            }
        }
    }
    IEnumerator Think()
    {
        yield return new WaitForSeconds(0.1f);
        int ranAction = Random.Range(0, 4);
        switch (ranAction)
        {
            case 0:
                StartCoroutine("Attack1"); // 근접공격
                break;
            case 1:
                StartCoroutine("Attack2"); // 돌던지기
                break;
            case 2:
                StartCoroutine("Junp"); // 점프공격
                break;
            case 3:
                StartCoroutine("Rush"); // 돌진공격
                break;
        }
    }
    IEnumerator Attack1()
    {
        isChase = false;
        isAttack = true;
        anim.SetTrigger("Attack2");
        meleeArea.enabled = true;
        yield return new WaitForSeconds(1f);
        meleeArea.enabled = false;
        yield return new WaitForSeconds(2f);
        isChase = true;
        isAttack = false;
    }
    IEnumerator Attack2()
    {
        isChase = false;
        isAttack = true;
        anim.SetTrigger("Rush");
        yield return new WaitForSeconds(2f);
        isChase = true;
        isAttack = false;
    }

    IEnumerator Junp()
    {
        isChase = false;
        isAttack = true;
        anim.SetTrigger("Jump");
        yield return new WaitForSeconds(2f);
        isChase = true;
        isAttack = false;
    }
    IEnumerator Rush()
    {
        isChase = false;
        isAttack = true;
        anim.SetTrigger("Attack1");
        yield return new WaitForSeconds(2f);
        isChase = true;
        isAttack = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            Weapon weapon = other.GetComponent<Weapon>();
            curHealth -= weapon.Damage;
            Vector3 reactVec = transform.position - other.transform.position;
            StartCoroutine(OnDamage(reactVec));
        }
        else if (other.tag == "MagicArea")
        {
            MagicArea magic = other.GetComponent<MagicArea>();
            Vector3 reactVec = transform.position - other.transform.position;
            curHealth -= magic.Damage;
            StartCoroutine(OnDamage(reactVec));
        }

        else if (other.tag == "MagicArrow")
        {
            MagicArrow magic = other.GetComponent<MagicArrow>();
            Vector3 reactVec = transform.position - other.transform.position;
            curHealth -= magic.Damage;
            StartCoroutine(OnDamage(reactVec));
        }
    }

    IEnumerator OnDamage(Vector3 reactVec)
    {
        foreach (MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.red;
        }
        yield return new WaitForSeconds(0.1f);

        if (curHealth > 0)
        {
            foreach (MeshRenderer mesh in meshs)
            {
                mesh.material.color = Color.white;
            }

        }
        else
        {
            foreach (MeshRenderer mesh in meshs)
            {
                mesh.material.color = Color.gray;
                gameObject.layer = 10;
                isDead = true;
                isChase = false;
                nav.enabled = false;
                anim.SetTrigger("Die");
                reactVec = reactVec.normalized;
                reactVec += Vector3.up;
                rigid.AddForce(reactVec, ForceMode.Impulse);
            }
            Destroy(gameObject, 4);
            Player.player.curexp += 150 ;
        }
    }
}
