using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Barrier : MonoBehaviour
{
    public GameObject claireCanvas;

    private void Start()
    {
        claireCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Player.hasCourage && Player.hasWisdom && Player.hasPower)
        {
            SceneManager.LoadScene("Win"); 
        }
        else
        {
            claireCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        claireCanvas.SetActive(false); 
    }


}
