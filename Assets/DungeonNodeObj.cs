using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonNodeObj : MonoBehaviour
{
    [Header("______Data Node______")]
    public NodeType Type;
    public Image img;
    public List<Sprite> IconList = new();
    public string deletme;

    [Header("______Data Event______")]
    public SO_Encounter Encounter;


    private void Start()
    {
        Invoke("Initilisation", 0.1f);
        //Initilisation();
    }

    public void Initilisation()
    {
        switch (Type)
        {
            case (NodeType.Boss):
                img.sprite = IconList[4];

                break;

            case (NodeType.EliteEnemy):
                img.sprite = IconList[3];

                break;

            case (NodeType.MinorEnemy):
                img.sprite = IconList[5];

                break;

            case (NodeType.Mystery):
                img.sprite = IconList[6];

                break;

            case (NodeType.RestSite):
                img.sprite = IconList[1];

                break;

            case (NodeType.Store):
                img.sprite = IconList[0];

                break;

            case (NodeType.Treasure):
                img.sprite = IconList[2];

                break;


        }
    }

}

