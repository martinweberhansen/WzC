using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic; //need this for List

public class GameAI {
    
    public static GameAI singeltonInstance = null;
    private List<GameObject> FriendlyUnits;
    private List<GameObject> EnemyUnits;
    private List<GameObject> redFlagList;
    private List<GameObject> blueFlagList;

    System.Random rand;
    public GameObject targetFlag; // the flag the friendly warriors should attempt to capture
    public GameObject HUDCtrl;
    private GameObject whenGameIsOver;
    

    private int Coins;
    private int Crystals;
    private bool hasATeamWon;
    private bool arePlayerVictories;

    public bool ArePlayerVictories()
    {
        return arePlayerVictories;
    }
    private GameAI() { }
    
    public static GameAI    getGameAISingelton
    {
        get
        {
            if (singeltonInstance == null)
            {
                Debug.Log("creating new singeltonInstance of game AI");
                singeltonInstance = new GameAI();
                singeltonInstance.Initialize();
            }
            return singeltonInstance;
        }
    }
    void                    Initialize()
    {
        FriendlyUnits = new List<GameObject>();
        EnemyUnits = new List<GameObject>();
        blueFlagList = new List<GameObject>();
        redFlagList = new List<GameObject>();
        rand = new System.Random();
        HUDCtrl = GameObject.Find("HUDCanvas");
        whenGameIsOver = GameObject.Find("WhenGameIsOver");
        
    }

    public void DestroySingeltonInstance()
    {
        singeltonInstance = null;
    }

    public void ShowStats()
    {
        Debug.Log("UNITS count in AI: " + FriendlyUnits.Count);
    }

    //public void CheckInitState()//need this because the singeltons doesnt get deleted when changing scene. But the list doesnt get reset.
    //{
        
    //    if (HUDCtrl)
    //    {
    //        Debug.Log("init state in AI");
    //        Debug.Log("IT EXISTS");
    //    }
    //    else
    //    {
    //        Initialize();
    //    }
    //}

    public void             SetCoins(int coins)
    {
        this.Coins = coins;
    }
    public void             SetCrystals(int crystals)
    {
        this.Crystals = crystals;
    }

    public void             AddCoins(int coins)
    {
        this.Coins += coins;
    }
    public void             AddCrystals(int crystals)
    {
        this.Crystals += crystals;
    }

    public void             SubtracCoins(int coins)
    {
        this.Coins -= coins;
    }
    public void             SubtractCrystals(int crystals)
    {
        this.Crystals -= crystals;
    }

    public int              GetCoins()
    {
        return Coins;
    }
    public int              GetCrystals()
    {
        return Crystals;
    }
    
	
    public void             AddFriendlyUnit(GameObject unit)
    {
        FriendlyUnits.Add(unit);

    }

    public void             AddEnemyUnit(GameObject unit)
    {
        EnemyUnits.Add(unit);
    }

    public void             RemoveFriendlyUnit(GameObject unit)
    {
        FriendlyUnits.Remove(unit);
    }

    public void             RemoveEnemyUnit(GameObject unit)
    {
        EnemyUnits.Remove(unit);
    }
    
    public void             SetEnemyFlagList(List<GameObject> list)
    {
        redFlagList = list;
    }

    public void             SetFriendlyFlagList(List<GameObject> list)
    {
        blueFlagList = list;
    }
    
    public List<GameObject> GetRedFlagList()
    {
        return redFlagList;
    }

    public List<GameObject> GetBlueFlagList()
    {
        return blueFlagList;
    }

    public GameObject       GetRandomEnemyFlag()
    {
        try
        {
            int randInt = rand.Next(0, redFlagList.Count);
            return redFlagList[randInt];
        }
        catch
        {
            
        }
        return null;
        
        
    }

    public GameObject       GetRandomFriendlyFlag()
    {
        int randInt = rand.Next(0, 0 + blueFlagList.Count);
        return blueFlagList[randInt];
    }

    public GameObject       GetEnemyFlagByNumber(int num)
    {
        foreach(GameObject flag in redFlagList)
        {
            if (flag.name == "flagEnemy" + num)
            {
                return flag;
            }
        }
        return null;
    }

    public GameObject       GetFriendlyFlagByNumber(int num)
    {
        foreach (GameObject flag in blueFlagList)
        {
            if (flag.name == "flagfriendly" + num)
            {
                return flag;
            }
        }
        return null;
    }

    public void             RemoveFriendlyFlag(GameObject FlagToRemove)
    {
        GameObject toBeDestroyed = null;
        foreach (GameObject flag in blueFlagList)
        {
            if (flag.name == FlagToRemove.name)
            {
                Debug.Log("first stage of inactive friendly flag");
                toBeDestroyed = flag;
            }
        }
        HUDCtrl.GetComponent<HUDController>().SetFlagInactive(toBeDestroyed);
        blueFlagList.Remove(toBeDestroyed);
        CheckIfATeamHasWon();
    }

    public void             RemoveEnemyFlag(GameObject FlagToRemove)
    {
        Debug.Log("count on redflag: " + redFlagList.Count);
        GameObject toBeDestroyed = null;
        foreach (GameObject flag in redFlagList)
        {
            if (flag.name == FlagToRemove.name)
            {
                Debug.Log("first stage of inactive enemy flag");
                toBeDestroyed = flag;
            }
        }
        HUDCtrl.GetComponent<HUDController>().SetFlagInactive(toBeDestroyed);
        redFlagList.Remove(toBeDestroyed);
        CheckIfATeamHasWon();
    }
    
    private void            CheckIfATeamHasWon()
    {
        if ((blueFlagList.Count <= 0))
        {
            Debug.Log("RED TEAM HAS WON");
            StopAllWarriorMovement();
            whenGameIsOver.GetComponent<WhenGameIsOverScript>().InitializeGameOver(true);
            whenGameIsOver.GetComponent<WhenGameIsOverScript>().ShowGameOverMenu("YOU ARE DEFEATED");
            arePlayerVictories = false;
            SetHasATeamWon(true);
        }
        else if ((redFlagList.Count <= 0))
        {
            Debug.Log("BLUE TEAM HAS WON!");
            StopAllWarriorMovement();
            whenGameIsOver.GetComponent<WhenGameIsOverScript>().InitializeGameOver(false);
            whenGameIsOver.GetComponent<WhenGameIsOverScript>().ShowGameOverMenu("YOU ARE VICTORIOUS");
            arePlayerVictories = true;
            SetHasATeamWon(true);
        }
    }
    
    public GameObject       GetTargetFlag()
    {
        return targetFlag;
    }

    public void             SetTargetFlag(int targetNumber)
    {
        Debug.Log("target flag Number is: " + targetNumber);
        HUDCtrl.GetComponent<HUDController>().SetTargetFlag(targetNumber);
        targetFlag = GetEnemyFlagByNumber(targetNumber);
    }

    private void            StopAllWarriorMovement()
    {
        foreach(GameObject warrior in FriendlyUnits)
        {
            warrior.GetComponent<UnitStatePattern>().SetToIdle();
        }
        foreach(GameObject warrior in EnemyUnits)
        {
            warrior.GetComponent<UnitStatePattern>().SetToIdle();
        }
    }
    
    public bool             HasAteamWon()
    {
        return hasATeamWon;
    }

    public void             SetHasATeamWon(bool ans)
    {
        hasATeamWon = ans;
    }

    public void             DisplayShopInGame()
    {
        HUDCtrl.GetComponent<HUDController>().DisplayShopInGame();
    }

    public void             HideShopInGame()
    {
        HUDCtrl.GetComponent<HUDController>().HideShopInGame();
    }

}
