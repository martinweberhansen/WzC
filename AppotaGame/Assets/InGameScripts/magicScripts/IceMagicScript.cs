using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IceMagicScript : MonoBehaviour {

    float timer;
    List<GameObject> freezedWarriors;
 
    void Start()
    {
        freezedWarriors = new List<GameObject>();
        StartCoroutine(KeepAliveFor20Seconds());
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1)
        {
            foreach(GameObject w in freezedWarriors)
            {
                w.gameObject.GetComponent<UnitAI>().ReceiveDamage(1);
                timer = 0;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            if (other.gameObject.layer == 10 && !freezedWarriors.Contains(other.gameObject))
            {
                other.gameObject.GetComponent<UnitStatePattern>().FreezeWarrior();
                freezedWarriors.Add(other.gameObject);
            }
        }
    }

    IEnumerator KeepAliveFor20Seconds()
    {
        yield return new WaitForSeconds(20);
        foreach(GameObject w in freezedWarriors)
        {
            w.GetComponent<UnitStatePattern>().Unfreeze();
        }
        Destroy(gameObject);
    }
}
