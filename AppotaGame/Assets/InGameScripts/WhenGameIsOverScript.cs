using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WhenGameIsOverScript : MonoBehaviour {


    [SerializeField]
    GameObject smallBlue;
    [SerializeField]
    GameObject smallYellow;
    [SerializeField]
    GameObject smallGreen;
    [SerializeField]
    GameObject bigBlue;
    [SerializeField]
    GameObject bigYellow;
    [SerializeField]
    GameObject bigGreen;

    //brug samme værdier som fra CameraMovement component på Main Camera GameObject.
    [SerializeField]
    int mapSizeLeft;
    [SerializeField]
    int mapSizeRight;
    [SerializeField]
    GameObject gameOverPopup;
    [SerializeField]
    Text WhenGameOverText;

    public void InitializeGameOver(bool friendlyWon)
    {
        StartCoroutine("KeepFireworksGoingFor1min");
    }

    IEnumerator KeepFireworksGoingFor1min()
    {
        List<GameObject> fireworks = new List<GameObject>();
        fireworks.Add(smallBlue);
        fireworks.Add(smallYellow);
        fireworks.Add(smallGreen);
        fireworks.Add(bigBlue);
        fireworks.Add(bigYellow);
        fireworks.Add(bigGreen);

        System.Random randFireWork = new System.Random();
        System.Random randDir = new System.Random();

        float time = 0;
            while (time< 60)
            {
                Vector3 location = new Vector3(randDir.Next(mapSizeLeft, mapSizeRight), randDir.Next(1, 30), randDir.Next(1, 30));
                Instantiate(fireworks[randFireWork.Next(0, fireworks.Count)], location, transform.rotation);
                time += Time.deltaTime;
                yield return null;
            }
        }

    public void ShowGameOverMenu(string str)
    {
        StartCoroutine(DisplayGameOverPopup(str));
    }

    IEnumerator DisplayGameOverPopup(string str)
    {
        yield return new WaitForSeconds(4);
        WhenGameOverText.text = str;
        gameOverPopup.SetActive(true);
    }
}
