using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvCompleteScript : MonoBehaviour {

    [SerializeField]
    private Text Score;
    [SerializeField]
    private Text Bonus;
    [SerializeField]
    private Text Total;
    [SerializeField]
    private Button NextLevel;
    void Start ()
    {
        LoadData();
    }
    void LoadData()
    {

        Score.text = " Score : "+ PlayerPrefs.GetInt("TempScore").ToString();
        Bonus.text = "Bonus : "+ PlayerPrefs.GetInt("Bonus").ToString();
        Total.text = "Total : "+ PlayerPrefs.GetInt("TotalScore").ToString();
    }
    public void nextLevelOnClick()
    {
        Application.LoadLevel(PlayerPrefs.GetInt("GameLv")); 
    }
}
