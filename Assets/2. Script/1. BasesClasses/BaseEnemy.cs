using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BaseEnemy : BaseClass
{
    public enum Type
    { Grass, Fire, Water, Air, light, dark }
    public enum Rarety
    {Comon, Uncomon, Rare, Superrare, }


    [Header("TYPE AND RARETY")]
    public Type enemyType;
    public Rarety enemyRarety;
    
    [Header("REWARD")]
    public int exp;
    public int gold;
    //public GameObject itemDrop;
    //public GameObject DropRate;

    [Header("GRAPHIC")]
    // graphics
    // model,
    // menu image,
    // battler
    private string deleteme;



}





// GRAPHIC
// image / model

// REWARD
// Exp
// Gold
// Item (type / drop rate)

// Ia condition action list des skills 
// skill + rating prio of action)
// CONDITION : always / turn x + y (*z) / hp range / mp range / state / party level / switch