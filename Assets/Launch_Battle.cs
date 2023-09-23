using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Launch_Battle : MonoBehaviour
{
    public string sceneAName;
    public string sceneBName;
    public GameEvent onBattleStart;


    private bool isSceneAActive = true;
    
    private void Awake()
    {
        // Ensure that this GameObject persists between scenes
        DontDestroyOnLoad(gameObject);
    }

    public void Trigger_Combat()
    {
        // Get the currently active scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Get the name of the currently active scene
        sceneAName = currentScene.name;

        SceneManager.LoadScene(sceneBName);

        //Battle_GUI_Manager BM = FindObjectOfType<Battle_GUI_Manager>();
        //BM.Launch_Battle = this;
    }

    public void End_Combat()
    {

        SceneManager.LoadScene(sceneAName);
    }


}
