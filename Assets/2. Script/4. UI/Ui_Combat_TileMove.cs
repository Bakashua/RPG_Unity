using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_Combat_TileMove : MonoBehaviour
{
    public GameObject Tile;
    public Material neutral;
    public Material select;


    // using event trigger to trigger those functions
    public void SelectBtn()
    {
        Tile.GetComponent<MeshRenderer>().material = select;
    }

    public void DeselectBtn()
    {
        Tile.GetComponent<MeshRenderer>().material = neutral;
    }

}
