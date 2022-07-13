using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public static EnemySpawn enemySpawn;
    public Transform[] enemyzone;
    public GameObject[] enemies;
    public Player player;
    public List<int> enemylist;
    GameObject instantEnemy;
    public int EnemyCount;
    public List<GameObject> enemyObjlist;
    public void Start()
    {
        enemySpawn = this.GetComponent<EnemySpawn>();
        spawnstart();
    }
    void Awake()
    {
        enemyObjlist = new List<GameObject>();
        enemylist = new List<int>();
    }
    void spawnstart()
    {
        if(EnemyCount < 3 && player.isPortal)
        {
            StartCoroutine("EnemyZone");
        }
        Invoke("spawnstart", 5f);
    }
    IEnumerator EnemyZone()
    {
        for (int i = 0; i < 5; i++)
        {
            int ran = Random.Range(0, 3);
            enemylist.Add(ran);
        }
        while (enemylist.Count > 0 && EnemyCount < 5)
        {
            int ranzone = Random.Range(0, 8);
            instantEnemy = Instantiate(enemies[enemylist[0]],
            enemyzone[ranzone].position, enemyzone[ranzone].rotation);
            Enemy enemy = instantEnemy.GetComponent<Enemy>();
            enemy.Target = player.characterBody;
            enemylist.RemoveAt(0);
            enemyObjlist.Add(instantEnemy);
            EnemyCount++;
            yield return new WaitForSeconds(1f);
        }
    }
    public void enemyEnd()
    {
        StopCoroutine("EnemyZone");
        for (int i = 0; i < enemyObjlist.Count; i++)
        {
           Destroy(enemyObjlist[i]);
        }
        enemylist.Clear();
        enemyObjlist.Clear();
        EnemyCount = 0;
    }
}
