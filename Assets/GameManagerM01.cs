using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerM01 : MonoBehaviour
{
    public GameObject[] RoomList;
    public GameObject[] EnemyList;
    public int PlayerRoomId;
    public int EnemiesKilled;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies(0, 5);
        SpawnEnemies(1, 5);
        SpawnEnemies(2, 5);
        SpawnEnemies(3, 5);
        SpawnEnemies(4, 5);
        SpawnEnemies(5, 5);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnEnemies(int roomId, int enemyCount)
    {
        GameObject room = RoomList[roomId];
        GameObject child = null;
        for (int i = 0; i < room.transform.childCount; i++)
        {
            child = room.transform.GetChild(i).gameObject;
            if (child.name.CompareTo("Spawners") == 0) break;
        }

        int spawnsCount = child.transform.childCount;

        if (enemyCount > spawnsCount) enemyCount = spawnsCount;

        for(int i = 0; i < enemyCount; i++)
        {
            GameObject enemy = RandomEnemy();
            Transform spawnPoint = child.transform.GetChild(i);
            GameObject enemyInst = Instantiate(enemy, spawnPoint.position, Quaternion.identity);
            try
            {
                enemyInst.GetComponent<EnemyController>().RoomId = roomId;
            }
            catch 
            {
                enemyInst.GetComponent<RougeLogic>().RoomId = roomId;
            }
        }
    }

    GameObject RandomEnemy()
    {
        int randomInt = Random.Range(0, EnemyList.Length);
        return EnemyList[randomInt];
    }
}
