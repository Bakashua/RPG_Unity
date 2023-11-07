using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[CreateAssetMenu(fileName = "Core_", menuName = "My Game/Skills/New Core")]
public class SO_Core : ScriptableObject
{
    [SerializeField] string Name;
    [SerializeField] Sprite Icon;
    public int Level;
    public int XpRequired = 100;
    public int Xpcurrent;
    [SerializeField] List<CoreEffect> CoreEffect = new();
    public List<Status_Effect_SO> EmblemEffects = new();
    [SerializeField] List<BaseAttack> SpellGiven = new();

    public void ApplyCoreEffect()
    {
        foreach (CoreEffect item in CoreEffect)
        {
            if (item.Isactive)
            {
                item.Effect.ApplyEffect();
            }
        }

        foreach(Status_Effect_SO item in EmblemEffects)
        {
            item.ApplyEffect();
        }

    }

    public void AddEmblemEffect(Status_Effect_SO eft)
    {
        EmblemEffects.Add(eft);
    }


    public void GainXp(int xpReceived)
    {
        Xpcurrent += xpReceived;

        if (Xpcurrent >= XpRequired)
        {
            Level += 1;
            Xpcurrent = Xpcurrent - XpRequired;
            XpRequired = XpRequired * 2 * Level;
        }

    }


}

[System.Serializable]
public class CoreEffect
{
    public Status_Effect_SO Effect;
    public bool Isactive;
    [SerializeField] Sprite EffectIcon;
    public string EffectDescription;
    [TextArea]
    public string EffectDetails;
}
