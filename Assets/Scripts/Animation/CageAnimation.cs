using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageAnimation : MonoBehaviour
{
    private Animator m_Animator;
    public Sprite cageOpen;
    private SpriteRenderer m_SpriteRenderer;
    
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Services.GameManager.goalAchieved)
        {
            m_Animator.SetTrigger("DoSwing");
            m_SpriteRenderer.sprite = cageOpen;
        }      
    }
    
    bool right = false;
    bool left = false;
    public void Swing(GameObject swingObj, float rotateAngle, float speed)
    {
        float refAngle = rotateAngle;  

        if (right)
        {
            float angle = swingObj.transform.eulerAngles.z;
         
            if (angle < rotateAngle -1)
            {
                angle = Mathf.Lerp(angle, rotateAngle, speed); 
                swingObj.transform.eulerAngles = new Vector3(0f, 0f, angle);
                Debug.Log("this is still true");
            }

            else
            {
                right = !right;
                left = !left;
            }
        }

        if (left)
        {
            float angle = swingObj.transform.eulerAngles.z;
         
            if (angle > -rotateAngle+1)
            {
                angle = Mathf.Lerp(angle, -rotateAngle, speed);   
                swingObj.transform.eulerAngles = new Vector3(0f, 0f, angle);
            }

            else 
            {
                right = !right;
                left = !left;
            }
        }
    }
}
