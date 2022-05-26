using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public BoxCollider meleeArea;
    public TrailRenderer traileffect;
    public int Damage;
    public Transform magic1Pos;
    public Transform magic2Pos;

    public GameObject magic1;
    public GameObject magic2;
    public GameObject magic3;
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
        Instantiate(magic1, magic1Pos.position, magic1Pos.rotation);
    }
    IEnumerator Skill2()
    {
        yield return new WaitForSeconds(0.1f);
        Instantiate(magic2, magic2Pos.position, magic2Pos.rotation);
    }
    IEnumerator Skill3()
    {
        yield return new WaitForSeconds(0.1f);
        Instantiate(magic3, magic2Pos.position, magic2Pos.rotation);
    }
    IEnumerator Skill4()
    {
        yield return new WaitForSeconds(0.1f);
    }
    IEnumerator SkillR()
    {
        yield return new WaitForSeconds(0.1f);
    }
}
