using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InGameMenuCtrl : MonoBehaviour {

    [SerializeField]
    GameObject selectedMagic;

    [SerializeField]
    Text selectedMagicCrystalsText;
    [SerializeField]
    Text selectedMagicDamageText;
    [SerializeField]
    Text selectedMagicText;

    [SerializeField]
    GameObject wizzard;

   



    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void BombButtonPress()
    {
        wizzard.GetComponent<MagicController>().SetSelectedMagic(SelectedMagic.timeBomb);
        selectedMagicText.text         = BombString();
        selectedMagicDamageText.text   = "100";
        selectedMagicCrystalsText.text = "4";
    }
    public void FireBallButtonPress()
    {
        wizzard.GetComponent<MagicController>().SetSelectedMagic(SelectedMagic.fireBall);
        selectedMagicText.text         = FireString();
        selectedMagicDamageText.text   = "10";
        selectedMagicCrystalsText.text = "2";
    }
    public void CrystalButtonPress()
    {
        wizzard.GetComponent<MagicController>().SetSelectedMagic(SelectedMagic.iceBall);
        selectedMagicText.text         = CrystalString();
        selectedMagicDamageText.text = "1";
        selectedMagicCrystalsText.text = "3";
    }
    public void WallButtonPress()
    {
        wizzard.GetComponent<MagicController>().SetSelectedMagic(SelectedMagic.MagicWall);
        selectedMagicText.text         = WallString();
        selectedMagicDamageText.text   = "N/A";
        selectedMagicCrystalsText.text = "4";
    }



    private string BombString()
    {
        return "The bomb magic is a power full bomb thrown by the wizzard. The bomb has a 6 second delay, so be careful not to throw it in the water.";
    }
    private string WallString()
    {
        return "The wall magic is fired from the wizzard staff. It holds enemy forces behind the wall for a 30 second time period.";
    }
    private string FireString()
    {
        return "The fireball it thorwn by the wizzard. It has a small blast radius, but the fireball explodes on every impact, so attempt to make it bounce as close to enemies as possible.";
    }
    private string CrystalString()
    {
        return "The crystal magic is fired from the wizzard staff. It slows down enemies and makes damage as long as the enemies are in the freezing zone. The crystal magic last for 20 seconds.";
    }

    
}
