using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Transform[] enemyzone;
    public GameObject[] Chicken;
    public List<int> enemylist;
    GameObject instantEnemy;
    public List<GameObject> enemychickenlist;
    void Awake()
    {
        enemychickenlist = new List<GameObject>();
        enemylist = new List<int>();
    }
    public void enemyStart()
    {
        StartCoroutine("ChickenZone");   
    }
    IEnumerator ChickenZone()
    {
        for (int i = 0; i < 7; i++)
        {
            int ran = Random.Range(0, 2);
            enemylist.Add(ran);
        }
        while (enemylist.Count > 0)
        {
            for (int i = 0; i < 7; i++)
            {
                instantEnemy = Instantiate(Chicken[enemylist[0]],
                    enemyzone[i].position, enemyzone[i].rotation);
                enemylist.RemoveAt(0);
                enemychickenlist.Add(instantEnemy);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void enemyEnd()
    {

        for (int i = 0; i < 7; i++)
        {
            Destroy(enemychickenlist[i]);
        }
        enemychickenlist.Clear();
    }
}
