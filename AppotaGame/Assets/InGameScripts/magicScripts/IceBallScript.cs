using UnityEngine;
using System.Collections;

public class IceBallScript : MonoBehaviour {


    [SerializeField]
    GameObject iceCrystalFloor;
    [SerializeField]
    GameObject explosion;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == 15)
        {
            Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
        }
        if(col.gameObject.layer == 8)
        {
            Instantiate(explosion, gameObject.transform.position, iceCrystalFloor.transform.rotation);
            Instantiate(iceCrystalFloor, gameObject.transform.position, iceCrystalFloor.transform.rotation);
            Destroy(gameObject);
        }
    }
}
