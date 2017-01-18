using UnityEngine;
using System.Collections;
using System;

public class UnitAI : MonoBehaviour {

    public enum Team { Friendly, Enemy };
    public enum AttackStyle { Melee, Ranged };

    public Team unitTeam;
    public AttackStyle unitAttackStyle;


    private GameObject target = null;
    private NavMeshAgent agent;
    private UnitStatePattern sp;
    private UnitStats unitStats;
    private GameObject selectedFlag;
    private GameObject mainCam;
    private GameObject TempDamageShower;


    void            Start()
    {
        unitStats = gameObject.GetComponent<UnitStats>();
        sp = gameObject.GetComponent<UnitStatePattern>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        mainCam = GameObject.Find("Main Camera");
      
        
        switch (unitTeam)
        {
            case Team.Enemy :
                Physics.IgnoreLayerCollision(10, 8);  //ignore map
                Physics.IgnoreLayerCollision(10, 10); // ignore same team
                if (GameAI.getGameAISingelton.GetRandomFriendlyFlag())
                {
                    selectedFlag = GameAI.getGameAISingelton.GetRandomFriendlyFlag();
                }
                break;

            case Team.Friendly :
                Physics.IgnoreLayerCollision(9, 8);  //ignore map
                Physics.IgnoreLayerCollision(9, 9);  // ignore same team
                if (selectedFlag == null)
                {
                    if (GameAI.getGameAISingelton.GetTargetFlag() != null)
                    {
                        selectedFlag = GameAI.getGameAISingelton.GetTargetFlag();
                    }
                    else
                    {
                        if(GameAI.getGameAISingelton.GetRandomEnemyFlag() != null)
                        {
                            selectedFlag = GameAI.getGameAISingelton.GetRandomEnemyFlag();
                        }
                    }
                }
                break;
        }
        if (selectedFlag)
        {
            target = selectedFlag;
            agent.SetDestination(target.transform.position);
        }
    }

    void            Update()
    {
        try
        {
            if (!sp.IsSetToIdle()) // bliver kun permanent sat til idle når et team har vundet.
            {
                sp.setInRangeToAttack(false);
                if (target.gameObject == null || !target.gameObject) //hvis der ikke er noget target.
                {
                    if (selectedFlag.gameObject && selectedFlag != null) //hvis flag ikke er destroyed.
                    {
                        target = selectedFlag;
                        agent.SetDestination(target.gameObject.transform.position);
                    }
                    else //ellers vælg nyt flag.
                    {
                        switch (unitTeam)
                        {
                            case Team.Enemy:
                                selectedFlag = GameAI.getGameAISingelton.GetRandomFriendlyFlag(); // hvis der ikke er flere flag, gå til idle, eller fejl.
                                break;

                            case Team.Friendly:
                                selectedFlag = GameAI.getGameAISingelton.GetRandomEnemyFlag();
                                break;
                        }
                        target = selectedFlag;
                    }
                    if (target.gameObject != null || target.gameObject) // hvis der er et target, set destination
                    {
                        agent.SetDestination(target.gameObject.transform.position);
                        agent.Resume();
                    }
                }

                if (unitStats.health < 0)//hvis liv under 0, kill.
                {
                    sp.Kill();  //drop flag hvis carrying er flag true
                }
                else if (sp.CarryingFlag())//hvis warrrior har flag, go to castle.
                {
                    switch (unitTeam)
                    {
                        case Team.Enemy:
                            target = GameObject.Find("EnemyCastlePoint");
                            break;

                        case Team.Friendly:
                            target = GameObject.Find("FriendlyCastlePoint");
                            break;
                    }
                    agent.SetDestination(target.gameObject.transform.position);
                    agent.Resume();
                }
                else if (target != null && gameObject != null) //ellers attack. 
                {
                    float distance = Vector3.Distance(gameObject.transform.position, target.transform.position);

                    if (target == selectedFlag && distance < 2)//hvis target er flag, og unit er under 2f fra flag
                    {
                        sp.setInRangeToAttack(true);
                        gameObject.transform.LookAt(target.transform);
                        agent.Stop();
                    }
                    else if (distance < unitStats.attackRange && target != selectedFlag)//hvis target er indenfor attackRange, og ikke er et flag.
                    {
                        sp.setInRangeToAttack(true);
                        gameObject.transform.LookAt(target.transform);
                        agent.Stop();
                    }
                    else
                    {
                        sp.setInRangeToAttack(false);
                        agent.SetDestination(target.gameObject.transform.position);
                        agent.Resume();
                    }
                }
            }
        }
        catch(Exception e)
        {
            Debug.Log("Caught Exception: " + e);
        }
    }
    
    public void     ReceiveDamage(int damage)
    {
        unitStats.health -= damage;
        ShowDamageTaken(damage);
    }

    public void     AttackTarget()
    {
        if (unitAttackStyle == AttackStyle.Melee) // hvis unit ikke har bue
        {
            if (target.gameObject != null && target.gameObject)
            {
                float distance = Vector3.Distance(gameObject.transform.position, target.transform.position);
                if (target.gameObject.layer == 9 || target.gameObject.layer == 10)
                {
                    target.gameObject.GetComponent<UnitAI>().ReceiveDamage(gameObject.GetComponent<UnitStats>().damage);
                }
                else if (target.gameObject.layer == 11 && distance < 2f || target.gameObject.layer == 12 && distance < 2f)// hvis enemy er tæt på friendly flag eller friendly er tæt på enemy flag
                {
                    AttachFlag(target.gameObject);
                    //sp.SetCarryingFlag(true);
                }
            }
        }
    }

    void            OnTriggerEnter(Collider other)
    {
        try
        {
            if (!sp.IsSetToIdle())
            {
                if (target.gameObject && target.gameObject != null && other.gameObject && other.gameObject != null)
                {
                    switch (unitTeam)
                    {
                        case Team.Enemy:
                            if (other.gameObject.layer == 9 && target.layer != 14 && !gameObject.transform.Find("Armature/root/spine/armUp.L/armDown.L/armHand.L/weapon0(Clone)")) // hvis collider er friendly && target ikke castle && warrior har våben
                            {
                                agent.SetDestination(other.gameObject.transform.position);
                                target = other.gameObject;
                            }
                            break;

                        case Team.Friendly:
                            if (other.gameObject.layer == 10 && target.layer != 13 && !gameObject.transform.Find("Armature/root/spine/armUp.L/armDown.L/armHand.L/weapon0(Clone)")) // hvis collider er enemy && target ikke castle && warrior har våben
                            {
                                agent.SetDestination(other.gameObject.transform.position);
                                target = other.gameObject;
                            }
                            break;
                    }
                }
            }
        }
        catch(Exception e)
        {
            Debug.Log("catched exception: " + e);
        }
    }

    void            OnTriggerStay(Collider other)
    {
        try
        {
            if (!sp.IsSetToIdle())
            {
                switch (unitTeam)
                {
                    case Team.Enemy:
                        if (target.gameObject != null && target.gameObject && !gameObject.transform.Find("Armature/root/spine/armUp.L/armDown.L/armHand.L/weapon0(Clone)"))
                        {
                            if (other.gameObject.layer == 9 && target.layer != 14)
                            {
                                // JEG TROR DET HER BRUGER MEGET CPU POWER
                                if (IsEnemyCloserThenTarget(other))
                                {
                                    target = other.gameObject;
                                    agent.SetDestination(other.gameObject.transform.position);
                                }
                                else if (other.gameObject == target)
                                {
                                    agent.SetDestination(other.gameObject.transform.position);
                                }
                            }
                        }
                        break;

                    case Team.Friendly:
                        if (target.gameObject != null && target.gameObject && !gameObject.transform.Find("Armature/root/spine/armUp.L/armDown.L/armHand.L/weapon0(Clone)"))
                        {
                            if (other.gameObject.layer == 10 && target.layer != 13)
                            {
                                // JEG TROR DET HER BRUGER MEGET CPU POWER
                                if (IsEnemyCloserThenTarget(other))
                                {
                                    target = other.gameObject;
                                    agent.SetDestination(other.gameObject.transform.position);
                                }
                                else if (other.gameObject == target)
                                {
                                    agent.SetDestination(other.gameObject.transform.position);
                                }
                            }
                        }
                        break;
                }

            }
        }
        catch (Exception e)
        {
            Debug.Log("catched exception: " + e);
        }
    }
    
    private bool    IsEnemyCloserThenTarget(Collider other)
    {
        float distanceToOther = Vector3.Distance(gameObject.transform.position, other.transform.position);
        float distanceToTarget = Vector3.Distance(gameObject.transform.position, target.transform.position);

        if (distanceToTarget < distanceToOther + 0.2)
        {
            return false;
        }
        else
            return true;
    }

    public void     AttachFlag(GameObject flag)
    {
        Transform placeHolder = gameObject.transform.Find("Armature/root/spine/neck/head/head_end").transform;
        flag.transform.parent = placeHolder; // sets the flags parrent to head_end(placeHolder)
        flag.transform.position = placeHolder.transform.position;
        flag.transform.localEulerAngles = new Vector3(-90, 0, 0);
    }

    public GameObject GetTarget()
    {
        return target;
    }

    public void     SetTeam(string choosenTeam)
    {
        if (choosenTeam == "Friendly")
            unitTeam = Team.Friendly;

        if (choosenTeam == "Enemy")
            unitTeam = Team.Enemy;
    }

    public Team     GetTeam()
    {
        return unitTeam;
    }

    public void     SetAttackStyle(string choosenStyle)
    {
        if (choosenStyle == "Melee")
            unitAttackStyle = AttackStyle.Melee;

        if (choosenStyle == "Ranged")
            unitAttackStyle = AttackStyle.Ranged;
    }
    
    public bool     IsAttackStyleMelee()
    {
        if (unitAttackStyle == AttackStyle.Melee)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void    ShowDamageTaken(int damage)
    {
        if(TempDamageShower == null || !TempDamageShower.gameObject)
        {
            TempDamageShower = Instantiate(Resources.Load("CharacterAssets/ShowDamage"), gameObject.transform.position + new Vector3(0, 3, 0), mainCam.transform.rotation) as GameObject;
        }
            switch (unitTeam)
            {
            case Team.Friendly:
                    TempDamageShower.GetComponent<ShowDamageScript>().AddDamageReceived(damage);
                    TempDamageShower.GetComponent<ShowDamageScript>().ShowDamage(true, gameObject.transform.position + new Vector3(0, 3, 0));
                break;
            case Team.Enemy:
                    TempDamageShower.GetComponent<ShowDamageScript>().AddDamageReceived(damage);
                    TempDamageShower.GetComponent<ShowDamageScript>().ShowDamage(false, gameObject.transform.position + new Vector3(0, 3, 0));
                break;
        }
    }
}
