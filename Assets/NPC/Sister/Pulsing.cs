using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulsing : MonoBehaviour
{
    private bool isAllowed;

    // Start is called before the first frame update
    void Start()
    {
        isAllowed = true;
    }

    private IEnumerator Pulse()
    {
        isAllowed = false;

        for (float i = 0f; i <= 1f; i += 0.2f)
        {
            transform.localScale = new Vector3(
                (Mathf.Lerp(transform.localScale.x, transform.localScale.x + 0.00025f, Mathf.SmoothStep(0f, 9f, i))),
                (Mathf.Lerp(transform.localScale.y, transform.localScale.y + 0.00025f, Mathf.SmoothStep(0f, 9f, i))),
                (Mathf.Lerp(transform.localScale.z, transform.localScale.z + 0.00025f, Mathf.SmoothStep(0f, 9f, i)))
                );
            yield return new WaitForSeconds(0.015f);
        }

        for (float i = 0f; i <= 1f; i += 0.2f)
        {
            transform.localScale = new Vector3(
                (Mathf.Lerp(transform.localScale.x, transform.localScale.x - 0.00025f, Mathf.SmoothStep(0f, 9f, i))),
                (Mathf.Lerp(transform.localScale.y, transform.localScale.y - 0.00025f, Mathf.SmoothStep(0f, 9f, i))),
                (Mathf.Lerp(transform.localScale.z, transform.localScale.z - 0.00025f, Mathf.SmoothStep(0f, 9f, i)))
                );
            yield return new WaitForSeconds(0.015f);
        }

        isAllowed = true; 
    }

    private void Update()
    {
        StartCoroutine(Pulse()); 
    }

}
