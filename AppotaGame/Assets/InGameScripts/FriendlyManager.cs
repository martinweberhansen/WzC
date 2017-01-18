using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FriendlyManager : MonoBehaviour {

    public GameObject friendly; // dette er min character fra unity
    public GameObject spawnPoint;
    
    public void Spawn(int buttonNumber)
    {
        if (!GameAI.getGameAISingelton.HasAteamWon())
        {
            List<Unit> units = WorldObject.GetworldObject.units;
            Unit unit = units[buttonNumber - 1];

            int helmetPrice = WorldObject.GetworldObject.GetItem("helmet", unit.headID).price;
            int armorPrice = WorldObject.GetworldObject.GetItem("armor", unit.bodyID).price;
            int weaponPrice = WorldObject.GetworldObject.GetItem("weapon", unit.weaponID).price;


            if (GameAI.getGameAISingelton.GetCoins() >= (helmetPrice + armorPrice + weaponPrice + 10))
            {
                spawnPoint = GameObject.Find("FriendlyCastlePoint") as GameObject;
                GameObject friendlyGameObject = Instantiate(friendly, spawnPoint.gameObject.transform.position, spawnPoint.gameObject.transform.rotation) as GameObject;
                friendlyGameObject.AddComponent<NavMeshAgent>();
                friendlyGameObject.AddComponent<Rigidbody>();
                friendlyGameObject.AddComponent<UnitStatePattern>();
                friendlyGameObject.AddComponent<UnitStats>();
                friendlyGameObject.AddComponent<testshoot>();
                friendlyGameObject.AddComponent<SphereCollider>().isTrigger = true;
                friendlyGameObject.AddComponent<CapsuleCollider>().isTrigger = false;
                friendlyGameObject.AddComponent<UnitAI>();
                friendlyGameObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("FriendlyAnimatorController") as RuntimeAnimatorController;
                friendlyGameObject.layer = 9;
                friendlyGameObject.tag = "Friendly";
                GameAI.getGameAISingelton.AddFriendlyUnit(friendlyGameObject);


                createHelmet(friendlyGameObject, unit.headID);
                createWeapon(friendlyGameObject, unit.weaponID);
                createArmor(friendlyGameObject, unit.bodyID);
                SetUnitStats(friendlyGameObject, unit.headID, unit.bodyID, unit.weaponID);
                friendlyGameObject.GetComponent<UnitAI>().SetTeam("Friendly");
            }
            else
            {
                GameAI.getGameAISingelton.DisplayShopInGame();
            }
        }
    }
    
    public void SetUnitStats(GameObject unit,int helmetId,int armorId,int weaponId)
    {
        if (weaponId == 1)
        {
            unit.GetComponent<UnitAI>().SetAttackStyle("Ranged");
        }
        else
        {
            unit.GetComponent<UnitAI>().SetAttackStyle("Melee");
        }

        Item helmet = WorldObject.GetworldObject.GetItem("helmet", helmetId);
        Item armor = WorldObject.GetworldObject.GetItem("armor", armorId);
        Item weapon = WorldObject.GetworldObject.GetItem("weapon", weaponId);

        int attackRange;
        if (helmetId == 5 && weaponId == 1)//hvis robinhood hat og bue
        {
            attackRange = 60;
            unit.GetComponent<SphereCollider>().radius = attackRange;
        }
        else if (weaponId == 1)//hvis bue
        {
            attackRange = 40;
            unit.GetComponent<SphereCollider>().radius = attackRange;
        }
        else // ellers
        {
            attackRange = 2;
            unit.GetComponent<SphereCollider>().radius = attackRange + 2;
        }

        unit.GetComponent<UnitStats>().InitializeStats(
        /*  speed    */ (2 - helmet.movementSpeed - armor.movementSpeed - weapon.movementSpeed),
        /*  health   */ (20 + helmet.health + armor.health + weapon.health),
        /*  damage   */ (weapon.damage),
        /*attackRange*/ (attackRange),
        /* maxHealth */ (30 + helmet.health + armor.health + weapon.health)
        );


        GameAI.getGameAISingelton.SubtracCoins(helmet.price + weapon.price + armor.price + 10);

    }
    
    //creates and attaches items to unit
    public void createHelmet(GameObject g, int helmetId)
    {
        GameObject helmet = Instantiate(Resources.Load("CharacterAssets/helmets/helmet/helmet"+ helmetId), gameObject.transform.position, gameObject.transform.rotation) as GameObject;
        Transform placeHolder = g.transform.Find("Armature/root/spine/neck/head/head_end").transform;
        
        helmet.transform.parent = placeHolder; // sets the helmets parrent to head_end(placeHolder)
        helmet.transform.position = placeHolder.transform.position;
        
        helmet.transform.localEulerAngles = new Vector3(-90, 0, 0);
        helmet.transform.localPosition = new Vector3(0.0001f, -0.002f, -0.0001f);
        helmet.transform.localScale = new Vector3(0.19f, 0.19f, 0.19f);
    }
    
    private void createArmor(GameObject g,int armorId)
    {
        GameObject armor = Instantiate(Resources.Load("CharacterAssets/armors/armor/armor"+ armorId), g.transform.position, g.transform.rotation) as GameObject;
        Transform placeHolder = g.transform.Find("Armature/root/spine").transform;
        armor.transform.parent = placeHolder; 
        armor.transform.position = placeHolder.transform.position;

        armor.transform.localEulerAngles = new Vector3(-80, 0, 0);
        armor.transform.localPosition = new Vector3(-0.0001f, -0.001f, 0.0004f);
        armor.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }

    private void createWeapon(GameObject g, int weaponId)
    {
        Transform placeHolder;
        GameObject weapon = Instantiate(Resources.Load("CharacterAssets/weapons/weapon/weapon"+weaponId), g.transform.position, g.transform.rotation) as GameObject;
        if(weaponId == 1)
        {
             placeHolder = g.transform.Find("Armature/root/spine/armUp.R/armDown.R/armHand.R").transform;//if bow attach to armHand.R_end
            weapon.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("BowController") as RuntimeAnimatorController;
        }
        else
        {
            placeHolder = g.transform.Find("Armature/root/spine/armUp.L/armDown.L/armHand.L").transform;// else attack to earmHand.L_end
        }
        weapon.transform.parent = placeHolder; // sets the weapon parrent to the hand
        weapon.transform.position = placeHolder.transform.position;
        
        weapon = setWeaponRotation(weapon);
    }
    
    public GameObject setWeaponRotation(GameObject weapon)
    {
        if (weapon.transform.name == "weapon3(Clone)")//hammer
        {
            weapon.transform.localEulerAngles = new Vector3(0.000f, 0.000f, 90.000f);
            weapon.transform.localPosition = new Vector3(0.0004f, 0.0008f, -0.0006f);
            weapon.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
            return weapon;
        }
        else if (weapon.transform.name == "weapon4(Clone)")//sabre
        {
            weapon.transform.localEulerAngles = new Vector3(0.000f, 0.000f, 180.000f);
            weapon.transform.localPosition = new Vector3(-0.0004f, 0.001f, -0.0004f);
            weapon.transform.localScale = new Vector3(0.018f, 0.018f, 0.018f);
            return weapon;
        }
        else if (weapon.transform.name == "weapon2(Clone)")//small sort
        {
            weapon.transform.localEulerAngles = new Vector3(0.000f, 0.000f, 0.000f);
            weapon.transform.localPosition = new Vector3(-0.0073f, 0.001f, -0.0004f);
            weapon.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
            return weapon;
        }
        else if (weapon.transform.name == "weapon5(Clone)")//long swoard
        {
            weapon.transform.localEulerAngles = new Vector3(0.000f, 0.000f, 0.000f);
            weapon.transform.localPosition = new Vector3(-0.0085f, 0.0011f, -0.0005f);
            weapon.transform.localScale = new Vector3(0.18f, 0.18f, 0.18f);
            return weapon;
        }
        else if (weapon.transform.name == "weapon1(Clone)")//bow
        {
            weapon.transform.localEulerAngles = new Vector3(78.0f, 60.0f, 56.0f);
            weapon.transform.localPosition = new Vector3(0.0002f, 0.0f, -0.0008f);
            weapon.transform.localScale = new Vector3(0.002f, 0.002f, 0.002f);
            return weapon;
        }else
        return null;
    }
}

