using UnityEngine;
using System.Collections;

public class menuWarriorAssetScript : MonoBehaviour {

    public string weaponName;
    public string armorName;
    public string helmetName;

    public void setAssets(string helmet,string armor,string weapon)
    {
        this.helmetName = helmet;
        this.armorName = armor;
        this.weaponName = weapon;
    }
    public void     setHelmetName(string name)
    {
        this.helmetName = name;
    }
    public string   getHelmetName()
    {
        return this.helmetName;
    }

    public void     setArmorName(string name)
    {
        this.armorName = name;
    }
    public string   getArmorName()
    {
        return this.armorName;
    }

    public void     setWeaponName(string name)
    {
        this.weaponName = name;
    }
    public string   getWeaponName()
    {
        return this.weaponName;
    }

    
}
