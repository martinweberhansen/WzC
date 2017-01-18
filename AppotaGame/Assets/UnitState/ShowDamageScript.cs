using UnityEngine;
using System.Collections;

public class ShowDamageScript : MonoBehaviour {

    int currentDamageReceived;
    TextMesh damageMesh;
    float lifeTimer;

    public void ShowDamage( bool isFriendly, Vector3 position)
    {
        gameObject.transform.position = position;
        if (!isFriendly)
        {
            damageMesh = gameObject.GetComponent<TextMesh>();
            damageMesh.color = Color.red;
        }
        else 
        {
            damageMesh = gameObject.GetComponent<TextMesh>();
            damageMesh.color = Color.blue;
        }
        damageMesh.text = "-"+ currentDamageReceived;
        lifeTimer = 0;
    }

    void Update()
    {
        lifeTimer += Time.deltaTime;
        if(lifeTimer > 3)
        {
            Destroy(gameObject);
        }
        gameObject.transform.Translate(0, 0.02f, 0);
    }

    public int GetDamageReceived()
    {
        return currentDamageReceived;
    }
    public void AddDamageReceived(int damage)
    {
        currentDamageReceived += damage;
    }

  
}
