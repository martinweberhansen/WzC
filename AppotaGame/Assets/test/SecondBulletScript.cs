using UnityEngine;
using System.Collections;

public class SecondBulletScript : MonoBehaviour
{
    //Vector2 start;
    //Vector2 end;
    //Vector2 mid;
    //float A;
    //float B;
    //float D;
    //float AA;
    //float BB;
    //float DD;
    //float BM;
    //float AAA;
    //float DDD;
    //float a;
    //float b;
    //float c;

    //Vector3 oldTargetPos;
    //public Transform target;
    //private float distance;
    //private Vector3 startPosition;
    //private float damage;
    //private float distanceTraveled;

    //[SerializeField]
    //private float speed;
    //[SerializeField]
    //private float height;
    //[SerializeField]
    //private float angle;

    //public float Damage
    //{
    //    get { return damage; }
    //    set { damage = value; }
    //}

    //public Vector3 StartPosition
    //{
    //    set { startPosition = value; }
    //}

    //void TargetDistance()
    //{
    //    Vector3 targetPos = target.position;
    //    targetPos.y = startPosition.y;
    //    distance = Vector3.Distance(startPosition, targetPos);
    //    targetPos = startPosition;
    //    targetPos.y = transform.position.y;
    //    distanceTraveled = Vector3.Distance(targetPos, transform.position);
    //}
    //// Use this for initialization
    //public void Initialize()
    //{
    //    TargetDistance();
    //    float LastAngle = 180 - (angle + 90);

    //    float calc1 = Mathf.Sin(LastAngle * Mathf.Deg2Rad);
    //    float calc2 = distance / 2;
    //    float calc3 = Mathf.Sin(angle * Mathf.Deg2Rad);


    //    height = (calc1 * calc2) * calc3;
    //    Debug.Log("sinY:" + calc1 + "dist" + calc2 + "sinX" + calc3 + "finalheight" + height);



    //    //height = target.position.y - startPosition.y;
    //    distanceTraveled = 0;

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    TargetDistance();
    //    if (distance <= distanceTraveled)
    //    {
    //        if (transform.position == target.position)
    //        {
    //            gameObject.SetActive(false);
    //        }
    //        transform.position = target.position;
    //    }
    //    else
    //    {
    //        TurnTowardsTarget();            
    //        transform.Translate(0, 0, speed * Time.deltaTime);
    //        BulletPath();
    //    }
    //}
    //void OnTriggerEnter(Collider collider)
    //{
    //    Debug.Log("THIS WAS COLLIDER: " + collider.gameObject.name);
    //    if (collider.gameObject == target)
    //    {
    //        //TROR GODT JEG KAN SLETTE DEN ENE, FORDI JEG TJEKKER TARGET!!
    //        if (collider.gameObject.layer == 10)
    //        {
    //            Debug.Log("BOW MAKING DAMAGE");
    //            collider.gameObject.GetComponent<UnitAI>().ReceiveDamage(2);
    //        }
    //        else if (collider.gameObject.layer == 9)
    //        {
    //            Debug.Log("BOW MAKING DAMAGE");
    //            collider.gameObject.GetComponent<UnitAI>().ReceiveDamage(2);
    //        }

    //        //giv skade og fjern bullet
    //        //GameObject.Destroy(this.gameObject);
    //    }
    //}


    //void TurnTowardsTarget()
    //{
    //    Vector3 targetPos = target.position;
    //    targetPos.y = transform.position.y;
    //    transform.LookAt(targetPos);
    //}

    //void BulletPath()
    //{       
        
    //    oldTargetPos = target.position;
    //    start = new Vector2(0, startPosition.y);
    //    end = new Vector2(distance, target.position.y);
    //    mid = new Vector2(distance / 2, startPosition.y + height);
    //    A = -Mathf.Pow(start.x, 2) + Mathf.Pow(mid.x, 2);
    //    B = -start.x + mid.x;
    //    D = -start.y + mid.y;
    //    AA = -Mathf.Pow(mid.x, 2) + Mathf.Pow(end.x, 2);
    //    BB = -mid.x + end.x;
    //    DD = -mid.y + end.y;
    //    BM = -(BB / B);
    //    AAA = BM * A + AA;
    //    DDD = BM * D + DD;
    //    a = DDD / AAA;
    //    b = (D - A * a) / B;
    //    c = start.y - a * Mathf.Pow(start.x, 2) - b * start.x;
        
    //    float tempHeight = (float)(a * Mathf.Pow(distanceTraveled, 2) + b * distanceTraveled + c);
    //    Vector3 heightToBe = new Vector3(transform.position.x, tempHeight, transform.position.z);
    //    transform.position = heightToBe;


    //}
}
