using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;
    public Transform Target;
    public bool isChase;

    MeshRenderer [] meshs;
    BoxCollider BoxCollider;
    NavMeshAgent nav;
    Rigidbody rigid;
    Animator anim;
    
    void Awake()
    {
        meshs = GetComponentsInChildren<MeshRenderer>();
        BoxCollider = GetComponent<BoxCollider>();
        Weapon weapon = GetComponent<Weapon>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        Invoke("ChaseStart", 2);
    }

    void ChaseStart()
    {
        isChase = true;
        anim.SetBool("isWalk", true);
    }

    void Update()
    {
        if (isChase)
        {
            nav.SetDestination(Target.position);
            nav.isStopped = !isChase;
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
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            Weapon weapon = other.GetComponent<Weapon>();
            curHealth -= weapon.Damage;

            StartCoroutine("OnDamage");

            
        }
    }

    IEnumerator OnDamage()
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
                isChase = false;
                nav.enabled = false;
                anim.SetTrigger("doDie");
                rigid.AddForce(Vector3.up * 3, ForceMode.Impulse);
            }

            Destroy(gameObject, 4);
        }





    }
}
