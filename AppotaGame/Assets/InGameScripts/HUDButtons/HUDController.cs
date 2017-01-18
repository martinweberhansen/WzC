using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class HUDController : MonoBehaviour {
    
    [SerializeField]
    GameObject blueFlagButtons;
    [SerializeField]
    GameObject redFlagButtons;

    [SerializeField]
    Text Coins_txt;
    [SerializeField]
    Text Crystals_txt;
    [SerializeField]
    GameObject inGameShop;


    void Update()
    {
        Coins_txt.text    = GameAI.getGameAISingelton.GetCoins().ToString();
        Crystals_txt.text = GameAI.getGameAISingelton.GetCrystals().ToString();
    }
    
    public void SetEnemyFlagImageOnButton(int ButtonNumber, Sprite sprite)
    {
        Button[] redBut = redFlagButtons.GetComponentsInChildren<Button>();
        redBut[ButtonNumber - 1].image.overrideSprite = sprite;
    }

    public void initializeButtonImage(List<GameObject> enemyFlags, List<GameObject> friendlyFlags)
    {
        Button[] blueBut = blueFlagButtons.GetComponentsInChildren<Button>();
        Button[] redBut = redFlagButtons.GetComponentsInChildren<Button>();

        foreach (GameObject flag in enemyFlags)
        {
            redBut[flag.GetComponent<FlagScript>().flagNumber - 1].image.overrideSprite = Resources.Load<Sprite>("Pics/redFlagSmall");
        }
        foreach (GameObject flag in friendlyFlags)
        {
            blueBut[flag.GetComponent<FlagScript>().flagNumber - 1].image.overrideSprite = Resources.Load<Sprite>("Pics/blueFlagSmall");
        }
    }

    public void SetTargetFlag(int newTargetFlagNumber)
    {
        if (GameAI.getGameAISingelton.GetTargetFlag() != null)
        {
            SetEnemyFlagImageOnButton(GameAI.getGameAISingelton.GetTargetFlag().GetComponent<FlagScript>().flagNumber, Resources.Load<Sprite>("flagPics/redFlagSmall"));
        }
        SetEnemyFlagImageOnButton(newTargetFlagNumber, Resources.Load<Sprite>("Pics/redFlagSmallGlow"));       
    }

    public void SetFlagInactive(GameObject flag)
    {
        if (flag.GetComponent<FlagScript>().flagName == "enemy")
        {
            Button[] redBut = redFlagButtons.GetComponentsInChildren<Button>();
            redBut[flag.GetComponent<FlagScript>().flagNumber - 1].enabled = false;
            redBut[flag.GetComponent<FlagScript>().flagNumber - 1].image.overrideSprite = Resources.Load<Sprite>("Pics/greyFlag");
        }

        if (flag.GetComponent<FlagScript>().flagName == "friendly")
        {
            Button[] blueBut = blueFlagButtons.GetComponentsInChildren<Button>();
            blueBut[flag.GetComponent<FlagScript>().flagNumber - 1].enabled = false;
            blueBut[flag.GetComponent<FlagScript>().flagNumber - 1].image.overrideSprite = Resources.Load<Sprite>("Pics/greyFlag");
        }
    }

    public void DisplayShopInGame()
    {
        inGameShop.SetActive(true);
    }

    public void HideShopInGame()
    {
        inGameShop.SetActive(false);
    }
}
