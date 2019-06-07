using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScripts : MonoBehaviour {
    [SerializeField]
    private Button Play;
    [SerializeField]
    private Button Quit;
    [SerializeField]
    private Button HighScore;   
 
    public void playOnClick()
    {
        Application.LoadLevel("Lv1");
    }
    public void highScoreOnClick()
    {
        Application.LoadLevel("HighScore");
    }
    public void quitOnClick()
    {
        Application.Quit();
    }
}
