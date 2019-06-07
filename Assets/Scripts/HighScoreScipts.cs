using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreScipts : MonoBehaviour {

    [SerializeField]
    private Button Back;
    [SerializeField]
    private Text PlayerName;
    [SerializeField]
    private Text Score;

    void Start ()
    {
        LoadData();
    }
    void LoadData()
    {
        PlayerName.text = PlayerPrefs.GetString("PlayerName");
        Score.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
    public void backOnClick()
    {
        Application.LoadLevel("Menu");
    }
}
