using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;
    public bool isDead;
    MeshRenderer[] meshs;
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
        meshs = GetComponentsInChildren<MeshRenderer>();
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isWalk)
        {
            this.transform.position += movevec * speed * Time.deltaTime;
        }
        transform.LookAt(transform.position + movevec);
    }

    void Move()
    {
        speed = Random.Range(0, 5);
        movevec = new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3)).normalized;
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
                anim.SetTrigger("doDie");

                reactVec = reactVec.normalized;
                reactVec += Vector3.up;
                rigid.AddForce(reactVec, ForceMode.Impulse);

            }

            Destroy(gameObject, 4);
        }
    }
}
