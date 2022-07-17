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
    public GameObject Rock;
    public GameObject MeleeArea;
    public GameObject RushArea;
    public GameObject JumpArea;
    public GameObject JumpWarning;
    Animator anim;
    Rigidbody rigid;
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
      //  meshs = GetComponentsInChildren<MeshRenderer>();
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
        }else
        {
            nav.isStopped = false;
        }

    }
    void CanAttack()
    {
        RaycastHit[] rayfindHits =
       Physics.SphereCastAll(transform.position, 30,
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
    void SearchAttck()
    {
        if (canAttack && !AttackCoolDown)
        {
            StartCoroutine("Think");
        }
        Invoke("SearchAttck", 1f);
    }

    IEnumerator Think()
    {
        int ranAction = Random.Range(0, 5);
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
        yield return new WaitForSeconds(1f);
        rigid.AddForce(transform.forward * 90, ForceMode.Impulse);
        RushArea.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        RushArea.SetActive(false);
        rigid.velocity = Vector3.zero;
        yield return new WaitForSeconds(3f);
        AttackCoolDown = false;
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
