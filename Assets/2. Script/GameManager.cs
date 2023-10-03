using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string sceneAName;
    public string sceneBName;
    public string sceneCName;


    private void Awake()
    {
        instance = this;

        //sc
    }


}
