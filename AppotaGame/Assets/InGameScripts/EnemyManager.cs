using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {

    public GameObject enemy;
    public GameObject spawnPoint;
    
    public void Spawn()
    {
        if (!GameAI.getGameAISingelton.HasAteamWon())
        {
            System.Random ran = new System.Random(); int num = ran.Next(0, 3);
            GameObject enemyGameObject = Instantiate(enemy, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
            enemyGameObject.AddComponent<NavMeshAgent>();
            enemyGameObject.AddComponent<Rigidbody>();
            enemyGameObject.AddComponent<UnitStatePattern>();
            enemyGameObject.AddComponent<UnitStats>();

            enemyGameObject.AddComponent<testshoot>();
            enemyGameObject.AddComponent<SphereCollider>().isTrigger = true;
            enemyGameObject.AddComponent<CapsuleCollider>().isTrigger = false;
            enemyGameObject.AddComponent<UnitAI>();
            enemyGameObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("FriendlyAnimatorController") as RuntimeAnimatorController;
            enemyGameObject.layer = 10;
            enemyGameObject.tag = "Enemy";//tror ikke jeg bruger
            GameAI.getGameAISingelton.AddEnemyUnit(enemyGameObject);
            
            List<Unit> units = WorldObject.GetworldObject.units;
            Unit unit = units[num];
            createHelmet(enemyGameObject, unit.headID);
            createWeapon(enemyGameObject, unit.weaponID);
            createArmor(enemyGameObject, unit.bodyID);

            SetUnitStats(enemyGameObject, unit.headID, unit.bodyID, unit.weaponID);

            enemyGameObject.GetComponent<UnitAI>().SetTeam("Enemy");

            if (unit.weaponID == 1)
            {
                enemyGameObject.GetComponent<UnitAI>().SetAttackStyle("Ranged");
            }
            else
            {
                enemyGameObject.GetComponent<UnitAI>().SetAttackStyle("Melee");
            }
        }

        
    }

    public void SetUnitStats(GameObject unit, int helmetId, int armorId, int weaponId)
    {    
        Item helmet = WorldObject.GetworldObject.GetItem("helmet", helmetId);
        Item armor = WorldObject.GetworldObject.GetItem("armor", armorId);
        Item weapon = WorldObject.GetworldObject.GetItem("weapon", weaponId);

        int attackRange;
        if (helmetId == 5 && weaponId == 1)//hvis robinhood hat og bue
        {
            attackRange = 60;
            unit.GetComponent<SphereCollider>().radius = attackRange;
        }
        else if (weaponId == 1)//hvis kun bue
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
    }

    //creates and attaches item to unit 
    public void createHelmet(GameObject g, int helmetId)
    {
        GameObject helmet = Instantiate(Resources.Load("CharacterAssets/helmets/helmet/helmet" + helmetId)) as GameObject; //, gameObject.transform.position, gameObject.transform.rotation
        Transform placeHolder = g.transform.Find("Armature/root/spine/neck/head/head_end").transform;
        
        helmet.transform.parent = placeHolder; // sets the helmets parrent to head_end(placeHolder)
        helmet.transform.position = placeHolder.transform.position;

        helmet.transform.localEulerAngles = new Vector3(-90, 0, 0);
        helmet.transform.localPosition = new Vector3(0.0001f, -0.002f, -0.0001f);
        helmet.transform.localScale = new Vector3(0.19f, 0.19f, 0.19f);
    }
    //creates and attaches item to unit 
    public void createArmor(GameObject g, int armorId)
    { 
        GameObject armor = Instantiate(Resources.Load("CharacterAssets/armors/armor/armor" + armorId), g.transform.position, g.transform.rotation) as GameObject;
        Transform placeHolder = g.transform.Find("Armature/root/spine").transform;
        armor.transform.parent = placeHolder;
        armor.transform.position = placeHolder.transform.position;

        armor.transform.localEulerAngles = new Vector3(-80, 0, 0);
        armor.transform.localPosition = new Vector3(-0.0001f, -0.001f, 0.0004f);
        armor.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }
    //creates and attaches item to unit 
    public void createWeapon(GameObject g, int weaponId)
    {
        Transform placeHolder;
        GameObject weapon = Instantiate(Resources.Load("CharacterAssets/weapons/weapon/weapon" + weaponId), g.transform.position, g.transform.rotation) as GameObject;
        if (weaponId == 1)
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
    //sets the weapons rotation, so it fits the warrior
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
        else if (weapon.transform.name == "weapon2(Clone)")//small sword
        {
            weapon.transform.localEulerAngles = new Vector3(0.000f, 0.000f, 0.000f);
            weapon.transform.localPosition = new Vector3(-0.0073f, 0.001f, -0.0004f);
            weapon.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
            return weapon;
        }
        else if (weapon.transform.name == "weapon5(Clone)")//long sword
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
        }
        return weapon;
    }

}
