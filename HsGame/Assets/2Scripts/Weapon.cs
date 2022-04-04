using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public BoxCollider meleeArea;
    public TrailRenderer traileffect;
    public int Damage;


    public void Use()
    {
        StartCoroutine("Ataack1");
    }
    IEnumerator Ataack1()
    {
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;
        traileffect.enabled = true;

        yield return new WaitForSeconds(0.6f);
        meleeArea.enabled = false;
       traileffect.enabled = false;

    }

}
