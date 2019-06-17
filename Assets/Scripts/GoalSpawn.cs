using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalSpawn : MonoBehaviour
{
    public GameObject achievementSpawn;

    public Vector3 spawnPos;

    bool spawned = false;
    public bool spawnNow = false;
    void Update()
    {
        if (achievementSpawn != null)
        {
            if (!spawned)
            {
                if (spawnNow)
                {
//                    Instantiate(achievementSpawn, spawnPos, Quaternion.identity);
                    Instantiate(achievementSpawn);
                    spawned = true;
                }
            } 
        }
    }

}
