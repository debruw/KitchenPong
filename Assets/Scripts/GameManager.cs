using System.Collections;
using System.Collections.Generic;
using TapticPlugin;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    public GameObject soundManagerPrefab;

    [HideInInspector]
    public GameObject selecTedPan;
    public bool isGameOver = false;
    public int currentLevel = 0;
    int maxLevelNumber = 20;
    GameObject currentLevelObject;
    LevelProperties currentLevelProperties;
    public GameObject StartScene;

    #region UI Elements
    public Text LevelText;
    public Slider MovementSlider, RotationSlider;
    public GameObject SliderAndButton, inGamePanel;
    public GameObject WinPanel, LosePanel, startPanel, transition;
    public GameObject VibrationButton;
    public Sprite VibrateOn, VibrateOff;
    #endregion

    [Header("Tutorial things")]
    public GameObject Tutorial1;
    public GameObject Tuto11, Tuto12;
    public GameObject Tutorial2;
    public GameObject Tuto21, Tuto22;
    public GameObject Tutorial3;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        if (PlayerPrefs.HasKey("LevelId"))
        {
            currentLevel = PlayerPrefs.GetInt("LevelId");
        }
        else
        {
            PlayerPrefs.SetInt("LevelId", currentLevel);
        }
        if (!PlayerPrefs.HasKey("VIBRATION"))
        {
            PlayerPrefs.SetInt("VIBRATION", 1);
            VibrationButton.transform.GetChild(0).GetComponent<Image>().sprite = VibrateOn;
        }
        if (SoundManager.Instance == null)
        {
            Instantiate(soundManagerPrefab);
        }

        //TODO Test için konuldu kaldırılacak
        //currentLevel = 10;

        if (currentLevel > maxLevelNumber)
        {
            int rand = Random.Range(0, maxLevelNumber);
            if (rand == PlayerPrefs.GetInt("LastLevel"))
            {
                rand = Random.Range(0, maxLevelNumber);
            }
            PlayerPrefs.SetInt("LastLevel", rand);
            currentLevelObject = Instantiate(Resources.Load("Level" + rand), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            currentLevelProperties = currentLevelObject.GetComponent<LevelProperties>();
        }
        else
        {
            Debug.Log(currentLevel);
            currentLevelObject = Instantiate(Resources.Load("Level" + currentLevel), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            currentLevelProperties = currentLevelObject.GetComponent<LevelProperties>();
        }

        if (PlayerPrefs.GetInt("UseMenu").Equals(1) || !PlayerPrefs.HasKey("UseMenu"))
        {
            startPanel.SetActive(true);
            currentLevelObject.SetActive(false);
            PlayerPrefs.SetInt("UseMenu", 1);
        }
        else if (PlayerPrefs.GetInt("UseMenu").Equals(0))
        {
            startPanel.SetActive(false);
            StartScene.SetActive(false);
            inGamePanel.SetActive(true);
            if (currentLevel == 0)
            {
                Tutorial1.SetActive(true);
                Tuto11.SetActive(true);
                Tuto12.SetActive(false);
            }
            else if (currentLevel == 5)
            {
                Tutorial2.SetActive(true);
                Tuto21.SetActive(true);
                Tuto22.SetActive(false);
            }
            else if (currentLevel == 10)
            {
                Tutorial3.SetActive(true);
            }
        }

        LevelText.text = "LEVEL " + (currentLevel + 1);
        if (currentLevel > 4)
        {
            RotationSlider.gameObject.SetActive(true);
        }
    }

    public int CollectedCoinCount;
    public Text CoinText;
    private void Start()
    {
        CollectedCoinCount = PlayerPrefs.GetInt("GlobalCoinCount");
        CoinText.text = CollectedCoinCount.ToString();
    }

    public void GameWin()
    {
        isGameOver = true;
        currentLevel++;
        PlayerPrefs.SetInt("LevelId", currentLevel);
        WinPanel.SetActive(true);
        PlayerPrefs.SetInt("GlobalCoinCount", CollectedCoinCount);
        SliderAndButton.SetActive(false);
        if (PlayerPrefs.GetInt("VIBRATION") == 1)
            TapticManager.Impact(ImpactFeedback.Light);
    }

    public void GameLose()
    {
        isGameOver = true;
        LosePanel.SetActive(true);
        SliderAndButton.SetActive(false);
        if (PlayerPrefs.GetInt("VIBRATION") == 1)
            TapticManager.Impact(ImpactFeedback.Light);
    }

    public void Cheer()
    {
        currentLevelProperties.CheerPeoples();
    }

    public void Yell()
    {
        currentLevelProperties.YellPeoples();
    }

    public void MoveSelectedPan()
    {
        selecTedPan.transform.localPosition = new Vector3(selecTedPan.transform.localPosition.x, MovementSlider.value, selecTedPan.transform.localPosition.z);
        if (Tuto12.activeSelf)
        {
            Tutorial1.SetActive(false);
        }        
        if (Tutorial3.activeSelf)
        {
            Tutorial3.SetActive(false);
        }
    }

    public void RotateSelectedPan()
    {
        selecTedPan.transform.localRotation = new Quaternion(RotationSlider.value, selecTedPan.transform.localRotation.y, selecTedPan.transform.localRotation.z, selecTedPan.transform.localRotation.w);
        if (Tuto22.activeSelf)
        {
            Tutorial2.SetActive(false);
        }
    }

    private void OnApplicationPause(bool pause)
    {
        Debug.Log("Quit");
        PlayerPrefs.SetInt("UseMenu", 1);
    }

    public void StartButtonClick()
    {
        transition.GetComponent<Animator>().SetTrigger("StartTransition");
        if (PlayerPrefs.GetInt("VIBRATION") == 1)
            TapticManager.Impact(ImpactFeedback.Light);
        StartCoroutine(WaitAndStart());
    }

    IEnumerator WaitAndStart()
    {
        yield return new WaitForSeconds(.5f);
        startPanel.SetActive(false);
        StartScene.SetActive(false);
        currentLevelObject.gameObject.SetActive(true);
        inGamePanel.SetActive(true);
        if (currentLevel == 0)
        {
            Tutorial1.SetActive(true);
            Tuto11.SetActive(true);
            Tuto12.SetActive(false);
        }
        else if (currentLevel == 5)
        {
            Tutorial2.SetActive(true);
            Tuto21.SetActive(true);
            Tuto22.SetActive(false);
        }
        else if (currentLevel == 10)
        {
            Tutorial3.SetActive(true);
        }
    }

    public void OkayButtonClick()
    {
        selecTedPan.GetComponent<SelectablePan>().CloseOutlines();
        selecTedPan = null;
    }

    public void NextRetryButtonClick()
    {
        transition.GetComponent<Animator>().SetTrigger("StartTransition");
        if (PlayerPrefs.GetInt("VIBRATION") == 1)
            TapticManager.Impact(ImpactFeedback.Light);
        PlayerPrefs.SetInt("UseMenu", 0);
        SceneManager.LoadScene("Scene_Game");
        Time.timeScale = 1f;
    }

    public void VibrateButtonClick()
    {
        if (PlayerPrefs.GetInt("VIBRATION").Equals(1))
        {//Vibration is on
            PlayerPrefs.SetInt("VIBRATION", 0);
            VibrationButton.transform.GetChild(0).GetComponent<Image>().sprite = VibrateOff;
        }
        else
        {//Vibration is off
            PlayerPrefs.SetInt("VIBRATION", 1);
            VibrationButton.transform.GetChild(0).GetComponent<Image>().sprite = VibrateOn;
        }

        if (PlayerPrefs.GetInt("VIBRATION") == 1)
            TapticManager.Impact(ImpactFeedback.Light);
    }
}
