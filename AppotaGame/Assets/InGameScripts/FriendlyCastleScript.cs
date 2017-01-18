using UnityEngine;
using System.Collections;
using System.Collections.Generic; //need this for List

public class FriendlyCastleScript : MonoBehaviour {
    
    void OnTriggerEnter(Collider other)
    {
        CheckNeedToMoveFlag(other);
    }

    void OnTriggerStay(Collider other)
    {
        CheckNeedToMoveFlag(other);
    }

    private void CheckNeedToMoveFlag(Collider other)
    {
        float distance = Vector3.Distance(gameObject.transform.position, other.transform.position);

        if (other.gameObject.GetComponent<UnitStatePattern>().CarryingFlag() && distance < 4)
        {
            Transform placeHolder = other.gameObject.transform.Find("Armature/root/spine/neck/head/head_end").transform;
            GameObject toBeDesroyed = null;

            for (int j = 1; j < placeHolder.childCount + 1; j++)
            {
                Transform t = placeHolder.GetChild(j);
                t.parent = null;

                foreach (GameObject flag in GameAI.getGameAISingelton.GetRedFlagList())
                {
                    if (t.gameObject.name == flag.name)
                    {
                        toBeDesroyed = flag;
                    }
                }
            }
            GameAI.getGameAISingelton.RemoveEnemyFlag(toBeDesroyed);
            Destroy(toBeDesroyed);
            other.gameObject.GetComponent<UnitStatePattern>().Kill();
        }
    }
}
