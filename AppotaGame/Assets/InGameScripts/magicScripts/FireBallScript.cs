using UnityEngine;
using System.Collections;

public class FireBallScript : MonoBehaviour {
    
    public GameObject explosion;

    void Update()
    {
        //gameObject.transform.Rotate(80, 80, 80);  // crazy mode!!!
        StartCoroutine("DecreaseSizeOverTime");
    }

    IEnumerator DecreaseSizeOverTime()
    {
        yield return new WaitForSeconds(3);
        gameObject.transform.localScale -= new Vector3(0.3f, 0.3f, 0.3f);
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision col)
    {
        gameObject.transform.FindChild("FireTrail2").gameObject.GetComponent<ParticleSystem>().Stop();
        if (col.gameObject.layer == 8 || col.gameObject.layer == 15)
        {
            Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
        }
        
    }
}
