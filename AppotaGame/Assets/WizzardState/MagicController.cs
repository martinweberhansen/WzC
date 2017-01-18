using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum WizzardState { sideways, vertical, idle };
public enum SelectedMagic { fireBall, MagicWall, iceBall, timeBomb }

public class MagicController : MonoBehaviour {

    public WizzardState wizzardState;
    public SelectedMagic selectedMagic;

    public GameObject arrow;
    public GameObject wizzard;

    public GameObject magicWall;
    public GameObject fireBall;
    public GameObject iceBall;
    public GameObject timeBomb;

    public GameObject shootMagicButton;
    public GameObject wizzardGlow;

    public Slider slider;

    private RectTransform fillRect;

    private WizzardStatePattern wizzardStatePattern;
    private Quaternion shootDirection;

    private bool movingUp;
    private float curValue;
    private float targetValue;
    private float fillSpeed = 1.0f;



    void Start()
    {
        //movingUp = true;
        //fillRect = slider.fillRect;
        wizzardState = WizzardState.idle;
        selectedMagic = SelectedMagic.fireBall;
        wizzardStatePattern = gameObject.GetComponent<WizzardStatePattern>();
    }

    void Update()
    {
        //if(movingUp && curValue < 100)
        //{
        //    movingUp = true;
        //    targetValue = 100;
        //    curValue = Mathf.MoveTowards(curValue, targetValue, Time.deltaTime * fillSpeed);
        //}
        //else if (!movingUp && curValue > 0)
        //{

        //}

    }
    
    public void MagicButtonPress()
    {
        if(wizzardState == WizzardState.idle)
        {
            arrow.transform.eulerAngles = new Vector3(-90.0f, 0.0f, 0.0f);
            wizzardState = WizzardState.sideways;
        }
        else if(wizzardState == WizzardState.sideways)
        {
            wizzardState = WizzardState.vertical;
        }
        else if (wizzardState == WizzardState.vertical)
        {
            shootDirection = arrow.transform.rotation;
            SubtractCrystal(selectedMagic);
            StartCoroutine("FireAfterAnimationIsDone");
        }
    }
   
    public void SetWizzardState(int num)
    {
        if(num == 0)
        {
            wizzardState = WizzardState.idle;
            arrow.transform.eulerAngles = new Vector3(-90.0f, 0.0f, 0.0f);
        }
        else if(num == 1)
        {
            wizzardState = WizzardState.sideways;
        }else if(num == 2)
        {
            wizzardState = WizzardState.vertical;
        }
    }

    private void FireMagic(GameObject selectedMagic, Quaternion shootDir)
    {
        Debug.Log("FIRE MAGIC WOKRS"+selectedMagic.name);
        GameObject projectile = Instantiate(selectedMagic, wizzard.transform.position, shootDir) as GameObject;
        projectile.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(10500, 0, 000));
    }
    
    IEnumerator FireAfterAnimationIsDone()
    {
        wizzardStatePattern.ResumeWizzardAnim();
        wizzardState = WizzardState.idle;
        yield return new WaitForSeconds(1);
        if(selectedMagic == SelectedMagic.fireBall)
        {
            FireMagic(fireBall, shootDirection);
        }
        else if(selectedMagic == SelectedMagic.MagicWall)
        {
            FireMagic(magicWall, shootDirection);
        }
        else if(selectedMagic == SelectedMagic.timeBomb)
        {
            FireMagic(timeBomb, shootDirection);
        }
        else if(selectedMagic == SelectedMagic.iceBall)
        {
            FireMagic(iceBall, shootDirection);
        }
        StartCoroutine("AfterMagicIsShotWaitFor3sec");
    }

    IEnumerator AfterMagicIsShotWaitFor3sec()
    {
        shootMagicButton.SetActive(false);
        wizzardGlow.SetActive(false);
        yield return new WaitForSeconds(3);
        shootMagicButton.SetActive(true);
        wizzardGlow.SetActive(true);
    }

    private void SubtractCrystal(SelectedMagic SM)
    {
        int cost = 0;
        if(SM == SelectedMagic.fireBall)
        {
            cost = 2;
        }
        GameAI.getGameAISingelton.SubtractCrystals(cost);
    }

    public SelectedMagic GetSelectedMagic()
    {
        return selectedMagic;
    }

    public void SetSelectedMagic(SelectedMagic SM)
    {
        selectedMagic = SM;
    }

    public void Slider()
    {

        //slider.fillRect
        //slider.GetComponent<RectTransform>().
    }


    //curValue = Mathf.MoveTowards(curValue, targetValue, Time.deltaTime* fillSpeed);

    //Vector2 fillAnchor = fillRect.anchorMax;
    //fillAnchor.x = Mathf.Clamp01(curValue/slider.maxValue);
    //fillRect.anchorMax = fillAnchor;




}
