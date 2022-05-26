using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrow : MonoBehaviour
{
    public int Damage;
    public GameObject Explode;
    public GameObject MagicObj;
    Rigidbody rigid;
    public enum Type {skill2,skill3};
    public Type enumType;
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    void Start()
    {
        MagicObj = Instantiate(MagicObj, transform.position, transform.rotation);
        switch (enumType)
        {
            case Type.skill2:
                rigid.velocity = transform.forward * 50;
                break;

            case Type.skill3:
                rigid.velocity = transform.forward * 80;
                break;
        }
     
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
            Destroy(MagicObj, 0.5f);
            Destroy(Explode, 2f);
            Destroy(gameObject, 1f);
        }
        if (other.gameObject.tag == "Enemy")
        {
            Explode = Instantiate(Explode, transform.position, transform.rotation);
            Destroy(Explode, 2f);
            switch (enumType)
            {
                case Type.skill2:
                    {
                        rigid.velocity = Vector3.zero;
                        Destroy(MagicObj, 0.5f);
                        Destroy(gameObject, 1f);
                    }
                    break;
            }
        }
    }
}
