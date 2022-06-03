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
    public Transform magic4Pos;
    public GameObject magic1;
    public GameObject magic2;
    public GameObject magic3;
    public GameObject magic4;
    public GameObject magicRStart;
    GameObject magicRstart;
    public GameObject magicRsucces;
    public enum Type {Melee,Range};
    public Type type;
    public bool cast;



    public void Swing()
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
        Instantiate(magic4, magic4Pos.position, magic4Pos.rotation);
    }
    public void MagicRstart()
    {
        StartCoroutine("SkillRstart");
    }
    IEnumerator SkillRstart()   
    {
        yield return new WaitForSeconds(0.0005f);
        magicRstart = Instantiate(magicRStart, magic2Pos.position, magic2Pos.rotation);
    }
    public void MagicRUp()
    {
        cast = false;
        Destroy(magicRstart);
    }
    public void MagicRsucces()
    {
        StartCoroutine("SkillRsucces");
    }
    IEnumerator SkillRsucces()
    {
        yield return new WaitForSeconds(0.1f);
        Instantiate(magicRsucces, magic2Pos.position, magic2Pos.rotation);
    }
}
