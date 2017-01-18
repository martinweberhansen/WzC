using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameCameraScript : MonoBehaviour {

    [SerializeField]
    bool                testing;
    [SerializeField]
    float               cameraMoveSpeed;
    [SerializeField]
    GameObject          WorldPivot;
    [SerializeField]
    float               mapLength;
    [SerializeField]
    List<GameObject>    enemyFlags;
    [SerializeField]
    List<GameObject>    friendlyFlags;

    TouchScreenKeyboard keyboard;
    [SerializeField]
    GameObject          InGameSpawnButtons;
    [SerializeField]
    GameObject          HudCtrl;

    [SerializeField]
    GameObject          blueFlagButtons;
    [SerializeField]
    GameObject          redFlagButtons;

    [SerializeField]
    Text                totalGold;
    [SerializeField]
    Text                totalCrystal;
    [SerializeField]
    GameObject          fader;
    private int         counter = 1800;
    Level               levelselection;
    MapSelection        mapToLoad;


    void Start ()
    {
        GameAI.getGameAISingelton.DestroySingeltonInstance();//destroy singelton før scene, ellers bliver listerne ikke updateret.
        WorldObject.GetworldObject.DestroySingeltonInstance();
        
        GameAI.getGameAISingelton.SetEnemyFlagList(enemyFlags);
        GameAI.getGameAISingelton.SetFriendlyFlagList(friendlyFlags);
        
        //ingen crash ved loading
        if (testing)
        {
            //UnityEditor.FileUtil.DeleteFileOrDirectory(Application.persistentDataPath);
            
            bool doneloadingXML = false;
            StartCoroutine(WorldObject.GetworldObject.LoadFromXML((callback) =>
            {
                if (callback)
                {
                    doneloadingXML = true;
                    LoadPartyUnitsFromXML();
                    LoadFlagImagesOnButtons();
                    
                    GameAI.getGameAISingelton.SetCoins(WorldObject.GetworldObject.worldInfo.gold);
                    GameAI.getGameAISingelton.SetCrystals(WorldObject.GetworldObject.worldInfo.crystal);
                    
                }
            }));
            while (doneloadingXML)
            {

            }
        }
        
        fader.SetActive(true);
        fader.GetComponent<Image>().color = new Color(0, 0, 0, 1);
        StartCoroutine(WorldObject.GetworldObject.ChangeScene(1, FadeDirection.Fadein, (callback) =>
        {
            if (callback)
            {
                fader.SetActive(false);
            }
        }));
    }
    
    void Update () {


        if (counter > 650)
        {
            //GameObject.Find("EnemyCastlePoint").GetComponent<EnemyManager>().Spawn();
            //GameObject.Find("EnemyCastlePoint").GetComponent<EnemyManager>().Spawn();
            GameObject.Find("EnemyCastlePoint").GetComponent<EnemyManager>().Spawn();
            counter = 0;
        }
        else
            counter++;
    }

    void LoadPartyUnitsFromXML()
    {
        foreach (Unit unit in WorldObject.GetworldObject.units)
        {
            Button[] but = InGameSpawnButtons.GetComponentsInChildren<Button>();
            but[unit.id - 1].image.overrideSprite = Resources.Load<Sprite>("CharacterAssets/helmets/pictures/helmetPicture" + unit.headID);
        }
    }

    void LoadFlagImagesOnButtons()
    {
        HudCtrl.GetComponent<HUDController>().initializeButtonImage(enemyFlags,friendlyFlags);  
    }

    public void SetEnemyFlagImageOnButton(int ButtonNumber,Sprite sprite)
    {
        Button[] redBut = redFlagButtons.GetComponentsInChildren<Button>();
        redBut[ButtonNumber - 1].image.overrideSprite = sprite;
    }

    void SetCoinsAndCrystals()
    {
        totalGold.text = WorldObject.GetworldObject.worldInfo.gold.ToString();
        totalCrystal.text = WorldObject.GetworldObject.worldInfo.crystal.ToString();
    }

    public void ShowStats()
    {
        GameAI.getGameAISingelton.ShowStats();
        WorldObject.GetworldObject.ShowStats();
    }
    
}
