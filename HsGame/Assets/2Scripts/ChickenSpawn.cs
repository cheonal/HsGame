using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSpawn : MonoBehaviour
{
    public Transform[] chickenzone;
    public GameObject[] Chicken;
    public List<int> chickenlist;
    GameObject instantchicken;
    public List<GameObject> chickenObjlist;
    void Awake()
    {
        chickenObjlist = new List<GameObject>();
        chickenlist = new List<int>();
    }
    public void ChickenStart()
    {
        StartCoroutine("ChickenZone");
    }
    IEnumerator ChickenZone()
    {
        for (int r = 0; r < 7; r++)
        {
            int rann = Random.Range(0, 2);
            chickenlist.Add(rann);
        }
        while (chickenlist.Count > 0)
        {
            for (int r = 0; r < 7; r++)
            {
                instantchicken =
                    Instantiate(Chicken[chickenlist[0]], chickenzone[r].position, chickenzone[r].rotation);
                chickenlist.RemoveAt(0);
                chickenObjlist.Add(instantchicken);
            }
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(1f);
    }
    public void chickenEnd()
    {
        for (int i = 0; i < 7; i++)
        {
            Destroy(chickenObjlist[i]);
        }
        chickenObjlist.Clear();
    }
}
