using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapSelection : MonoBehaviour {

    public string sceneName;
    public int id;
    [SerializeField]
    Text nameText;



    public void UpdateButton(Level level)
    {
        nameText.text = level.name;
        if (level.locked)
        {
            this.gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
            this.gameObject.GetComponent<Button>().enabled = false;
        }
    }

    public string getSceneName()
    {
        return this.sceneName;
    }

}
