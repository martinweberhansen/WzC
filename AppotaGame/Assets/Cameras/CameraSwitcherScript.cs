using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraSwitcherScript : MonoBehaviour {
    
    public Camera cam1;
    public Camera cam2;
    private GameObject pointerArrow;
    private GameObject wizzard;
    private bool HasButtonJustBeenPushed;

    [SerializeField]
    GameObject inGameMenu;
    [SerializeField]
    GameObject exitPopup;
    [SerializeField]
    GameObject GameOverPopup;


    void Start()
    {
        cam1.enabled = true;
        cam2.enabled = false;
        pointerArrow = GameObject.Find("pointerArrow");
        wizzard = GameObject.Find("wizzard");
        HasButtonJustBeenPushed = false;
    }

    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.C) ||  (Input.touchCount == 4) && !HasButtonJustBeenPushed)
         
        {
            if (cam1.enabled)
            {
                cam1.gameObject.SetActive(false);
                cam2.gameObject.SetActive(true);
                InitializeTowerCamAttributes();
                cam1.GetComponent<AudioListener>().enabled = false;
                cam2.GetComponent<AudioListener>().enabled = true;

            }
            else
            {
                cam1.gameObject.SetActive(true);
                cam2.gameObject.SetActive(false);
                ShowMainCamButtons();
                cam1.GetComponent<AudioListener>().enabled = true;
                cam2.GetComponent<AudioListener>().enabled = false;
            }
            cam1.enabled = !cam1.enabled;
            cam2.enabled = !cam2.enabled;
            StartCoroutine("WaitHalfSecond");
        }
    }

    private void InitializeTowerCamAttributes()
    {
        pointerArrow.SetActive(true);
        wizzard.GetComponent<MagicController>().SetWizzardState(0);
    }

    private void ShowMainCamButtons()
    {
        pointerArrow.SetActive(false);
    }

    IEnumerator WaitHalfSecond()
    {
        HasButtonJustBeenPushed = true;
        yield return new WaitForSeconds(0.5f);
        HasButtonJustBeenPushed = false;
    }

    public void ShowInGameMenu() // skulle nok ha heddet ToggleInGameMenu()
    {
        if (inGameMenu.activeInHierarchy)
        {
            inGameMenu.SetActive(false);
        }
        else
        {
            inGameMenu.SetActive(true);
        }
        
    }

    public void ShowExitPopup()
    {
        exitPopup.SetActive(true);
    }

    public void HideExitPopup()
    {
        exitPopup.SetActive(false);
    }

    public void ShowGameOverPopup()
    {
        GameOverPopup.SetActive(true);
    }

    public void HideGameOverPopup()
    {
        GameOverPopup.SetActive(false);
    }



    public void PlayLevelAgain(int levelID)
    {
        SceneManager.LoadScene("level" + levelID);
    }

    public void ExitToMainMenu(int levelID)//with save
    {
        if (GameAI.getGameAISingelton.HasAteamWon())
        {
            if (GameAI.getGameAISingelton.ArePlayerVictories())
            {
                WorldObject.GetworldObject.AddcurrentCoinsAndCrystalToGameWorld(GameAI.getGameAISingelton.GetCoins(), GameAI.getGameAISingelton.GetCrystals());
                WorldObject.GetworldObject.CompletedLevel(levelID);
            }
        }
        SceneManager.LoadScene("martinMenu");
    }

    public void ExitWithoutSaving()
    {
        SceneManager.LoadScene("martinMenu");
    }






}
