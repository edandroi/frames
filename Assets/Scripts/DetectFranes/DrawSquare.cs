using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class DrawSquare : MonoBehaviour
{
    private SpriteRenderer m_SpriteRenderer;
    private Vector2 orgBoxPos = Vector2.zero;
    private Vector2 endBoxPos = Vector2.zero;

    private Vector2 upCorner;
    private Vector2 downCorner;

    public bool drawSquare =  true;
    
    private SpriteRenderer lastFrameRenderer;

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
        storeLastFrame(); // store last frame size and use it to generate frames below
        if (drawSquare)
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
            if(m_SpriteRenderer.size != Vector2.zero)
            m_SpriteRenderer.size = Vector2.zero;
            
            transform.position = Camera.main.ScreenToWorldPoint(orgBoxPos) + new Vector3(0, 0, 10);
        }
        
        if (Input.GetKey(KeyCode.Mouse0))
            endBoxPos = Input.mousePosition;
        
        if (Input.GetMouseButtonUp(0))
            orgBoxPos = Input.mousePosition;

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


    private Vector2 lastFrameSize;
    void storeLastFrame()
    {
        if (m_SpriteRenderer.size.magnitude != 0)
        lastFrameSize = m_SpriteRenderer.size;
    }

    public void GenerateSquare()
    {
        var newFrame = new GameObject();
        newFrame.name = "NewFrame";
        SpriteRenderer renderer = newFrame.AddComponent<SpriteRenderer>();
        newFrame.transform.position = transform.position;
        renderer.sprite = m_SpriteRenderer.sprite;
        renderer.drawMode = SpriteDrawMode.Tiled;
        renderer.size = lastFrameSize;
        renderer.material = m_SpriteRenderer.material;
        renderer.color = m_SpriteRenderer.color;
        renderer.sortingOrder = 1;
        m_SpriteRenderer.size = Vector2.zero;
    }

    // return right bottom corner
    public Vector3 downPos()
    {
        return downCorner;
    }
}
