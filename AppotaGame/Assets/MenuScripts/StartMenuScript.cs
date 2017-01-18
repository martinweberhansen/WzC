using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartMenuScript : MonoBehaviour {
    /*
     *HUSK ALTID AT SÆTTE ALLE BUTTONS I UNITY INSPECTOREN TIL VISIBLE, ELLERS KOMMER DE IKKE FREM VED START
     *HUSK ALTID AT SÆTTE ALLE BUTTONS I UNITY INSPECTOREN TIL VISIBLE, ELLERS KOMMER DE IKKE FREM VED START
     */

    GameObject chooseMapMenu;
    GameObject magicMenu;
    GameObject warriorMenu;
    GameObject startMenu;
    GameObject menuWarrior;

    [SerializeField]
    GameObject unitSelector;

    [SerializeField]
    GameObject unitCustomize;

    void Start()
    {
        chooseMapMenu   = GameObject.Find("ChooseMapMenu").gameObject;
        magicMenu       = GameObject.Find("MagicMenu").gameObject;
        warriorMenu     = GameObject.Find("WarriorMenu").gameObject;
        startMenu       = GameObject.Find("StartMenu").gameObject;
        menuWarrior     = GameObject.Find("MenuWarrior");

        chooseMapMenu.SetActive(false);
        magicMenu.SetActive(false);
        startMenu.SetActive(true);
        warriorMenu.SetActive(false);
        unitSelector.SetActive(false);
        menuWarrior.SetActive(false);
    }

    public void ToMapSelection()
    {
        chooseMapMenu.SetActive(true);
        startMenu.SetActive(false);
        magicMenu.SetActive(false);
        warriorMenu.SetActive(false);
        unitSelector.SetActive(false);
        unitCustomize.SetActive(false);
        menuWarrior.SetActive(false);
    }
    public void ToStartMenu()
    {
        startMenu.SetActive(true);
        chooseMapMenu.SetActive(false);
        magicMenu.SetActive(false);
        warriorMenu.SetActive(false);
        unitSelector.SetActive(false);
        unitCustomize.SetActive(false);
        menuWarrior.SetActive(false);
    }
    public void ToMagicMenu()
    {
        magicMenu.SetActive(true);
        startMenu.SetActive(false);
        chooseMapMenu.SetActive(false);
        warriorMenu.SetActive(false);
        unitSelector.SetActive(false);
        unitCustomize.SetActive(false);
        menuWarrior.SetActive(false);
    }
    public void ToWarriorMenu()
    {
        warriorMenu.SetActive(true);
        magicMenu.SetActive(false);
        startMenu.SetActive(false);
        chooseMapMenu.SetActive(false);
        unitSelector.SetActive(true);
        unitCustomize.SetActive(false);
        menuWarrior.SetActive(true);
    }
    public void ToUnitCustomize()
    {
        unitSelector.SetActive(false);
        unitCustomize.SetActive(true);
    }

}
