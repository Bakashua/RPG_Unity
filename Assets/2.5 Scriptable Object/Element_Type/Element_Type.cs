using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Element_Type", menuName = "My Game/Status_Weakness/New Element Type")]
public class Element_Type : ScriptableObject
{
    public string ElementName;
    public int ElementKey;
    public Sprite ElementIcon;

}
