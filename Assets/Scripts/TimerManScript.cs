using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerManScript : MonoBehaviour
{
    [SerializeField] Timer timer;

    private void Start()
    {
        timer
        .SetDuration(180)
        .OnEnd(() => SceneManager.LoadScene("GameOver"))
        .Begin();
    }

}
