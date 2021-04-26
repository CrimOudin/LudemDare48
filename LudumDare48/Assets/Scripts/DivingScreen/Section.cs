using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Section : MonoBehaviour
{
    public Transform top;
    public Transform bottom;

    public List<Transform> enemySpawnLocations = new List<Transform>();
    public List<Transform> floorEnemySpawnLocations = new List<Transform>();
    public List<Transform> treasureSpawnLocations = new List<Transform>();
    private int myOrder;

    public void SetOrder(int order, Vector3 topConnection, bool firstZone, bool isBossZone = false)
    {
        myOrder = order;
        transform.position = topConnection - top.localPosition;
        if (isBossZone == false)
        {
            PopulateBasedOnOrder(firstZone);
        }
    }

    public void PopulateBasedOnOrder(bool first)
    {
        EnemyDepthInfo enemies = WorldManager.Instance.enemyInfo[myOrder];
        EnemyDepthInfo floorEnemies = WorldManager.Instance.floorEnemies[myOrder];
        TreasureDepthInfo treasures = WorldManager.Instance.treasureInfo[myOrder];

        foreach(Transform t in enemySpawnLocations)
        {
            float random = UnityEngine.Random.value * 100;
            if(random <= enemies.myEnemies.Sum(x => x.percentSpawnChance))
            {
                float sum = 0;
                EnemyInfo newEnemy = enemies.myEnemies.TakeWhile(x => { var temp = sum; sum += x.percentSpawnChance; return temp < random; }).Last();
                if(newEnemy != null)
                {
                    GameObject enemy = Instantiate(newEnemy.enemyPrefab);
                    enemy.transform.SetParent(transform);
                    enemy.transform.localScale = new Vector3(1, 1, 1);
                    enemy.transform.position = t.position;
                    enemy.transform.SetLocalPosition(z: -1);

                    if (enemy.GetComponent<Enemy>() != null)
                        enemy.GetComponent<Enemy>().SetLevel(myOrder);
                }    
            }
        }
        foreach (Transform t in floorEnemySpawnLocations)
        {
            float random = UnityEngine.Random.value * 100;
            if (first)
                random = 1;

            if (random <= floorEnemies.myEnemies.Sum(x => x.percentSpawnChance))
            {
                float sum = 0;
                EnemyInfo newEnemy = floorEnemies.myEnemies.TakeWhile(x => { var temp = sum; sum += x.percentSpawnChance; return temp < random; }).Last();
                if (newEnemy != null)
                {
                    GameObject enemy = Instantiate(newEnemy.enemyPrefab);
                    enemy.transform.SetParent(transform);
                    enemy.transform.localScale = new Vector3(1, 1, 1);
                    enemy.transform.position = t.position;
                    enemy.transform.SetLocalPosition(z: -1);

                    if (enemy.GetComponent<Enemy>() != null)
                        enemy.GetComponent<Enemy>().SetLevel(myOrder);
                }
            }
        }

        foreach (Transform t in treasureSpawnLocations)
        {
            float random = UnityEngine.Random.value * 100;
            if (first)
                random = 1;

            if (random <= treasures.myTreasures.Sum(x => x.percentSpawnChance))
            {
                float sum = 0;
                TreasureInfo newTreasure = treasures.myTreasures.TakeWhile(x => { var temp = sum; sum += x.percentSpawnChance; return temp < random; }).Last();
                if (newTreasure != null)
                {
                    GameObject enemy = Instantiate(newTreasure.treasurePrefab);
                    enemy.transform.SetParent(transform);
                    enemy.transform.localScale = new Vector3(1, 1, 1);
                    enemy.transform.position = t.position;
                    enemy.transform.SetLocalPosition(z: -1);

                    if (enemy.GetComponent<Pickup>() != null)
                        enemy.GetComponent<Pickup>().SetValue(myOrder);
                }
            }
        }
    }
}
