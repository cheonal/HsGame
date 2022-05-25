using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrow : MonoBehaviour
{
    public int Damage;
    public GameObject Explode;
    public GameObject MagicObj;
    Rigidbody rigid;
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    void Start()
    {
        MagicObj = Instantiate(MagicObj, transform.position, transform.rotation);
        rigid.velocity = transform.forward * 50;       
    }
    void Update()
    {
        MagicObj.transform.position = gameObject.transform.position;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            Explode = Instantiate(Explode, transform.position, transform.rotation);
            rigid.velocity = Vector3.zero;
            Destroy(MagicObj, 1.2f);
            Destroy(Explode, 2f);
            Destroy(gameObject, 1f);
        }

        if (other.gameObject.tag == "Enemy")
        {
            Explode = Instantiate(Explode, transform.position, transform.rotation);
            rigid.velocity = Vector3.zero;
            Destroy(MagicObj, 1.2f);
            Destroy(Explode, 2f);
            Destroy(gameObject, 1f);
        }
    }

    

}
