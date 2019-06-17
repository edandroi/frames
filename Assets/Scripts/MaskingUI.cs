using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskingUI : MonoBehaviour
{
    private Mask m_Mask;
    public SpriteRenderer player_Renderer;

    private RectTransform m_Transform;
    
    private SpriteRenderer m_Sprite;
    private Vector2 orgBoxPos = Vector2.zero;
    private Vector2 endBoxPos = Vector2.zero;
    void Start()
    {
        m_Mask = GetComponent<Mask>();
        m_Transform = GetComponent<RectTransform>();
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
            /*
            if(transform.localScale != Vector3.zero){}
            transform.localScale = Vector2.zero;
            transform.position = Camera.main.ScreenToWorldPoint(orgBoxPos) + new Vector3(0, 0, 10);
            */


            m_Transform.anchoredPosition = orgBoxPos;
        }
        
        if (Input.GetKey(KeyCode.Mouse0))
            endBoxPos = Input.mousePosition;

        Vector2 startPos = orgBoxPos;
        Vector2 endPos = endBoxPos;
//        m_Transform.anchorMax = endBoxPos;
//        m_Transform.anchorMax= new Vector3(endPos.x - startPos.x , startPos.y- endPos.y  , 0);
    }
}
