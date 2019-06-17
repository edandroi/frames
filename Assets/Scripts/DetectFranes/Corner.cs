using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corner : MonoBehaviour
{
    private bool cornerOverlap;
    private GameObject goalParent;

    private BoxCollider2D p_Collider;
    private Collider2D m_Collider;

    private GameObject player;
    void Start()
    {
        player = FindObjectOfType<DrawSquare>().gameObject;
        p_Collider = player.GetComponent<BoxCollider2D>();
        m_Collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        // Check is there is collision between player square and this collider
        if (m_Collider.bounds.Intersects(p_Collider.bounds))
        {
            cornerOverlap = true;
        }
        else
        {
            cornerOverlap = false;
        }
    }

    public bool overlapping()
    {
        return cornerOverlap;
    }
}
