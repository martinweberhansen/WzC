using UnityEngine;
using System.Collections;

public class magicWallScript : MonoBehaviour {

    [SerializeField]
    GameObject magicWall;
    [SerializeField]
    GameObject explosion;

    void OnCollisionEnter(Collision col)
    {
        Instantiate(explosion, gameObject.transform.position, magicWall.transform.rotation);

        Instantiate(magicWall, gameObject.transform.position,magicWall.transform.rotation);
        
        Destroy(gameObject);
    }
}
