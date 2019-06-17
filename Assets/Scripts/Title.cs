using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    private TextMeshPro m_Text;
    void Start()
    {
        m_Text = GetComponent<TextMeshPro>();
        m_Text.text = SceneManager.GetActiveScene().name;       
    }
}
