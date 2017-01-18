using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

public class WarriorMenuButtonScript : MonoBehaviour, IPointerClickHandler //IPointerDownHandler, IPointerUpHandler // IPointerClickHandler
{
    string buttonActionName;
    GameObject menuWarrior;
    //string type;
    //Vector2 touchPoint;
    

    public string getButtonActionName()
    {
        return buttonActionName;
    }
    public void   SetButtonActionName(string btnActionName, string type)
    {
        this.buttonActionName = btnActionName;
        //this.type = type;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        string selected = eventData.selectedObject.GetComponent<WarriorMenuButtonScript>().getButtonActionName();
        EquipAsset(selected);
    }
    
    void Start()
    {
        menuWarrior = GameObject.Find("MenuWarrior");
    }

    public void EquipAsset(string selected)
    {
        menuWarrior.GetComponent<MenuWarriorScript>().attatchAssest(selected);
    }


    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    touchPoint =  eventData.pressPosition;
    //}
    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    if (eventData.position == touchPoint)
    //    {
    //        string selected = eventData.selectedObject.GetComponent<WarriorMenuButtonScript>().getButtonActionName();
    //        EquipAsset(selected);
    //    }
    //}
}
