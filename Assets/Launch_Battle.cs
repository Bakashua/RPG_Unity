using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Launch_Battle : MonoBehaviour
{
    public string sceneAName;
    public string sceneBName;
    //public string sceneCName;
    [SerializeField] public List<AsyncOperation> sceneloading = new List<AsyncOperation>();

    [Header("____________")]
    public SO_Encounter Encounter;


    [Header("____________")]

    //public GameObject VFX;
    public GameObject transition;
    public GameObject launchBtn;
    public GameObject mainCam;

    public Slider bar; 
    float loadProgress = 0;

    private void Awake()
    {
        //SceneManager.LoadSceneAsync(sceneAName));
        //if (instance_BSM != null && instance_BSM != this)
        //{
        //    Destroy(this);
        //}
        //else
        //{
        //    instance_BSM = this;
        //}

        // Ensure that this GameObject persists between scenes
        DontDestroyOnLoad(gameObject);
    }



    public void _StartBattle()
    {
        launchBtn.SetActive(false);
        transition.SetActive(true);
        Invoke("Trigger_Combat", 0.1f);
    }


    public void Trigger_Combat()
    {

        Scene currentScene = SceneManager.GetActiveScene();
        sceneAName = currentScene.name;

        //sceneloading.Add(SceneManager.UnloadSceneAsync(sceneAName));
        SceneManager.UnloadSceneAsync(sceneAName);
        //print(sceneAName);
        sceneloading.Add(SceneManager.LoadSceneAsync(sceneBName, LoadSceneMode.Additive));


        //AsynchronousOxperation loadingOperation = SceneManager.LoadSceneAsync(sceneToLoad);

        //AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(sceneBName, LoadSceneMode.Additive);
        //float loadProgress = loadingOperation.progress;

        StartCoroutine(SceneProgressTransition());
    }

    IEnumerator SceneProgressTransition()
    {
        for (int i = 0; i < sceneloading.Count; i++)
        {
            while (!sceneloading[i].isDone)
            {
                foreach (AsyncOperation operation in sceneloading)
                {
                    loadProgress += operation.progress; 
                }

                loadProgress = (loadProgress / sceneloading.Count) * 100f;
                //bar.value = Mathf.RoundToInt(loadProgress);
                bar.value = loadProgress;

                yield return null;
            }
        }

        mainCam.SetActive(false);
        transition.SetActive(false);
    }


    public void End_Combat()
    {
        gameObject.SetActive(true);
        SceneManager.UnloadSceneAsync(sceneBName);
        mainCam.SetActive(true);
        launchBtn.SetActive(true);
        //SceneManager.LoadScene(sceneAName);
    }




}
