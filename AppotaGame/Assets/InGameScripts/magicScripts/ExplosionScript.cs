using UnityEngine;
using System.Collections;

public class ExplosionScript : MonoBehaviour {


    [SerializeField]
    float radius; 
    [SerializeField]
    int power; // damage
    [SerializeField]
    float destructionTime; // the time when the explosion game object is destroyed.
    
    void Start () {
        Vector3 explosionPos = gameObject.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                //rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
                if (hit.gameObject.layer == 10)
                {
                    hit.gameObject.GetComponent<UnitAI>().ReceiveDamage(power);
                }
            }
        }
        Destroy(gameObject, destructionTime);
    }
}
