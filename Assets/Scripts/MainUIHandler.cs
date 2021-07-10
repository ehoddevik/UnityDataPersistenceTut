using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIHandler : MonoBehaviour
{
    public void SeeScores()
    {
        SceneManager.LoadScene(2);
    }
}
