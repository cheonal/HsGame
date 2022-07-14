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
    NavMeshAgent nav;
    Rigidbody rigid;
    Animator anim;
    public GameObject Missle;
    public Transform MisslePort;
    public bool isLook;

    Vector3 lookVec;
    Vector3 tauntVec;   

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();       
        meshs = GetComponentsInChildren<MeshRenderer>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
       // nav.isStopped = true;
        //StartCoroutine("Think");
    }

    void Update()
    {
        Targeting();
        if (isDead)
        {
            StopAllCoroutines();
            return;
        }
        else
        {
            nav.SetDestination(Target.position);
        }
        if (isAttack)
        {
            this.nav.velocity = Vector3.zero;
        }
            anim.SetBool("isWalk", true);

        /*if (isLook)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            lookVec = new Vector3(h, 0, v) * 5f;
            transform.LookAt(Target.position + lookVec);
        }*/
    }
    void Targeting()
    {
        if (!isDead)
        {
            float targetRadius = 1.5f;
            float targetRange = 10f;

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
        anim.SetTrigger("Attack1");
        isAttack = true;
        meleeArea.enabled = true;
        yield return new WaitForSeconds(1f);
        meleeArea.enabled = false;
        yield return new WaitForSeconds(1f);
        isAttack = false;
        StartCoroutine(Think());
    }
    IEnumerator Attack2()
    {
        anim.SetTrigger("Attack2");
        isAttack = true;
        yield return new WaitForSeconds(1f);
        isAttack = false;
        StartCoroutine(Think());
    }

    IEnumerator Junp()
    {
        anim.SetTrigger("Jump");
        isAttack = true;
        yield return new WaitForSeconds(1f);
        isAttack = false;
        StartCoroutine(Think());
    }
    IEnumerator Rush()
    {
        anim.SetTrigger("Rush");
        isAttack = true;
        yield return new WaitForSeconds(1f);
        isAttack = false;
        StartCoroutine(Think());
    }
    void FixedUpdate()
    {
        FrezzeVelocity();
    }
    void FrezzeVelocity()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
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
