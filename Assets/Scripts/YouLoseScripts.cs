using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YouLoseScripts : MonoBehaviour {
    [SerializeField]
    private Button TryAgain;
    [SerializeField]
    private Button Quit;
    [SerializeField]
    private Text Score;
    // Use this for initialization
    void Start ()
    {
        Score.text = PlayerPrefs.GetInt("TotalScore").ToString();
	}
    public void tryAgainOnClick()
    {
        Application.LoadLevel("Menu");
    }
    public void quitOnClick()
    {
        Application.Quit();
    }



}
