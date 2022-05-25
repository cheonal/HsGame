using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArea : MonoBehaviour
{
    public int Damage;
    public GameObject MagicObj;
    SphereCollider Area;
    void Awake()
    {
        Area = GetComponent<SphereCollider>();
        StartCoroutine("MagicStart");
        Invoke("MagicEnd",10f);
    }


    void Start()
    {
        MagicObj = Instantiate(MagicObj, transform.position, transform.rotation);

    }

    IEnumerator MagicStart()
    {
        yield return new WaitForSeconds(1f);
        Area.enabled = true;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine("MagicOff");

    }
    IEnumerator MagicOff()
    {
        yield return new WaitForSeconds(0.1f);
        Area.enabled = false;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine("MagicOn");
    }
    IEnumerator MagicOn()
    {
        yield return new WaitForSeconds(0.1f);
        Area.enabled = true;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine("MagicOff");
    }
    void Update()
    {
        MagicObj.transform.position = transform.position;
    }

    void MagicEnd()
    {
        Destroy(gameObject);
        Destroy(MagicObj);
    }
}
