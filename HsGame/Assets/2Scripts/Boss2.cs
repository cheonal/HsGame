using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Boss2 : MonoBehaviour
{
    public Transform Target;
    public NavMeshAgent nav;
    public bool canAttack;
    public bool AttackCoolDown;
    public bool isChase;
    Animator anim;
    Rigidbody rigid;
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
      //  meshs = GetComponentsInChildren<MeshRenderer>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        Invoke("SearchAttck", 2f);
    }
    void Update()
    {
        Chase();
        CanAttack();
        Attack();
    }
    void Chase()
    {
        if (AttackCoolDown)
        {
            this.nav.velocity = Vector3.zero;
        }
        if (canAttack && AttackCoolDown)
        {
            anim.SetBool("isWalk", false);
            nav.isStopped = true;
        }else
        {
            nav.isStopped = false;
        }
        if (isChase && !AttackCoolDown)
        {
            nav.SetDestination(Target.position);
            anim.SetBool("isWalk",true);
        }


    }
    void SearchAttck()
    {
        if (canAttack && !AttackCoolDown)
        {
            StartCoroutine("Think");
        }
        Invoke("SearchAttck", 1f);
    }
    void CanAttack()
    {
        RaycastHit[] rayfindHits =
       Physics.SphereCastAll(transform.position, 30,
                            Vector3.up, 0f,
                             LayerMask.GetMask("Player"));
        if (rayfindHits.Length>0)
        {
            canAttack = true;
            isChase = false;
        }else
        {
            canAttack = false;
            isChase = true;
        }
    }
    void Attack()
    {

    }
    IEnumerator Think()
    {
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
        anim.SetTrigger("Attack1");
        AttackCoolDown = true;

        yield return new WaitForSeconds(2f);

        AttackCoolDown = false;
        Debug.Log("1");
    }
    IEnumerator Attack2()
    {
        anim.SetTrigger("Attack2");
        AttackCoolDown = true;

        yield return new WaitForSeconds(2f);

        AttackCoolDown = false;
        Debug.Log("2");
    }

    IEnumerator Jump()
    {
        anim.SetTrigger("Jump");
        AttackCoolDown = true;
        yield return new WaitForSeconds(3f);

        AttackCoolDown = false;
        Debug.Log("2");
    }
    IEnumerator Rush()
    {
        anim.SetTrigger("Rush");
        AttackCoolDown = true;

        yield return new WaitForSeconds(5f);

        AttackCoolDown = false;
        Debug.Log("2");
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
