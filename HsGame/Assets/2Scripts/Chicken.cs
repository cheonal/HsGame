using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;
    public bool isDead;
    SkinnedMeshRenderer[] meshs;
    Rigidbody rigid;
    Animator anim;
    bool isWalk;
    public int speed;
    public Vector3 movevec;
    void Awake()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(Action());
        Invoke("Move", 2);
        meshs = GetComponentsInChildren<SkinnedMeshRenderer>();
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {


        if (isWalk)
        {
            this.transform.position += movevec * speed * Time.deltaTime;

        }
        transform.LookAt(transform.position + movevec);
        if (isDead)
        {
            movevec = Vector3.zero;
        }
    }

    void Move()
    {
        speed = Random.Range(1,3);
        movevec = new Vector3(Random.Range(0, 3), 0, Random.Range(0, 3)).normalized;
        Invoke("Move", 2);
    }



    IEnumerator Action()
    {
        yield return new WaitForSeconds(0.1f);

        int random = Random.Range(0, 3);
        switch (random)
        {
            case 0:
                StartCoroutine(Walk());
                break;
            case 1:
                StartCoroutine(TurnHead());
                break;
            case 2:
                 StartCoroutine(Eat());
                break;
        }
    }

    IEnumerator Walk()
    {


        yield return new WaitForSeconds(0.1f);
        isWalk = true;
        anim.SetBool("Walk", true);

        yield return new WaitForSeconds(2f);
        isWalk = false;
        anim.SetBool("Walk", false);
        StartCoroutine(Action());
    }


    IEnumerator Eat()
    {
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("Eat", true);
        yield return new WaitForSeconds(2f);
        anim.SetBool("Eat", false);
        StartCoroutine(Action());
    }

    IEnumerator TurnHead()
    {
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("Turn Head", true);
        yield return new WaitForSeconds(2f);
        anim.SetBool("Turn Head", false);
        StartCoroutine(Action());
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
        foreach (SkinnedMeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.red;
        }
        yield return new WaitForSeconds(0.1f);

        if (curHealth > 0)
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
                anim.SetTrigger("doDie");
            }

            Destroy(gameObject, 4);
        }
    }


}
