using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class MenuWarriorScript : MonoBehaviour {

    List<GameObject> weapons = new List<GameObject>();
    List<GameObject> armors = new List<GameObject>();
    List<GameObject> helmets = new List<GameObject>();
    public GameObject menuWarrior;
    int walkHash = Animator.StringToHash("Walk");
    Animator anim;
    int menuWarriorNr;
    GameObject lookAtCube;

    int currentWeapon;
    int currentArmor;
    int currentHelmet;

    public int getCurrentWeapon() { return currentWeapon; }
    public int getCurrentArmor() { return currentArmor; }
    public int getCurrentHelmet() { return currentHelmet; }

    public void setMenuWarriorNr(int nr)
    {
        this.menuWarriorNr = nr;
    }
    public int getMenuWarriorNr()
    {
        return this.menuWarriorNr;
    }

    void Start()
    {
        lookAtCube = GameObject.Find("LookAtCube");
    }
    void Update()
    {
        gameObject.transform.LookAt(lookAtCube.transform); // quick fix for at få menuWarrior til at kigge den rigtige retning
        gameObject.GetComponent<Animator>().SetTrigger(walkHash);
    }
    
    public void AddWeapon(GameObject wep)
    {
        this.weapons.Add(wep);
    }
    public void AddArmor(GameObject arm)
    {
        this.armors.Add(arm);
    }
    public void AddHelmet(GameObject hel)
    {
        this.helmets.Add(hel);
    }

    public List<GameObject> getWeaponsList()
    {
        return weapons;
    }
    public List<GameObject> getArmorList()
    {
        return armors;
    }
    public List<GameObject> getHelmetsList()
    {
        return helmets;
    }

    public void attatchAssest(string selected)
    {
        //HELMET EQUIP
        if (selected.StartsWith("helmet"))
        {
            foreach (GameObject helmet in menuWarrior.GetComponent<MenuWarriorScript>().getHelmetsList())
            {
                if (helmet.GetComponent<menuWarriorAssetScript>().getHelmetName() == selected)
                {
                    helmet.SetActive(true);
                    this.currentHelmet = int.Parse(selected.Substring(6));
                }   
                else
                    helmet.SetActive(false);
            }
        }//ARMOR EQUIP
        if (selected.StartsWith("armor"))
        {
            foreach (GameObject armor in menuWarrior.GetComponent<MenuWarriorScript>().getArmorList())
            {
                if (armor.GetComponent<menuWarriorAssetScript>().getArmorName() == selected)
                {
                    armor.SetActive(true);
                    this.currentArmor = int.Parse(selected.Substring(5)); 
                }
                else
                    armor.SetActive(false);
            }
        }//WEAPON EQUIP
        if (selected.StartsWith("weapon"))
        {
            foreach (GameObject weapon in menuWarrior.GetComponent<MenuWarriorScript>().getWeaponsList())
            {
                if (weapon.GetComponent<menuWarriorAssetScript>().getWeaponName() == selected)
                {
                    weapon.SetActive(true);
                    this.currentWeapon = int.Parse(selected.Substring(6));
                }
                else
                    weapon.SetActive(false);
            }
        }
    }

}
