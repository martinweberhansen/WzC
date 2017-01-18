using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine.UI;
using System.Xml.Serialization;
using System.IO;

public class SplashScreen : MonoBehaviour
{
    [SerializeField]
    GameObject loadingImage;
    [SerializeField]
    float rotatespeed;
    [SerializeField]
    GameObject LoadingComplete_text;
    [SerializeField]
    GameObject nowLoading_Text;
    [SerializeField]
    string MenuSceneName;
    public bool allowLoading;
    AsyncOperation op;
    float timer;
    [SerializeField]
    float timeToLoad;
    [SerializeField]
    bool doneLoading;
    [SerializeField]
    bool doneloadingXML;
    bool beginLoadingXML;
    public TextAsset xmldb;
    public List<Unit> units;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(LoadScene());
        allowLoading = false;
    }
    void Update()
    {
        if (Input.touchCount > 4)
        {
            SceneManager.LoadScene("testscene");
        }
        if (!beginLoadingXML)
        {
            beginLoadingXML = true;
            StartCoroutine(WorldObject.GetworldObject.LoadFromXML((callback) =>
            {
                if (callback)
                {
                    doneloadingXML = true;
                }
            }));
        }

        timer += Time.deltaTime;
        loadingImage.transform.Rotate(new Vector3(0, rotatespeed * Time.deltaTime, 0)); //rotate loading image

        //wait for timer and loading to change text
        if (timeToLoad < timer)
        {
            if (doneLoading && doneloadingXML)
            {
                nowLoading_Text.SetActive(false);
                loadingImage.SetActive(false);
                LoadingComplete_text.SetActive(true);

                //load next level on touch
                if (Input.touchCount > 0)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Ended && !WorldObject.GetworldObject.isFading)
                    {

                        StartCoroutine(WorldObject.GetworldObject.ChangeScene(0, FadeDirection.Fadeout, (callback) =>
                        {
                            if (callback)
                            {
                                allowLoading = true;
                            }
                        }));
                    }
                }
                else if (Input.GetButtonUp("Fire1"))
                {
                    StartCoroutine(WorldObject.GetworldObject.ChangeScene(0, FadeDirection.Fadeout, (callback) =>
                    {
                        if (callback)
                        {
                            allowLoading = true;
                        }
                    }));
                }
            }
        }
    }
    private IEnumerator LoadScene()
    {
        op = SceneManager.LoadSceneAsync(MenuSceneName);
        op.allowSceneActivation = false; //stops changing the screen after loading is done

        while (op.progress < 0.9f) //loading progress
        {
            yield return null;
        }
        print("done loading next scene");
        doneLoading = true;
        while (!allowLoading)
        {
            // Wait for change scene push
            yield return null;
        }
        print("changing scene");
        op.allowSceneActivation = true; //change to next screen
        yield return null;
    }
}
