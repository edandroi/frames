using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFrameFollower : MonoBehaviour
{
    
    private SpriteRenderer m_SpriteRenderer;
    private Vector2 orgBoxPos = Vector2.zero;
    private Vector2 endBoxPos = Vector2.zero;

    private Vector2 upCorner;
    private Vector2 downCorner;

    private void Awake()
    {
        gameObject.AddComponent<BoxCollider2D>();
        gameObject.GetComponent<BoxCollider2D>().autoTiling = true;
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.drawMode = SpriteDrawMode.Tiled;
        m_SpriteRenderer.size = Vector2.zero;
    }

    void Update()
    {
        if (Services.PlayerSquare.drawSquare)
        {
            DrawingSquare(); 
            SquareCoordinates();
        }
    }

    void DrawingSquare()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            orgBoxPos = Input.mousePosition;
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(m_SpriteRenderer.size != Vector2.zero){}
            m_SpriteRenderer.size = Vector2.zero;
            transform.position = Camera.main.ScreenToWorldPoint(orgBoxPos) + new Vector3(0, 0, 10);
        }
        
        if (Input.GetKey(KeyCode.Mouse0))
            endBoxPos = Input.mousePosition;

        Vector2 startPos = Camera.main.ScreenToWorldPoint(orgBoxPos);
        Vector2 endPos = Camera.main.ScreenToWorldPoint(endBoxPos);
        
        m_SpriteRenderer.size = new Vector3(endPos.x - startPos.x , startPos.y- endPos.y  , 0);
    }

    void SquareCoordinates()
    {
        float xVal = m_SpriteRenderer.bounds.extents.x;
        float yVal = m_SpriteRenderer.bounds.extents.y;

        upCorner = transform.position;
        downCorner = transform.position + new Vector3(xVal*2, -yVal*2, 0);
    }

    public Vector3 downPos()
    {
        return downCorner;
    }
}
