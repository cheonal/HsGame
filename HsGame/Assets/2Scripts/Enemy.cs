using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
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

    public MeshRenderer [] meshs;
    public NavMeshAgent nav;
    public Rigidbody rigid;
    public Animator anim;
    private AudioSource audiosoruce;
    [SerializeField] private AudioClip[] clip;
    void Awake()
    {
        audiosoruce = GetComponent<AudioSource>();
        meshs = GetComponentsInChildren<MeshRenderer>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        hpbar.rectTransform.localScale = new Vector3(1, 1, 1);
    }

    void ChaseStart()
    {
            RaycastHit[] rayfindHits =
             Physics.SphereCastAll(transform.position, 25,
                                  Vector3.up, 0,
                                   LayerMask.GetMask("Player"));
        if(rayfindHits.Length > 0)
        {
            isSearch = true;
        }
        else
        {
            isSearch = false;
        }

        if(isSearch == true)
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
        HpBar();

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

    void HpBar()
    {
        if (curHealth <= 0)
        {
            hpbar.rectTransform.localScale = new Vector3(0, 1, 1);
        }
        else
        {
            hpbar.rectTransform.localScale = new Vector3((float)curHealth / (float)maxHealth, 1, 1);
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
            float targetRange = 3f;

            RaycastHit[] rayHits =
                      Physics.SphereCastAll(transform.position, targetRadius,
                                            transform.forward, targetRange,
                                            LayerMask.GetMask("Player"));

            if (rayHits.Length > 0 && !isAttack)
            {
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        isChase = false;
        isAttack = true;
        anim.SetBool("isAttack", true);
        audiosoruce.clip = clip[0];
        audiosoruce.Play();

        yield return new WaitForSeconds(0.2f);
        meleeArea.enabled = true;

        yield return new WaitForSeconds(1f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(1f);

        isChase = true;
        isAttack = false;
        anim.SetBool("isAttack", false);


    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            audiosoruce.clip = clip[1];
            audiosoruce.Play();
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
                audiosoruce.clip = clip[2];
                audiosoruce.Play();
                mesh.material.color = Color.gray;
                gameObject.layer = 10;
                isDead = true;
                isChase = false;
                nav.enabled = false;
                anim.SetTrigger("doDie");
                reactVec = reactVec.normalized;
                reactVec += Vector3.up;
                rigid.AddForce(reactVec, ForceMode.Impulse);
            }
            QuestManager.questManager.EnemyPoint++;
            EnemySpawn.enemySpawn.EnemyCount--;
            Destroy(gameObject, 4);
            Player.player.curexp += 25;
        }
    }
}
