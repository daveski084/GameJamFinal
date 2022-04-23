using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlaskUI : MonoBehaviour
{
    public GameObject redFlask, greenFlask, blueFlask;

    // Start is called before the first frame update
    void Start()
    {
        redFlask.SetActive(false);
        greenFlask.SetActive(false);
        blueFlask.SetActive(false); 
    }

    private void FixedUpdate()
    {
        if(Player.hasCourage)
        {
            ActivateGreen();
        }
        if(Player.hasWisdom)
        {
            ActivateBlue();
        }
        if(Player.hasPower)
        {
            ActivateRed();
        }
    }

    public void ActivateRed()
    {
        redFlask.SetActive(true);
    }

    public void ActivateBlue()
    {
        blueFlask.SetActive(true);
    }

    public void ActivateGreen()
    {
        greenFlask.SetActive(true);
    }
   
}
