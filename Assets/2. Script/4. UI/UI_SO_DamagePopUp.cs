using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DamagePopUp", menuName = "My Game/UI/ New DamagePopUp")]
public class UI_SO_DamagePopUp : ScriptableObject
{
    public enum AtkEffect
    { CRITICAL, EFFECTIVE, UNEFFECTIVE, NORMAL, MISS, BREAK }
    public AtkEffect currentState;

    public float Damage;
    public Transform TargetPos;
    public GameObject TargetGO;
}
