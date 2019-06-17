using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private DrawSquare player;
    private SpriteRenderer _Renderer;
    public GameObject titleObj;
    
    public static GameManager instance;

    public enum gameState
    {
        start, tutorial, tutorial2, melancholia, grief, parting, loneliness, nostalgia, end
    }

    public static gameState currentState;

    public bool goalAchieved = false;

    private void Awake()
    {
     
        /*
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        */
    }
    
    void Start()
    {
        if (titleObj != null)
        {
            StartCoroutine(RemoveTitle(3f));
        }
        goalAchieved = false;
 
         player = GameObject.FindObjectOfType<DrawSquare>();
         _Renderer = player.GetComponent<SpriteRenderer>();
//         detectGameState();
    }

    void Update()
    {
        if (goalAchieved)
        {
            StartCoroutine(NextScene(2.5f));
        }
    }

    IEnumerator NextScene(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
        yield return goalAchieved = false;
    }

    private TextMesh subtitle;
    void detectGameState()
    {
        if (SceneManager.GetActiveScene().name == "Start")
        {
            currentState = gameState.start;
        }  
        
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            currentState = gameState.tutorial;
        }  
        
        if (SceneManager.GetActiveScene().name == "Tutorial2")
        {
            currentState = gameState.tutorial2;
        }  
        
        if (SceneManager.GetActiveScene().name == "Melancholia")
        {
            currentState = gameState.melancholia;
            subtitle.text = "Melancholia";
        }  
        
        if (SceneManager.GetActiveScene().name == "Grief")
        {
            currentState = gameState.grief;
            subtitle.text = "Grief";
        }  
        
        if (SceneManager.GetActiveScene().name == "Parting")
        {
            currentState = gameState.parting;
            subtitle.text = "Parting";
        }  
        
        if (SceneManager.GetActiveScene().name == "Nostalgia")
        {
            currentState = gameState.nostalgia;
            subtitle.text = "Nostalgia";
        }  
        
        if (SceneManager.GetActiveScene().name == "End")
        {
            currentState = gameState.end;
        }  
    }

    IEnumerator RemoveTitle(float seconds)
    {
        var thisTitle = Instantiate(titleObj);
        yield return new WaitForSeconds(seconds);
        Destroy(thisTitle);
        yield return null;
    }
}
