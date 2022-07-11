using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public enum Type { A, B };
    public Type enumType;
    public Transform[] enemyzone;
    public GameObject[] Enemy;
    public List<int> enemylist;
    GameObject instantEnemy;
    public List<GameObject> enemyObjlist;
    void Awake()
    {
        enemyObjlist = new List<GameObject>();
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
        while (enemylist.Count > 0 && enemylist.Count < 10)
        {
            for (int i = 0; i < 7; i++)
            {
                instantEnemy = Instantiate(Enemy[enemylist[0]],
                    enemyzone[i].position, enemyzone[i].rotation);
                enemylist.RemoveAt(0);
                enemyObjlist.Add(instantEnemy);
            }
            yield return new WaitForSeconds(1f);
        }
    }
    public void enemyEnd()
    {
        for (int i = 0; i < 7; i++)
        {
            Destroy(enemyObjlist[i]);
        }
        enemyObjlist.Clear();
    }
}
