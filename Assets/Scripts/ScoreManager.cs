using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public string player;

    [System.Serializable]
    public class ScoreEntry
    {
        public string name;
        public int score;
    }

    [System.Serializable]
    public class SerializableList<T>
    {
        public List<T> innerList;
    }

    public ScoreEntry topScore;

    public class Comp : IComparer<ScoreEntry>
    {
        public int Compare(ScoreEntry lhs, ScoreEntry rhs)
        {
            return rhs.score - lhs.score;
        }
    }

    public List<ScoreEntry> highScores;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        player = "";
        highScores = LoadScores();
        topScore = highScores[0];
    }

    public void UpdateHighScore(int nScore)
    {
        highScores.Sort(new Comp());
        if (nScore > highScores[highScores.Count-1].score)
        {
            ScoreEntry newHigh = new ScoreEntry
            {
                name = player,
                score = nScore
            };
            highScores.Add(newHigh);
            highScores.Sort(new Comp());
            while (highScores.Count > 10)
            {
                highScores.RemoveAt(highScores.Count - 1);
            }
        }
        topScore = highScores[0];
    }

    public void SaveScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        SerializableList<ScoreEntry> wrapperList = new SerializableList<ScoreEntry>
        {
            innerList = highScores
        };

        string json = JsonUtility.ToJson(wrapperList);
        Debug.Log("Writing JSON to " + path + ": \n" + json + "\n" + highScores);

        File.WriteAllText(path, json);

    }

    public List<ScoreEntry> LoadScores()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<SerializableList<ScoreEntry>>(json);

            data.innerList.Sort(new Comp());

            return data.innerList;
        } 
        else
        {
            ScoreEntry defaultData = new ScoreEntry
            {
                score = 0,
                name = "AAA"
            };
            List<ScoreEntry> emptyList = new List<ScoreEntry>();
            emptyList.Add(defaultData);

            return emptyList;
        }
    }
}
