using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Damage_Matrice : MonoBehaviour
{
    public Chara_BaseStats chara;
    public float[,] matriceATK; // = new float[,]
 //{
 //       // les coordonnée sont hors tableau, ex 0.5 de ligne 1 = coordonnée [1, 3] 
 //        {1, 2, 0.5f, 1.5f, 0, 0, 0}, // ligne = atk colone = def
 //        {1, 2, 0.5f, 1.5f, 0, 0, 0}, // entre {} sont les coefs multiplicateur
 //        {1, 2, 0.5f, 1.5f, 0, 0, 0},
 //        {1, 2, 0.5f, 1.5f, 0, 0, 0},
 //        {1, 2, 0.5f, 1.5f, 0, 0, 0},
 //        {1, 2, 0.5f, 1.5f, 0, 0, 0},
 //        {1, 2, 0.5f, 1.5f, 0, 0, 0},
 //};

    Dictionary<string, int> dicoATK = new Dictionary<string, int>();

    void Start()
    {
        //dicoATK.Add("slash", 0); // = ligne 0
        //dicoATK.Add("bash", 1); // = ligne 0
        //dicoATK.Add("pierce", 2); // = ligne 0
        //dicoATK.Add("water", 3); // = ligne 1
        //dicoATK.Add("fire", 4);
        //dicoATK.Add("wind", 5);
        //dicoATK.Add("earth", 6);
        //dicoATK.Add("light", 7);
        //dicoATK.Add("darkness", 8);

        dicoATK.Add("Slash", 0); // = ligne 0
        dicoATK.Add("Bash", 1); // = ligne 0
        dicoATK.Add("Pierce", 2); // = ligne 0
        dicoATK.Add("Water", 3); // = ligne 1
        dicoATK.Add("Fire", 4);
        dicoATK.Add("Wind", 5);
        dicoATK.Add("Earth", 6);
        dicoATK.Add("Light", 7);
        dicoATK.Add("Dark", 8);

        if (gameObject.layer == 0)
        {
            chara = GetComponent<HeroStateMachine>().hero;
        }
        if (gameObject.layer == 6)
        {
        chara = GetComponent<EnemyStateMachine>().enemy;
        }
        // Set the Character Matrices
        matriceATK = new float[,] 
        { {
                chara.defense.vuln_Slash,
                chara.defense.vuln_Bash,
                chara.defense.vuln_Pierce,
                chara.defense.vuln_water,
                chara.defense.vuln_fire,
                chara.defense.vuln_wind,
                chara.defense.vuln_earth,
                chara.defense.vuln_light,
                chara.defense.vuln_darkness
        } };
    }

    public float ComputeElement(string elementATK, string elementDEF)
    {
        // retourne valeur élément d'atk sur def en fonction de la matrice
        //return matriceATK[dicoATK[elementATK], dicoATK[elementDEF]];
        //Debug.Log(dicoATK[elementATK]);
        //Debug.Log(matriceATK[0, dicoATK[elementATK]]);
        return matriceATK[ 0, dicoATK[elementATK]];



        //dicoData.TryGetValue(word, out float value);
    }
    // + return if weak / resist / neutral
    
    // { CRITICAL, EFFECTIVE, UNEFFECTIVE, NORMAL, MISS, BREAK
}
  
