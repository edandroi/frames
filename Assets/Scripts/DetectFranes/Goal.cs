using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    // Up and Down Corners
    public GameObject upCorner;
    public GameObject downCorner;

    private Corner upColOverlap;
    private Corner downColOverlap;
    
    private Vector3 upPos;
    private Vector3 downPos;

    private bool upIsOverlapping;
    private bool downIsOverlapping;

    private GameObject Player;
    private DrawSquare squarePlayer;
    private SpriteRenderer playerSprite;
    private SpriteRenderer m_SpriteRenderer;

    public float goalPercentage = .3f;

    private GoalAudio m_Audio;

    private GoalSpawn m_GoalSpawn; // to spawn effects etc.
    void Start()
    {
        gameObject.tag = "Goal";
        upColOverlap = upCorner.GetComponent<Corner>();
        downColOverlap = downCorner.GetComponent<Corner>();
        upPos = upCorner.transform.position;
        downPos = downCorner.transform.position;

        m_Audio = gameObject.GetComponent<GoalAudio>();
        
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.drawMode = SpriteDrawMode.Tiled;
        m_SpriteRenderer.size = new Vector3(upPos.x - downPos.x , upPos.y- downPos.y  , 0);
        
        Player = FindObjectOfType<DrawSquare>().gameObject;
        squarePlayer = Player.GetComponent<DrawSquare>();
        playerSprite = Player.GetComponent<SpriteRenderer>();

        m_GoalSpawn = GetComponent<GoalSpawn>();
    }

    void Update()
    {
        CheckOverlaps();
        CheckFrameSize();
        CheckGoal();
        
        if (Input.GetMouseButtonUp(0))
           GoalAchieved();
    }

    private bool overlapping = false;
    void CheckOverlaps()
    {
        if (upColOverlap.overlapping() && downColOverlap.overlapping())
        {
            overlapping = true;
        }
        else
        {
            overlapping = false;
        }
    }

    // Check the Frame Size
    private bool frameSize;
    void CheckFrameSize()
    {
        if (Mathf.Abs(playerSprite.size.x) > .1f && Mathf.Abs(playerSprite.size.y) > .1f)
        {          
            if (Approximately(m_SpriteRenderer.bounds.max, playerSprite.bounds.max, goalPercentage))
                frameSize = true;
            else
                frameSize = false;
        }
        else
        {
            frameSize = false;
        }
    }
    
    bool Approximately(Vector3 thisVector, Vector3 other, float percentage)
    {
        var dx = thisVector.x - other.x;
        var dy = thisVector.y - other.y;

        if (Mathf.Abs(dx) > percentage)
        {
            return false;
        }
        else if (Mathf.Abs(dy) > percentage)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    
    // Tell if the goal is achieved
    private bool goalAchieved;
    void CheckGoal()
    {
        if (overlapping && frameSize)// Check if the frame size and Collision works
        {
            goalAchieved = true;
//            Debug.Log("requirements fulfilled");
        }
        
        if (Input.GetMouseButtonDown(0) && goalAchieved)
        {
            goalAchieved = false;
        }
    }

    //Do these if goal achieved
    private bool squareIsGenerated = false;
    void GoalAchieved()
    {
        if (goalAchieved)
        { 
            if (Services.PlayerSquare.drawSquare)
                m_Audio.playSound = true;
//              Services.PlayerSquare.drawSquare = false;
            Services.GoalManager.GoalAccomplished();

            if (!squareIsGenerated) // if we haven't created a square yet, create one now
            {
                Services.PlayerSquare.GenerateSquare(); // generate a new square for solution
                squareIsGenerated = true; // there should be only one square for each right solution
            }
            m_GoalSpawn.spawnNow = true; // spawn particles etc. if they exist
        }
    }
}
