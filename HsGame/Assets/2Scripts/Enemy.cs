using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;
    public Transform Target;
    public BoxCollider meleeArea;
    public bool isChase;
    public bool isAttack;
    public bool isSearch;
    public bool isDead;

    MeshRenderer [] meshs;
    NavMeshAgent nav;
    Rigidbody rigid;
    Animator anim;
    
    void Awake()
    {
        meshs = GetComponentsInChildren<MeshRenderer>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
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
            Weapon weapon = other.GetComponent<Weapon>();
            curHealth -= weapon.Damage;
            Vector3 reactVec = transform.position - other.transform.position;

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
                anim.SetTrigger("doDie");

                reactVec = reactVec.normalized;
                reactVec += Vector3.up;
                rigid.AddForce(reactVec, ForceMode.Impulse);

            }

            Destroy(gameObject, 4);
        }





    }
}
