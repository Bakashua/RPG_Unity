using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Combat_Tiles : MonoBehaviour
{
    public int x;
    public int z;
    public Material NeutralColor;
    public Vector2Int Coord ;
    public GameObject currentHolder;

    public bool Walkable = true;

    private void Start()
    {
        Coord = new Vector2Int(x, z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pawn" || other.tag == "Enemy")
        {
            currentHolder =  other.GetComponentInParent<Combat_Movement>().gameObject;
            other.GetComponentInParent<Combat_Movement>().currTiles = this.gameObject;
        }
    }


    public void ResetColor()
    {
        gameObject.GetComponent<Renderer>().material = NeutralColor;
    }

}

//  fonction qui gere tous les etats 
// fonction qui donne l etats on chara quand il rentre dans la tile