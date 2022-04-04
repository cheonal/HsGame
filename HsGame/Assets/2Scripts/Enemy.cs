using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;
    public MeshRenderer [] meshs;
    BoxCollider BoxCollider;
    
    void Awake()
    {
        meshs = GetComponentsInChildren<MeshRenderer>();
        BoxCollider = GetComponent<BoxCollider>();
        Weapon weapon = GetComponent<Weapon>();
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


      foreach (MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.white;
        }
      


    }
}
