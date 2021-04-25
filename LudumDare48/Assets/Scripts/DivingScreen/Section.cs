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
    public List<Transform> treasureSpawnLocations = new List<Transform>();
    private int myOrder;

    public void SetOrder(int order, Vector3 topConnection)
    {
        myOrder = order;
        transform.position = topConnection - top.localPosition;
    }

    public void PopulateBasedOnOrder()
    {
        EnemyDepthInfo enemies = WorldManager.Instance.enemyInfo[myOrder];
        TreasureDepthInfo treasures = WorldManager.Instance.treasureInfo[myOrder];

        foreach(Transform t in enemySpawnLocations)
        {
            float random = UnityEngine.Random.value * 100;
            //enemies.myEnemies.Where(x => x.)
        }
    }
}
