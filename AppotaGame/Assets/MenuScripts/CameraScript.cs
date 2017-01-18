using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    bool            testing;
    [SerializeField]
    float           difficultymultiplier;
    [SerializeField]
    GameObject      menuWarrior;
    [SerializeField]
    GameObject      shopPanel;
    [SerializeField]
    GameObject      partyPanel;
    [SerializeField]
    GameObject      spellsPanel;
    [SerializeField]
    Text            totalGold;
    [SerializeField]
    Text            totalCrystal;
    [SerializeField]
    GameObject      fader;
    
    [Header("Customize")]
    [SerializeField]
    GameObject      buttonToCopyCustomize;
    [SerializeField]
    List<GameObject> characterRowsCustomize;
    [SerializeField]
    float           characterRowMinimumSizeCustomize;
    [SerializeField]
    float           characterRowWidthOfButtonCostumize;
    [SerializeField]
    float           characterSpacingOfButtonCustomize;
    
    [Header("Selector")]
    [SerializeField]
    GameObject      CharacterRowsSelector;
    [SerializeField]
    float           characterRowMinimumSizeSelector;
    [SerializeField]
    float           characterRowWidthOfButtonSelector;
    [SerializeField]
    float           characterSpacingOfButtonSelector;
    
    [Header("warrior info text")]
    [SerializeField]
    Text            priceText;
    [SerializeField]
    Text            speedText;
    [SerializeField]
    Text            healtText;
    [SerializeField]
    Text            damageText;
    [SerializeField]
    Text            attackRangeText;
    [SerializeField]
    List<GameObject> levelButtons;


    Level levelselection;
    MapSelection mapToLoad;
    bool panelOpen;
    TouchScreenKeyboard keyboard;

    //[SerializeField]
    //bool            touchStartedOutsidePanel;
    //[SerializeField]
    //float           cameraMoveSpeed;
    //[SerializeField]
    //GameObject      WorldPivot;
    //[SerializeField]
    //float           mapLength;
    //[SerializeField]
    //Sprite          newUnitTexture;
    //[SerializeField]
    //int             maxPartySize;
    //[SerializeField]
    //GameObject      addToPartyButton;
    //[SerializeField]
    //GameObject      removeFromPartyButton;
    //[SerializeField]
    //Text            partySizeText;
    //[SerializeField]
    //GameObject      baseSelector;
    //[SerializeField]
    //GameObject      buttonToCopySelector;
    //[SerializeField]
    //GameObject      nameText;
    //[SerializeField]
    //GameObject      classNameText;
    //[SerializeField]
    //GameObject      classDescriptionText;
    //[SerializeField]
    //Unit            unitToCustomize;
    //[SerializeField]
    //GameObject      customizeBase;
    //[Header("Party panel")]
    //[SerializeField]
    //GameObject      CameraPanel;
    //[SerializeField]
    //GameObject      outfitPanel;
    //[SerializeField]
    //GameObject      displayCharacter;
    //[SerializeField]
    //Unit            selectedUnit;




    void Start()
    {
        //ingen crash ved loading
        if (testing)
        {
            bool doneloadingXML = false;
            StartCoroutine(WorldObject.GetworldObject.LoadFromXML((callback) =>
            {
                if (callback)
                {
                    doneloadingXML = true;
                }
            }));
            while (doneloadingXML)
            {

            }
        }
        
        LoadItemsIntoCustomizeSlots();
        LoadPartyUnitsFromXML();
        InformationInLevelButtons();
    }
    
    void Update()
    {
        UpdateMenuWarriorStats();
        
        UpdateScreen();

        if (testing)
        {
            Cheats();
        }
    }
    //update coins / crystals
    void UpdateScreen()
    {
        totalGold.text = WorldObject.GetworldObject.worldInfo.gold.ToString();
        totalCrystal.text = WorldObject.GetworldObject.worldInfo.crystal.ToString();
    } 
    // yes.. cheats.. FOR DEVELOPMENT ONLY! NEEDS TO BE DISABLED IN FINAL BUILD
    void Cheats()
    {
        if (Input.touchCount > 3)
        {
            WorldObject.GetworldObject.worldInfo.gold += 100;
            WorldObject.GetworldObject.worldInfo.crystal += 10;
        }
    } 
    
    void InformationInLevelButtons()
    {
        foreach (GameObject button in levelButtons)
        {
            MapSelection mapselect = button.GetComponent<MapSelection>();
            Level showlevel = WorldObject.GetworldObject.levels.Find(level => level.id == mapselect.id);
            mapselect.UpdateButton(showlevel);
        }
    }

    public void setTEXT(string input)
    {
        Text text = GameObject.Find("TEXT").GetComponent<Text>();
        text.text = text.text + input;
    }
    
    void LoadItemsIntoCustomizeSlots()
    {
        //load alle items
        List<Item> headItems = WorldObject.GetworldObject.items.FindAll(item => item.location == Location.Head);
        List<Item> bodyItems = WorldObject.GetworldObject.items.FindAll(item => item.location == Location.Body);
        List<Item> WeaponItems = WorldObject.GetworldObject.items.FindAll(item => item.location == Location.Weapon);
        //scale menu så den passer med antal knapper;
        characterRowsCustomize[0].GetComponent<RectTransform>().sizeDelta = new Vector2((headItems.Count * characterRowWidthOfButtonCostumize) + ((headItems.Count - 1) * characterSpacingOfButtonCustomize), characterRowWidthOfButtonCostumize);
        characterRowsCustomize[1].GetComponent<RectTransform>().sizeDelta = new Vector2((bodyItems.Count * characterRowWidthOfButtonCostumize) + ((bodyItems.Count - 1) * characterSpacingOfButtonCustomize), characterRowWidthOfButtonCostumize);
        characterRowsCustomize[2].GetComponent<RectTransform>().sizeDelta = new Vector2((WeaponItems.Count * characterRowWidthOfButtonCostumize) + ((WeaponItems.Count - 1) * characterSpacingOfButtonCustomize), characterRowWidthOfButtonCostumize);

        
        //lav nye knapper med item info
        int ticker = 0;
        foreach (Item item in headItems)
        {
            createButtonAndAttachHeadItems(item,ticker);
            ticker++;
        }
        ticker = 0;
        foreach (Item item in bodyItems)
        {
            createButtonAndAttachArmorItems(item, ticker);
            ticker++;
        }
        ticker = 0;
        foreach (Item item in WeaponItems)
        {
            CreateButtonAndAttachWeapons(item, ticker);
            ticker++;
        }
    }
    
    void LoadPartyUnitsFromXML()
    {
        foreach (Unit unit in WorldObject.GetworldObject.units)
        {
            Button[] but = CharacterRowsSelector.GetComponentsInChildren<Button>();
            but[unit.id-1].image.overrideSprite = Resources.Load<Sprite>("CharacterAssets/helmets/pictures/helmetPicture" + unit.headID);
        }
    }
    
    public void SaveCurrentParty()
    {
        WorldObject.GetworldObject.SaveUnits();
    }

    public void SaveCostumizedMenuWarrior()
    {
        int selectedWarriorToCostumizeNr = menuWarrior.GetComponent<MenuWarriorScript>().getMenuWarriorNr();

        WorldObject.GetworldObject.units[selectedWarriorToCostumizeNr - 1]
            .setAssetIDs(
                  menuWarrior.GetComponent<MenuWarriorScript>().getCurrentHelmet()
                , menuWarrior.GetComponent<MenuWarriorScript>().getCurrentArmor()
                , menuWarrior.GetComponent<MenuWarriorScript>().getCurrentWeapon()
            );
        SaveCurrentParty();
        LoadPartyUnitsFromXML();

    }

     void UpdateMenuWarriorStats()
    {
        int helmetNumber  = menuWarrior.GetComponent<MenuWarriorScript>().getCurrentHelmet();
        int armorNumber = menuWarrior.GetComponent<MenuWarriorScript>().getCurrentArmor();
        int weaponNumber = menuWarrior.GetComponent<MenuWarriorScript>().getCurrentWeapon();

        Item h = WorldObject.GetworldObject.GetItem("helmet", helmetNumber);
        Item a = WorldObject.GetworldObject.GetItem("armor", armorNumber);
        Item w = WorldObject.GetworldObject.GetItem("weapon", weaponNumber);
        
        priceText.text = (10+h.price + a.price + w.price) +"";
        speedText.text = (2 + (h.movementSpeed + a.movementSpeed + w.movementSpeed)*-1 + "");
        healtText.text = (20 + h.health + a.health + w.health ) + "";
        damageText.text = (h.damage + a.damage + w.damage ) + "";
        if(w.assetName == "weapon1")
        {
            if(h.assetName == "helmet5")
            {
                attackRangeText.text = "60";
            }
            else
            {
                attackRangeText.text = "40";
            }
        }
        else
        {
            attackRangeText.text = "2";
        }
    }
    
    public void ChangeSelectedMenuWarrior(int warriorButtonNr)
    {
        Unit warrior = WorldObject.GetworldObject.units [warriorButtonNr-1];
        
        menuWarrior.GetComponent<MenuWarriorScript>().attatchAssest("helmet" + warrior.headID);
        menuWarrior.GetComponent<MenuWarriorScript>().attatchAssest("armor" + warrior.bodyID);
        menuWarrior.GetComponent<MenuWarriorScript>().attatchAssest("weapon" + warrior.weaponID);
        menuWarrior.GetComponent<MenuWarriorScript>().setMenuWarriorNr(warriorButtonNr);
    }
    
    public void ChangeFaceClick()
    {

    }
    
    public void SelectLevelButtonClick(Button button)
    {
        MapSelection mapselect = button.GetComponent<MapSelection>();
        //Level showlevel = WorldObject.GetworldObject.levels.Find(level => level.id == mapselect.id);
        SceneManager.LoadScene(mapselect.getSceneName());
    }
  
    private void createButtonAndAttachHeadItems(Item item,int ticker)
    {
        GameObject obj = Instantiate(buttonToCopyCustomize, characterRowsCustomize[0].transform.position, Quaternion.identity) as GameObject;
        obj.GetComponent<ScrollButton>().item = item;
        obj.transform.SetParent(characterRowsCustomize[0].transform);
        obj.transform.localScale = new Vector3(1, 1, 1);

        obj.GetComponent<Image>().sprite = Resources.Load<Sprite>("CharacterAssets/helmets/pictures/helmetPicture" + ticker);
        obj.AddComponent<WarriorMenuButtonScript>();
        obj.GetComponent<WarriorMenuButtonScript>().SetButtonActionName("helmet" + ticker, "helmet");
        
        GameObject helmet = Instantiate(Resources.Load("CharacterAssets/helmets/helmet/helmet" + ticker), menuWarrior.transform.position, menuWarrior.transform.rotation) as GameObject;
        helmet.AddComponent<menuWarriorAssetScript>();
        Transform placeHolder = menuWarrior.transform.Find("Armature/root/spine/neck/head/head_end").transform;
        helmet.transform.parent = placeHolder;
        helmet.transform.position = placeHolder.transform.position;
        helmet.GetComponent<menuWarriorAssetScript>().setHelmetName("helmet" + ticker);
        menuWarrior.GetComponent<MenuWarriorScript>().AddHelmet(helmet);

        helmet.transform.localEulerAngles = new Vector3(-90, 0, 0);
        helmet.transform.localPosition = new Vector3(0.0001f, -0.002f, -0.0001f);
    }
    private void createButtonAndAttachArmorItems(Item item, int ticker)
    {
        GameObject obj = Instantiate(buttonToCopyCustomize, characterRowsCustomize[1].transform.position, Quaternion.identity) as GameObject;
        obj.GetComponent<ScrollButton>().item = item;
        obj.transform.SetParent(characterRowsCustomize[1].transform);
        obj.transform.localScale = new Vector3(1, 1, 1);

        obj.GetComponent<Image>().sprite = Resources.Load<Sprite>("CharacterAssets/armors/pictures/pictureArmor" + ticker);
        obj.AddComponent<WarriorMenuButtonScript>();
        obj.GetComponent<WarriorMenuButtonScript>().SetButtonActionName("armor" + ticker, "armor");

        
        //GameObject g = GameObject.Find("Plane");   //"
        GameObject armor = Instantiate(Resources.Load("CharacterAssets/armors/armor/armor" + ticker), menuWarrior.transform.position, menuWarrior.transform.rotation) as GameObject;
        armor.AddComponent<menuWarriorAssetScript>();
        Transform placeHolder = menuWarrior.transform.Find("Armature/root/spine").transform;
        armor.transform.parent = placeHolder;
        armor.transform.position = placeHolder.transform.position;
        armor.GetComponent<menuWarriorAssetScript>().setArmorName("armor" + ticker);
        menuWarrior.GetComponent<MenuWarriorScript>().AddArmor(armor);

        armor.transform.localEulerAngles = new Vector3(-80, 0, 0);
        armor.transform.localPosition = new Vector3(-0.0001f, -0.001f, 0.0004f);
    }
    private void CreateButtonAndAttachWeapons(Item item,int ticker)
    { 
            GameObject obj = Instantiate(buttonToCopyCustomize, characterRowsCustomize[2].transform.position, Quaternion.identity) as GameObject;
            obj.GetComponent<ScrollButton>().item = item;
            obj.transform.SetParent(characterRowsCustomize[2].transform);
            obj.transform.localScale = new Vector3(1, 1, 1);
            obj.GetComponent<Image>().sprite = Resources.Load<Sprite>("CharacterAssets/weapons/pictures/weapon" + ticker);
            obj.AddComponent<WarriorMenuButtonScript>();
            obj.GetComponent<WarriorMenuButtonScript>().SetButtonActionName("weapon" + ticker, "weapon");

            
            if (ticker == 1) // hvis det er bue, parrent til right arm
            {
                //GameObject g = GameObject.Find("Plane");
                GameObject weapon = Instantiate(Resources.Load("CharacterAssets/weapons/weapon/weapon" + ticker), menuWarrior.transform.position, menuWarrior.transform.rotation) as GameObject;
                weapon.AddComponent<menuWarriorAssetScript>();
                Transform placeHolder = menuWarrior.transform.Find("Armature/root/spine/armUp.R/armDown.R/armHand.R").transform;
                weapon.transform.parent = placeHolder;
                weapon.transform.position = placeHolder.transform.position;
                weapon.GetComponent<menuWarriorAssetScript>().setWeaponName("weapon" + ticker);
                menuWarrior.GetComponent<MenuWarriorScript>().AddWeapon(weapon);
                menuWarrior.GetComponent<FriendlyManager>().setWeaponRotation(weapon);
            }
            else
            {
                //GameObject g = GameObject.Find("Plane");
                GameObject weapon = Instantiate(Resources.Load("CharacterAssets/weapons/weapon/weapon" + ticker), menuWarrior.transform.position, menuWarrior.transform.rotation) as GameObject;
                weapon.AddComponent<menuWarriorAssetScript>();
                Transform placeHolder = menuWarrior.transform.Find("Armature/root/spine/armUp.L/armDown.L/armHand.L").transform;
                weapon.transform.parent = placeHolder; 
                weapon.transform.position = placeHolder.transform.position;
                weapon.GetComponent<menuWarriorAssetScript>().setWeaponName("weapon" + ticker);
                menuWarrior.GetComponent<MenuWarriorScript>().AddWeapon(weapon);
                menuWarrior.GetComponent<FriendlyManager>().setWeaponRotation(weapon);
            }
        }
}
