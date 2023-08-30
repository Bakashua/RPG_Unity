using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Character_Spell_List", menuName = "My Game/Character/ New Spell_List")]
public class Chara_SpellList : ScriptableObject
{
    [Header("ID")]
    [SerializeField] string id;
    public string ID { get { return id; } }

    [Header("Neutral")]
    public List<float> passive = new List<float>();
    //public BaseAttack attack;
    [HideInInspector] public BaseAttack attack;
    public BaseAttack O_attack;
    [HideInInspector] public BaseAttack defense;
    public BaseAttack O_defense;
    public float move;
    public float item;


    [Header("Special")]
    public List<BaseAttack> Set01 = new List<BaseAttack>();
    public List<BaseAttack> O_Set01 = new List<BaseAttack>();
    public List<BaseAttack> Set02 = new List<BaseAttack>();
    public List<BaseAttack> O_Set02 = new List<BaseAttack>();

    [Header("Ultimate")]
    [HideInInspector] public BaseAttack ultimate;
    public BaseAttack O_ultimate;
    [HideInInspector] public BaseAttack limitBreak;
    public BaseAttack O_limitBreak;
    [HideInInspector] public BaseAttack limitBreak2;
    public BaseAttack O_limitBreak2;
    public float changechara;
    public float giveturn;

    protected virtual void OnValidate()
    {
        //string path = AssetDatabase.GetAssetPath(this);
        //id = AssetDatabase.AssetPathToGUID(path);
    }

    public void initialisation()
    {
        attack = ScriptableObject.Instantiate(O_attack);
        //attack.ResetTargetTiles();
        defense = ScriptableObject.Instantiate(O_defense);

        if (O_Set01[0] != null)
        { Set01[0] = ScriptableObject.Instantiate(O_Set01[0]); }
        if (O_Set01[1] != null)
        { Set01[1] = ScriptableObject.Instantiate(O_Set01[1]); }
        if (O_Set01[2] != null)
        { Set01[2] = ScriptableObject.Instantiate(O_Set01[2]); }
        if (O_Set01[3] != null)
        { Set01[3] = ScriptableObject.Instantiate(O_Set01[3]); }

        if (O_Set02[0] != null)
        { Set02[0] = ScriptableObject.Instantiate(O_Set02[0]); }
        if (O_Set02[1] != null)
        { Set02[1] = ScriptableObject.Instantiate(O_Set02[1]); }
        if (O_Set02[2] != null)
        { Set02[2] = ScriptableObject.Instantiate(O_Set02[2]); }
        if (O_Set02[3] != null)
        { Set02[3] = ScriptableObject.Instantiate(O_Set02[3]); }

        if (O_ultimate != null)
        { ultimate = ScriptableObject.Instantiate(O_ultimate); }
        if (O_limitBreak != null)
        { limitBreak = ScriptableObject.Instantiate(O_limitBreak); }
        if (O_limitBreak2 != null)
        { limitBreak2 = ScriptableObject.Instantiate(O_limitBreak2); }
    }

}
