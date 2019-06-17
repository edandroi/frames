using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGravity : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;
    void Start()
    {
        col = gameObject.GetComponent<Collider2D>();
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    void Update()
    {
        if (Services.GameManager.goalAchieved)
        {
            rb.mass = Random.RandomRange(1f, 3f);
            rb.gravityScale = Random.RandomRange(.5f, 2f);
        }
    
    }
}
