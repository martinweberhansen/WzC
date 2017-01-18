using UnityEngine;
using System.Collections;

public class ScrollButton : MonoBehaviour {

    public Item item;
    public bool locked;

    public void initialize(Item item, bool locked)
    {
        this.locked = locked;
        this.item = item;
    }
}
