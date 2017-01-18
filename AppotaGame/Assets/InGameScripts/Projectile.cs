using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public Transform targetPos;
    float archHeightCalculated = 1;
    Vector3 arrowPos;
    public float speed = 10;

    [Range(0.01f, 60.0f)] 
    public float archHeight;

    
    void Start()
    {
        arrowPos = transform.position;
    }
    
    void Update()
    {
        if (targetPos)
        {
            float x0 = arrowPos.x;
            float z0 = arrowPos.z;
            float x1 = targetPos.position.x; 
            float z1 = targetPos.position.z;
            //distance from arrow til target
            float dist = ( x1 + z1 ) - ( x0 + z0 ); 

            archHeightCalculated = dist / archHeight;
            //next X position of the arrow
            float nextX = Mathf.MoveTowards( transform.position.x, x1, speed * Time.deltaTime );
            //next Z position of the arrow
            float nextZ = Mathf.MoveTowards( transform.position.z, z1, speed * Time.deltaTime * Mathf.Abs(  (z0-z1) / (x0-x1) ));
            //Lerp interpolates between arrow y position and target y position by arrow x,z poition and targer x,z position divided by distance to get precise positions 
            float baseY = Mathf.Lerp(arrowPos.y+1, targetPos.position.y+1, ( (nextX+nextZ) - (x0+z0) ) / dist );
            //arc calculates the height of the current location

            float arc = archHeightCalculated * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
            
            //next position is calculated based on nextX, nextY, baseY, arc and placed in a vector3
            Vector3 nextPos;
            if (targetPos.position.x > arrowPos.x)
            {
                nextPos = new Vector3((nextX), (baseY) + (arc), (nextZ));
            }
            else
            {
                nextPos = new Vector3((nextX), (baseY) + (-arc), (nextZ));
            }
            transform.rotation = LookAt2D(nextPos - transform.position , transform);
            transform.position = nextPos;
            
            //if distance to target is below 1 float, run Arrived method
            if( Vector3.Distance( targetPos.position, gameObject.transform.position ) < 1.5f )
            {
                Arrived();
            } 
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }//deal damage to target warrior and then destroy arrow GamwObject.
    void Arrived()
    {
        targetPos.gameObject.GetComponent<UnitStatePattern>().ReceiveDamage(2);
        Destroy(this.gameObject);
    }
    static Quaternion LookAt2D(Vector2 forward, Transform arrowTransform)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
    }
}
