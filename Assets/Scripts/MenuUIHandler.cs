using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
using System.IO;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField nameEntry;
    public TextMeshProUGUI highScoreText;

    public void Start()
    {
        Debug.Log("MenuUIHandler.Start() ran");
        highScoreText.text = $"Best Score: {ScoreManager.Instance.topScore.name} : {ScoreManager.Instance.topScore.score}";
        if (ScoreManager.Instance.player != "")
        {
            nameEntry.text = ScoreManager.Instance.player;
        }
    }

    public void StartGame()
    {
        ScoreManager.Instance.player = nameEntry.text;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        //ScoreManager.Instance.SaveScore();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
