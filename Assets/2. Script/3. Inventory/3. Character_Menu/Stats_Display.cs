using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


// ici je voulais faire instantiation des popups de dgt mais j'ai décaller ça sur ke gui manager
// je me demande si il faudrai pas assayé de caller tout l'ui des monstres ici : barre de vie / text pv et de l'update ici
// le script state manager servant juste a envoyer des floats dans ces scripts pour que eux update l'interface

public class Stats_Display : MonoBehaviour
{
    //private BattleStateMachine BSM;

    //[Header("GameObject")]
    //public GameObject damagePopUp;
    //public Transform damagePopUpSpawn;


    //[Header("GameObject")]
    //public TextMeshProUGUI heroHP;
    //public Slider hpSlider;

    //void Start()
    //{
    //    BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();        
    }


    //void UpdateHP()
    //{
    //    //statsUI.heroHP.text = enemy.currentHP.ToString();
    //    //statsUI.hpSlider.maxValue = enemy.baseHP;
    //    //statsUI.hpSlider.value = enemy.currentHP;
    //}

// 1 pop up qui s'instantie quand on attaque la cible