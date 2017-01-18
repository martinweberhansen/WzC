using UnityEngine;
using System.Collections;

public class testshoot : MonoBehaviour {

    UnitAI unitAI;
    UnitStatePattern sp;
    
    [SerializeField]
    GameObject bullet;
    [SerializeField] // skal fjernes efter test
    GameObject target;

    //[SerializeField]
    //float timer;
    //[SerializeField]
    //float timermax = 2;
    
    void Start () {
        bullet = Resources.Load("bullet") as GameObject;
        sp = gameObject.GetComponent<UnitStatePattern>();
        unitAI = gameObject.GetComponent<UnitAI>();
    }

    public void ShootBow()
    {
        target = unitAI.GetTarget();
        
        if (target != null)
        {
            float distance = Vector3.Distance(gameObject.transform.position, target.transform.position);
            if (sp.InRangeToAttack() && target.gameObject.layer == 9 || sp.InRangeToAttack() && target.gameObject.layer == 10)
            {
                //Quaternion rotation = Quaternion.Euler(this.transform.rotation.x, 90, this.transform.rotation.z);
                GameObject arrow = Instantiate(bullet, this.transform.position, this.transform.rotation) as GameObject;

                Projectile p = arrow.GetComponent<Projectile>();
                p.targetPos = target.transform;
            }
            
            else if ( distance < 2 && target.gameObject.layer == 11 || distance < 2 &&  target.gameObject.layer == 12)
            {
                unitAI.AttachFlag(target.gameObject);
                //sp.SetCarryingFlag(true);
            }
        }
    }
}
