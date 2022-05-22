using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    public int Damage;
    public GameObject Explode;
    public GameObject MaigicObj;
    Rigidbody rigid;
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    void Start()
    {
        MaigicObj = Instantiate(MaigicObj, transform.position, transform.rotation);
        rigid.velocity = transform.forward * 50;       
    }
    void Update()
    {
        MaigicObj.transform.position = gameObject.transform.position;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            Explode = Instantiate(Explode, transform.position, transform.rotation);
            rigid.velocity = Vector3.zero;
            Destroy(MaigicObj,0.6f);
            Destroy(Explode, 5f);
            Destroy(gameObject, 1f);
        }

        if (other.gameObject.tag == "Enemy")
        {
            Explode = Instantiate(Explode, transform.position, transform.rotation);
            rigid.velocity = Vector3.zero;
            Destroy(MaigicObj, 0.6f);
            Destroy(Explode, 2f);
            Destroy(gameObject, 1f);
        }
    }

    

}
