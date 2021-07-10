using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SettingsUIHandler : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public void Awake()
    {
        scoreText.text = "";
        foreach (var score in ScoreManager.Instance.highScores)
        {
            scoreText.text += score.score + "\t" + score.name + "\n";
        }
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
    #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
    #else
        Application.Quit();
    #endif
    }
}
