using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfterGoal : MonoBehaviour
{
    void Update()
    {
        if (Services.GameManager.goalAchieved)
        {
            Destroy(gameObject);
        }
    }
}
