using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Chicken : MonoBehaviour
{
    public float maxHealth;
    public float curHealth;
    public Image hpbar;
    public enum Type { A, B };
    public Type enumType;
    public bool isDead;
    Transform trans;
    SkinnedMeshRenderer[] meshs;
    Rigidbody rigid;
    Animator anim;
    bool isWalk;
    float DeadCount;
    public int speed;
    public Vector3 movevec;
    private AudioSource audiosoruce;
    [SerializeField] private AudioClip[] clip;
    void Awake()
    {
        audiosoruce = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        StartCoroutine(Action());
        Invoke("Move", 2);
        meshs = GetComponentsInChildren<SkinnedMeshRenderer>();
        rigid = GetComponent<Rigidbody>();
        trans = GetComponent<Transform>();
    }

    void Update()
    {
        HpBar();

        if (isWalk && !isDead)
        {
            this.transform.position += movevec * speed * Time.deltaTime;

        }
        if (!isDead)
        {
            transform.LookAt(transform.position + movevec);
        }
        if (isDead)
        {

            DeadCount += Time.deltaTime;
            anim.enabled = false;
            if (DeadCount < 0.5)
            {
                trans.Rotate(new Vector3(0, 0, -5f));

            }
            movevec = Vector3.zero;
        }
    }
    void HpBar()
    {
        if (curHealth <= 0)
        {
            hpbar.rectTransform.localScale = new Vector3(0,1,1);
        }
        else
        {
            hpbar.rectTransform.localScale = new Vector3((float)curHealth / (float)maxHealth, 1, 1);
        }
    }
    void Move()
    {
        speed = Random.Range(1, 3);
        movevec = new Vector3(Random.Range(0, 3), 0, Random.Range(0, 3)).normalized;
        Invoke("Move", 2);
    }



    IEnumerator Action()
    {
        yield return new WaitForSeconds(0.1f);
        switch (enumType)
        {
            case Type.A:
                int random = Random.Range(0, 3);
                switch (random)
                {
                    case 0:
                        StartCoroutine(TurnHead());
                        break;
                    case 1:
                        StartCoroutine(Eat());
                        break;
                    case 2:
                        StartCoroutine(Walk());
                        break;
                }
                break;
            case Type.B:
                int ran = Random.Range(0, 2);
                switch (ran)
                {
                    case 0:
                        StartCoroutine(Walk());
                        break;
                    case 1:
                        StartCoroutine(Eat());
                        break;
                }
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

            StartCoroutine(OnDamage());
        }

        if (other.tag == "MagicArrow")
        {
            MagicArrow magic = other.GetComponent<MagicArrow>();

            curHealth -= magic.Damage;
            StartCoroutine(OnDamage());
        }

        if (other.tag == "MagicArea")
        {
            MagicArea magic = other.GetComponent<MagicArea>();

            curHealth -= magic.Damage;
            StartCoroutine(OnDamage());
        }

    }

    IEnumerator OnDamage()
    {
        foreach (SkinnedMeshRenderer mesh in meshs)
        {
            audiosoruce.clip = clip[0];
            audiosoruce.Play();
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
        else if (curHealth <= 0)
        {
            foreach (SkinnedMeshRenderer mesh in meshs)
            {
                audiosoruce.clip = clip[1];
                audiosoruce.Play();
                mesh.material.color = Color.gray;
                gameObject.layer = 10;
                isDead = true;
                QuestManager.questManager.ChickenPoint++;
            }

            Destroy(gameObject, 4);
        }
    }


}
