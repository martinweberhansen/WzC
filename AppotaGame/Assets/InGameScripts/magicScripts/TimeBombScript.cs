using UnityEngine;
using System.Collections;

public class TimeBombScript : MonoBehaviour {

    [SerializeField]
    GameObject explosion;

    public int rotateSpeed = 10;

    void Start()
    {
        StartCoroutine( ExplodeAfterSeconds() );
    }

    void Update()
    {
        gameObject.transform.Rotate(rotateSpeed , rotateSpeed , rotateSpeed);
    }

    IEnumerator ExplodeAfterSeconds()
    {
        yield return new WaitForSeconds(5);
        Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject, 0.2f);
    }

}
