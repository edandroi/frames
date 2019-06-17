using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeFrame : MonoBehaviour
{
    public float speed = 0.1f; //how fast it shakes
    public float amount = .3f; //how much it shakes

    void Start()
    {
        
    }

    public bool shakeNow = false;
    void Update()
    {
        if (shakeNow)
        {
            StartCoroutine(Shaking());
        }
    }

    IEnumerator Shaking()
    {
        ShakeAni();
        yield return new WaitForSecondsRealtime(1.3f);
    }

    void ShakeAni()
    {
        float shaker = Mathf.Sin(Time.time * speed) * amount;
        transform.position = new Vector3(shaker, transform.position.y, 0);
    }
}
