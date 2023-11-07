using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigationManager : MonoBehaviour
{
    public string Scene_City;
    public string Scene_MainMap;
    public string Scene_StartMenu;
    public string Scene_Dungeon;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Listener_LoadCity()
    {
        Destroy(FindObjectOfType<AudioListener>());
        SceneManager.LoadSceneAsync(Scene_City);
    }

    public void Listener_UnloadCity()
    {
        SceneManager.UnloadSceneAsync(Scene_City);
    }

    public void Listener_LoadDungeon()
    {
        Destroy(FindObjectOfType<AudioListener>());
        SceneManager.LoadSceneAsync(Scene_Dungeon);
    }

    public void Listener_UnloadDungeon()
    {
        SceneManager.UnloadSceneAsync(Scene_Dungeon);
    }

    public void Listener_LoadMainMap()
    {
        Destroy(FindObjectOfType<AudioListener>());
        SceneManager.LoadSceneAsync(Scene_MainMap);
    }

    public void Listener_UnloadMainMap()
    {
        SceneManager.UnloadSceneAsync(Scene_MainMap);
    }

    public void Listener_LoadStartMenu()
    {
        Destroy(FindObjectOfType<AudioListener>());
        SceneManager.LoadSceneAsync(Scene_StartMenu, LoadSceneMode.Additive);
    }

    public void Listener_UnloadStartMenu()
    {
        SceneManager.UnloadSceneAsync(Scene_StartMenu);
    }

}
