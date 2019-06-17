using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Masking : MonoBehaviour
{
    private SpriteMask m_Mask;
    public SpriteRenderer m_Renderer;
    
    private SpriteRenderer m_Sprite;
    private Vector2 orgBoxPos = Vector2.zero;
    private Vector2 endBoxPos = Vector2.zero;
    void Start()
    {
        m_Mask = GetComponent<SpriteMask>();
    }

    void Update()
    {
        if (Services.PlayerSquare.drawSquare)
             DrawingSquare();
    }
    
    void DrawingSquare()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            orgBoxPos = Input.mousePosition;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(transform.localScale != Vector3.zero){}
            transform.localScale = Vector2.zero;
            transform.position = Camera.main.ScreenToWorldPoint(orgBoxPos) + new Vector3(0, 0, 10);
        }
        
        if (Input.GetKey(KeyCode.Mouse0))
            endBoxPos = Input.mousePosition;

        Vector2 startPos = Camera.main.ScreenToWorldPoint(orgBoxPos);
        Vector2 endPos = Camera.main.ScreenToWorldPoint(endBoxPos);
        
       transform.localScale= new Vector3(endPos.x - startPos.x , startPos.y- endPos.y  , 0);
    }
}
