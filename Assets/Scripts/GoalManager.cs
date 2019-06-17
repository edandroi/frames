using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    private List<GameObject> goalsList;
    private int numOfGoals;
    private int goalsAccomplished = 0;
    void Start()
    {
        goalsList = new List<GameObject>();
        foreach (GameObject goalObj in GameObject.FindGameObjectsWithTag("Goal"))
        {
            goalsList.Add(goalObj);
        }

        numOfGoals = goalsList.Count;
        Debug.Log("num of goals is "+numOfGoals);
    }

    public void GoalAccomplished()
    {
        if (goalsAccomplished < numOfGoals)
        goalsAccomplished++;
    }

    void Update()
    {
//        Debug.Log("goals accomplished is "+goalsAccomplished);
        if (goalsAccomplished == numOfGoals)
        {
            Services.GameManager.goalAchieved = true;
            Services.PlayerSquare.drawSquare = false;
        }
    }
}
