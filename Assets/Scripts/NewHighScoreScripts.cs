using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewHighScoreScripts : MonoBehaviour {

    [SerializeField]
    private InputField playerName;
	public void confirmOnclick()
    {
        PlayerPrefs.SetString("PlayerName", playerName.text);
        PlayerPrefs.Save();
        Application.LoadLevel("HighScore");
    }
   
}
