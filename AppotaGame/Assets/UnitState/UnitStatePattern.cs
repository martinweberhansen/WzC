using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnitStatePattern : MonoBehaviour {
    
    private Animator anim;
    private Animator bowAnim;
    private UnitAI AI;
    private NavMeshAgent agent;
    private AudioSource audioSource;

    int stretchBowHash = Animator.StringToHash("StretchBow");
    
    public bool inRangeToAttack;
    private bool dead = false;
    public bool carryingFlag;
    private bool isSetToIdle;

    [HideInInspector]
    public IUnitState currentState;
    [HideInInspector]
    public IdleState IdleState;
    [HideInInspector]
    public WalkState WalkState;
    [HideInInspector]
    public AttackState AttackState;
    [HideInInspector]
    public DeathState DeathState;
    
    void Start()
    {
        AI          = gameObject.GetComponent<UnitAI>();
        anim        = gameObject.GetComponent<Animator>();
        agent       = gameObject.GetComponent<NavMeshAgent>();

        if (!AI.IsAttackStyleMelee()) // har bue
        {
            GameObject weapon = gameObject.transform.Find("Armature/root/spine/armUp.R/armDown.R/armHand.R/weapon1(Clone)").transform.gameObject;
            bowAnim = weapon.GetComponent<Animator>();
        }
        else // ellers tilføj Audio klip fra slagvåben til warrior
        {
            audioSource = gameObject.transform.GetComponentInChildren<AudioSource>();
        }
        
        AttackState = new AttackState(this, anim, AI);
        IdleState   = new IdleState(this, anim);
        WalkState   = new WalkState(this, anim);
        DeathState  = new DeathState(this, anim);

        agent.stoppingDistance = 1.5f;
        agent.speed = gameObject.GetComponent<UnitStats>().speed*2;
        currentState = IdleState;
    }

    
    void Update()
    {
        if (currentState != DeathState)
        {
            currentState.UpdateState();
            currentState.PlayAnimation();
        }
    }

    public bool IsSetToIdle()
    {
        return isSetToIdle;
    }

    public void SetToIdle()
    {
        isSetToIdle = true;
        agent.Stop();
    }

    public void ReceiveDamage(int damage)
    {
        AI.ReceiveDamage(damage);
    }

    public void setInRangeToAttack(bool ans)
    {
        inRangeToAttack = ans;
    }

    public bool InRangeToAttack()
    {
        return inRangeToAttack;
    }

    public void Kill()
    {
        if (!dead)
        {
            agent.Stop();
            dead = true;
            Destroy(gameObject, 6f);

            UnitAI.Team t = AI.GetTeam();
            if (t == UnitAI.Team.Friendly)
            {
                GameAI.getGameAISingelton.RemoveFriendlyUnit(gameObject);
            }
            else
            {
                GameAI.getGameAISingelton.RemoveEnemyUnit(gameObject);
            }
            if (CarryingFlag())
            {
                int ojbs = gameObject.transform.Find("Armature/root/spine/neck/head/head_end").transform.childCount;

                for (int i = 0; i < ojbs; i++)
                {
                    if (gameObject.transform.Find("Armature/root/spine/neck/head/head_end").GetChild(i).name.StartsWith("flag"))
                    {
                        Transform flag = gameObject.transform.Find("Armature/root/spine/neck/head/head_end").GetChild(i);
                        flag.parent = null;
                        flag.transform.Translate(0, 0, -1.0f);
                        flag.transform.localEulerAngles = new Vector3(-90, 0, 0);
                    }
                }
            }
        }
    }

    public bool IsDead()
    {
        return dead;
    }

    public float GetCurrentMovingSpeed()
    {
        return gameObject.GetComponent<Rigidbody>().velocity.magnitude;      //to get a Vector3 representation of the velocity and get magnitude
    }

    public void StopMoving()
    {
        gameObject.GetComponent<Rigidbody>().velocity.Set(0, 0, 0);
    }

    public bool CarryingFlag()
    {
        if( gameObject.transform.Find("Armature/root/spine/neck/head/head_end").childCount > 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void PrintText(string s)
    {
        Text textbox = GameObject.Find("TEST").GetComponent<Text>();
        textbox.text = s;
    }

    public void StretchBow()
    {
        bowAnim.SetTrigger(stretchBowHash);
    }

    public void ResetStretchBow()
    {
        bowAnim.ResetTrigger(stretchBowHash);
    }

    public void PlaySoundOfAttackingWeapon()
    {
        if (audioSource)
        {
            audioSource.Play();
        }
    }

    public void FreezeWarrior()
    {
        Debug.Log("I AM FREEZING");
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        agent.Stop();
        gameObject.GetComponent<UnitAI>().enabled = false;
    }

    public void Unfreeze()
    {
        Debug.Log("I AM UN FREEZING");
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        agent.Resume();
        gameObject.GetComponent<UnitAI>().enabled = true;
    }
}
