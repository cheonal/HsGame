using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public BoxCollider meleeArea;
    public TrailRenderer traileffect;
    public int Damage;
    public enum Type {Melee,Range};
    public Type type;


    public void Swing()
    {
        StartCoroutine("Ataack1");
    }
    public void Magic1()
    {
        StartCoroutine("Skill1");
    }
    public void Magic2()
    {
        StartCoroutine("Skill2");
    }
    public void Magic3()
    {
        StartCoroutine("Skill3");
    }
    public void Magic4()
    {
        StartCoroutine("Skill4");
    }
    public void MagicR()
    {
        StartCoroutine("SkillR");
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
    IEnumerator Skill1()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("succes1");
    }
    IEnumerator Skill2()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("succes2");
    }
    IEnumerator Skill3()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("succes3");
    }
    IEnumerator Skill4()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("succes4");
    }
    IEnumerator SkillR()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("succes5");
    }
}
