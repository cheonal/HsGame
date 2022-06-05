using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArea : MonoBehaviour
{
    public int Damage;
    public GameObject MagicObj;
    SphereCollider Area;
    public enum Type { skill1, skill4 };
    public Type enumType;
    void Awake()
    {
        Area = GetComponent<SphereCollider>();
        switch (enumType)
        {
            case Type.skill1:
                StartCoroutine("Magic1Start");
                Invoke("Magic1End", 5f);
                break;

            case Type.skill4:
                StartCoroutine("Magic4Start");
                Invoke("Magic4End", 1f);
                break;
        }

    }


    void Start()
    {
        MagicObj = Instantiate(MagicObj, transform.position, transform.rotation);
    }

    IEnumerator Magic1Start()
    {
        yield return new WaitForSeconds(1f);
        Area.enabled = true;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine("Magic1Off");

    }
    IEnumerator Magic1Off()
    {
        yield return new WaitForSeconds(0.1f);
        Area.enabled = false;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine("Magic1On");
    }
    IEnumerator Magic1On()
    {
        yield return new WaitForSeconds(0.1f);
        Area.enabled = true;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine("Magic1Off");
    }

    IEnumerator Magic4Start()
    {
        yield return new WaitForSeconds(0.1f);
        Area.enabled = true;
        yield return new WaitForSeconds(0.5f);
        Area.enabled = false;
    }
    void Update()
    {
        MagicObj.transform.position = transform.position;
    }

    void Magic1End()
    {
        Destroy(gameObject);
        Destroy(MagicObj);
    }

    void Magic4End()
    {
        Destroy(gameObject);
        Destroy(MagicObj);
    }
}
