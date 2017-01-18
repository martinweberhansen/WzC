using UnityEngine;
using System.Collections;

public class MagicWallDestructionScript : MonoBehaviour {

	void Start()
    {
        Destroy(gameObject,20);
    }
}
