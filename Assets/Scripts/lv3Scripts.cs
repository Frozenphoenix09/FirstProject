using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lv3Scripts : MonoBehaviour {


    [SerializeField]
    private GameObject Button;
    [SerializeField]
    Transform Panel;
    [SerializeField]
    private Sprite BgImage;
    private List<Button> BtnList = new List<Button>();
    [SerializeField]
    private List<Sprite> SpriteList = new List<Sprite>();
    private Sprite[] SourceSprite;
    GameObject btn;
    [SerializeField]
    Text Score;
    [SerializeField]
    Text Moves;
    [SerializeField]
    private AudioClip RightSound;
    [SerializeField]
    private AudioClip WrongSound;
    private AudioSource Source;
    private bool FirstClick, SecondClick;
    private string FirstName, SecondName;
    int FirstIndex, SecondIndex;

    private int GameLevel = 3;
    private int MatchGuess, MoveRemaining = 40, TempScore = 0, Bonus, TotalScore, HighScore;
    private void Awake()
    {
        CreateButton();
        LoadSprites();
        Shuffle(SpriteList);
        DataSaveAndLoad();
    }
    // Use this for initialization
    void Start()
    {
        AddListener();
    }
    void AddListener()
    {
        foreach (Button btn in BtnList)
        {
            btn.onClick.AddListener(() => ButtonOnClick());
        }
    }
    void DataSaveAndLoad()
    {
        /**PlayerPrefs.SetString("PlayerName", "");
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("TempScore", 0);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("GameLv", 0);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("Bonus", 0);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("TotalScore", 0);
        PlayerPrefs.Save();**/
        TempScore = PlayerPrefs.GetInt("TotalScore");
        HighScore = PlayerPrefs.GetInt("HighScore");
        Moves.text = "Moves Remaining : " + MoveRemaining.ToString();
        Score.text = "Score : " + TempScore.ToString();
        Source = GetComponent<AudioSource>();
    }
    void CreateButton()
    {
        for (int i = 0; i < 30; i++)
        {
            btn = Instantiate(Button);
            btn.name = "" + i;
            btn.transform.SetParent(Panel, false);
        }
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Button");
        for (int i = 0; i < obj.Length; i++)
        {
            BtnList.Add(obj[i].GetComponent<Button>());
        }
    }
    void LoadSprites()
    {
        SourceSprite = Resources.LoadAll<Sprite>("Sprites/AllSprites");
        int size = BtnList.Count;
        int index = 0;
        for (int i = 0; i < size; i++)
        {
            if (i == size / 2)
            {
                index = 0;
            }
            SpriteList.Add(SourceSprite[index]);
            index++;
        }
    }
    void ButtonOnClick()
    {
        if (!FirstClick)
        {
            FirstClick = true;
            FirstIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            FirstName = SpriteList[FirstIndex].name;
            BtnList[FirstIndex].image.sprite = SpriteList[FirstIndex];
            Debug.Log("First index is : " + FirstIndex + "First Name is : " + FirstName);
        }
        else if (!SecondClick)
        {
            SecondClick = true;
            SecondIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            SecondName = SpriteList[SecondIndex].name;
            BtnList[SecondIndex].image.sprite = SpriteList[SecondIndex];
            Debug.Log("Second Index is : " + SecondIndex + "SecondName is : " + SecondName);
            StartCoroutine(Check());
        }
    }
    IEnumerator Check()
    {
        yield return new WaitForSeconds(0.3f);
        if (FirstIndex != SecondIndex && FirstName == SecondName)
        {
            BtnList[FirstIndex].interactable = false;
            BtnList[SecondIndex].interactable = false;

            BtnList[FirstIndex].image.color = new Color(0, 0, 0, 0);
            BtnList[SecondIndex].image.color = new Color(0, 0, 0, 0);

            Source.PlayOneShot(RightSound);
            TempScore += 20;
            MoveRemaining--;
            MatchGuess++;
        }
        else if (FirstIndex == SecondIndex)
        {
            BtnList[FirstIndex].image.sprite = BgImage;
            BtnList[SecondIndex].image.sprite = BgImage;
        }
        else
        {
            BtnList[FirstIndex].image.sprite = BgImage;
            BtnList[SecondIndex].image.sprite = BgImage;

            Source.PlayOneShot(WrongSound);
            TempScore -= 5;
            MoveRemaining--;
        }
        FirstClick = SecondClick = false;
        if (TempScore <= 0)
        {
            TempScore = 0;
            // Score.text ="Score :" + TempScore.ToString();
        }

        Moves.text = "Move Remaining : " + MoveRemaining.ToString();
        Score.text = "Score :" + TempScore.ToString();
        CheckFinish();
    }
    void Shuffle(List<Sprite> list)
    {
        Sprite temp;
        for (int i = 0; i < list.Count; i++)
        {
            temp = list[i];
            int rand = Random.Range(i, list.Count);
            list[i] = list[rand];
            list[rand] = temp;
        }
    }
    void CheckFinish()
    {
        if (MoveRemaining >= 0 && MatchGuess == BtnList.Count / 2)
        {
            Debug.Log(" You Win ! ");
            StartCoroutine(Report());

        }
        if (MoveRemaining <= 0 && MatchGuess < BtnList.Count / 2)
        {
            Debug.Log(" You Lose ! ");
            TotalScore = TempScore;
            PlayerPrefs.SetInt("TotalScore", TotalScore);
            PlayerPrefs.Save();
            if (TotalScore > HighScore)
            {
                HighScore = TotalScore;
                PlayerPrefs.SetInt("HighScore", HighScore);
                PlayerPrefs.Save();
                Application.LoadLevel("NewHighScore");
            }
            else
                Application.LoadLevel("YouLose");
        }
    }
    IEnumerator Report()
    {
        yield return new WaitForSeconds(0.3f);
        GameLevel++;
        PlayerPrefs.SetInt("GameLv", GameLevel);
        PlayerPrefs.Save();
        Bonus = MoveRemaining * 10;
        PlayerPrefs.SetInt("Bonus", Bonus);
        PlayerPrefs.Save();
        TotalScore = TempScore + Bonus;
        PlayerPrefs.SetInt("TotalScore", TotalScore);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("TempScore", TempScore);
        PlayerPrefs.Save();
        Application.LoadLevel("LevelComplete");
    }
}
