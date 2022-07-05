using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrow : MonoBehaviour
{
    public float Damage;
    public GameObject Explode;
    public GameObject MagicObj;
    public static MagicArrow magicArrow;
    Rigidbody rigid;
    public enum Type {skill2,skill3,skill5};
    public Type enumType;
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        magicArrow = this.GetComponent<MagicArrow>();
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
            case Type.skill5:
                rigid.velocity = transform.forward * 100;
                break;
        }
    }
    void Update()
    {
        MagicObj.transform.position = gameObject.transform.position;
        switch (enumType)
        {
            case Type.skill2:
                Damage = 50 + (50 * (PlayerStateUi.PlayerUI.DamageUp * (float)0.1));
                break;

            case Type.skill3:
                Damage = 30 + (30 * (PlayerStateUi.PlayerUI.DamageUp * (float)0.1));
                break;
            case Type.skill5:
                Damage = 200 + (200 * (PlayerStateUi.PlayerUI.DamageUp * (float)0.1));
                break;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            Explode = Instantiate(Explode, transform.position, transform.rotation);
            rigid.velocity = Vector3.zero;
            Destroy(MagicObj, 0.5f);
            Destroy(Explode, 2f);
            Destroy(gameObject, 0.5f);
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
                        Destroy(gameObject, 0.5f);
                    }
                    break;
            }
        }
    }
}
