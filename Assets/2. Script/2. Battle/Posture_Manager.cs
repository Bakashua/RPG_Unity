using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Posture_Manager : MonoBehaviour
{
    /*
     * 1 qd recoit des dgt et curr cd dans act alors on entre en break
     * 2 les break damage recus donne + ou - de curr cd


    */
    [SerializeField] Chara_BaseStats hero;
    [SerializeField] HeroStateMachine CharaH;
    [SerializeField] EnemyStateMachine CharaE;

    //[Header("EVENT")]
    //[SerializeField] GameEvent GainPP;


    [Header("TITLE")]

    [SerializeField] Status_Effect_SO BreakEffect;
    [SerializeField] float turnBreak = 1;
    [SerializeField] float postureCurr;
    [SerializeField] float postureMax;
    [Space(20)]

    [SerializeField] bool canBerserk = false;
    public enum PostureState
    { NEUTRAL, BERSERK, BREAK, }
    public PostureState postureState;

    [Header("Feedback")]
    private Tween animationTween;
    public MeshRenderer Curr_color;
    public Material Or_color;
    public GameObject Text_Break;
    public VFX_Spawner VFX;
    public GameObject VFX_BreakLoop;

    bool onlyPlayOnce = true;

    [Header("Slow Motion")]
    public float slowMotionTimeScale = 0.5f;
    public float transitionDuration = 0.5f;
    private float originalTimeScale;
    private bool isSlowMotion = false;


    private void Start()
    {
        originalTimeScale = Time.timeScale;
        Or_color = Curr_color.material;
        Initialisation();
    }

    public void Initialisation()
    // set value depending if can berserk or not
    {
        // Doesnt work ???
        if (gameObject.tag == "Hero")
        {
            hero = gameObject.GetComponent<HeroStateMachine>().hero;
            CharaH = gameObject.GetComponent<HeroStateMachine>();
        }
        if (gameObject.tag == "Enemy")
        {
            hero = gameObject.GetComponent<EnemyStateMachine>().enemy;
            CharaE = gameObject.GetComponent<EnemyStateMachine>();
        }
        //Debug.Log(gameObject);
        //

        //// old TRail version
        //postureCurr = hero.life_Stats.postureCurr;
        //postureMax = hero.life_Stats.postureMax;
        //if (canBerserk) { postureCurr = 0; } else { postureCurr = postureMax; }

        // new version
        postureCurr = 1;

    }



    // UPDATE : ici on va switch les breaks damage vont etre les dgt infliger au current CD de la target
    // le break est gere hors dommage recu
    public float TakeHit(float Damage, float DamageTime)
    {
        // NEW FUNCTION W CD
        ManageCD(DamageTime, false);

        // deal damage to berserk state
        if (postureState == PostureState.BERSERK)
        {
            postureCurr -= Damage;
            if (0 <= postureCurr)
            {
                EnterBreak();
            }
        }
        // if can berserk it will turn berserk when posture reach max limit
        else if (canBerserk == true)
        {
            postureCurr += Damage;
            if (postureMax <= postureCurr)
            {
                EnterBerserk();
            }
        }
        // if take too much posture damage enter break state

        // normalement en else if mais le berserk a ete cut
        if (postureState != PostureState.BREAK)
        {
            postureCurr -= Damage;
            if (postureCurr <= 0)
            {
                EnterBreak();
                //Debug.Log(hero.name + "_ is in break");
            }
        }

        return postureCurr;
    }

    void ManageCD(float Damage, bool isbreak)
    {
        if (gameObject.tag == "Hero")
        {
            CharaH.curentCooldown -= Damage / 10;
        }
        if (gameObject.tag == "Enemy")
        {
            CharaE.curentCooldown -= Damage / 10;
        }

        if (isbreak)
        {
            if (gameObject.tag == "Hero")
            {
                CharaH.curentCooldown = 0.01f;
            }
            if (gameObject.tag == "Enemy")
            {
                CharaE.curentCooldown = 0.01f;
            }
        }

    }


    public void EnterBreak()
    {
        SpawnVFX();
        ManageCD(0, true);
        BreakStatEffect();
        //GainPP.TriggerEvent();
        postureState = PostureState.BREAK; 
        if (tag == "Enemy")
        {
            VFX_BreakLoop.SetActive(true);
        }
    }

    public void EnterBerserk()
    {
        postureState = PostureState.BERSERK;
    }

    public void EnterNeutral()
    {
        postureState = PostureState.NEUTRAL;
        PlayAnimFeedback(false); 
        RemoveBreakStatus();
        if (onlyPlayOnce == true) { }
        else
        {
            Initialisation();
        }
        onlyPlayOnce = false;

        if (tag == "Enemy")
        {
            VFX_BreakLoop.SetActive(false);
        }
    }

    void BreakStatEffect()
    {
        //Status_Effect_SO effect2 = ScriptableObject.Instantiate(BreakEffect);
        //effect2.target = hero;
        //effect2.caster = hero;
        //GetComponent<Status_Effect_Manager>().ActivateTraitFirstTime(effect2);

        hero.Battle_Stats.currentAtk = hero.Battle_Stats.currentAtk * 0.5f;
        hero.Battle_Stats.currentDef = hero.Battle_Stats.currentDef * 0.5f;
        hero.Battle_Stats.currentMatk = hero.Battle_Stats.currentMatk * 0.5f;
        hero.Battle_Stats.currentMdef = hero.Battle_Stats.currentMdef * 0.5f;
        hero.Battle_Stats.currentAcc = hero.Battle_Stats.currentAcc * 0.5f;
        hero.Battle_Stats.currentLuck = hero.Battle_Stats.currentLuck * 0.5f;
        hero.Battle_Stats.currentSpeed = hero.Battle_Stats.currentSpeed * 0.5f;
        hero.Battle_Stats.currentEva = hero.Battle_Stats.currentEva * 0.5f;
        hero.Battle_Stats.currentCritEva = hero.Battle_Stats.currentCritEva - 1000f;
        hero.Battle_Stats.currentCritMult = hero.Battle_Stats.currentCritMult * 0.5f;
        hero.Battle_Stats.currentCritRate = hero.Battle_Stats.currentCritRate * 0.5f;
    }

    void RemoveBreakStatus()
    {
        //// check si contain effect then delete effect 
        //Status_Effect_SO effect2 = ScriptableObject.Instantiate(BreakEffect);
        //Status_Effect_Manager SEM = GetComponent<Status_Effect_Manager>();

        //foreach (Status_Effect_SO item in SEM.StatusEffectOnCharacter)
        //{
        //    if (SEM.StatusEffectOnCharacter.Contains(item))
        //    {
        //        item.numberTurnLeft = 0;                
        //        SEM.CheckTrait(item);
        //        SEM.StatusEffectOnCharacter.Remove(item);
        //    }
        //}

        hero.Battle_Stats.currentAtk = hero.Battle_Stats.currentAtk * 2f;
        hero.Battle_Stats.currentDef = hero.Battle_Stats.currentDef * 2f;
        hero.Battle_Stats.currentMatk = hero.Battle_Stats.currentMatk * 2f;
        hero.Battle_Stats.currentMdef = hero.Battle_Stats.currentMdef * 2f;
        hero.Battle_Stats.currentAcc = hero.Battle_Stats.currentAcc * 2f;
        hero.Battle_Stats.currentLuck = hero.Battle_Stats.currentLuck * 2f;
        hero.Battle_Stats.currentSpeed = hero.Battle_Stats.currentSpeed * 2f;
        hero.Battle_Stats.currentEva = hero.Battle_Stats.currentEva * 2f;
        hero.Battle_Stats.currentCritEva = hero.Battle_Stats.currentCritEva * 2f;
        hero.Battle_Stats.currentCritMult = hero.Battle_Stats.currentCritMult * 2f;
        hero.Battle_Stats.currentCritRate = hero.Battle_Stats.currentCritRate * 2f;
    }



    #region FeedBack

    void SpawnVFX()
    {
        Vector3 posText = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2, gameObject.transform.position.z);
        Instantiate(Text_Break, posText, gameObject.transform.rotation);

        PlayAnimFeedback(true);

        //vfx     
        VFX.Target = gameObject.transform;
        VFX.SpawnFX();
    }

    void PlayAnimFeedback(bool play)
    {
        Color uneffective = new Color(0.5f, 0.5f, 0.5f, 1f);
        //Tween t1 = Curr_color.material.DOColor(uneffective, 1).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);


        if (play)
        {
            // Play slow mo on Break
            ToggleSlowMotion(true);


            Curr_color.material.color = uneffective;
            // Play the animation
            if (animationTween == null)
            {
                // Start a new DoTween animation
                animationTween = Curr_color.material.DOColor(uneffective, 1).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
            }
            else if (!animationTween.IsActive())
            {
                // Resume the existing DoTween animation
                animationTween.Play();
            }
        }
        else
        {
            Curr_color.material.color = Or_color.color;
            // Stop the animation
            if (animationTween != null && animationTween.IsActive())
            {

                animationTween.Pause();
                animationTween.Complete();
            }
        }
    }

    void StopSlowMo()
    {
        ToggleSlowMotion(false);
    }

    public void ToggleSlowMotion(bool isSlowMotion)
    {
            Invoke("StopSlowMo", 0.15f);
        if (isSlowMotion)
        {
            // Enable slow motion
            Time.timeScale = slowMotionTimeScale;
            Time.fixedDeltaTime = slowMotionTimeScale * 0.02f; // Adjust fixed delta time for physics
        }
        else
        {
            // Disable slow motion
            Time.timeScale = originalTimeScale;
            Time.fixedDeltaTime = originalTimeScale * 0.02f; // Reset fixed delta time
        }
    }

    #endregion

    /*
     * 
     * OLD VERSOPM CMME TRAIL
     * 
     *     public float TakeHit(float Damage)
    {
        // deal damage to berserk state
        if (postureState == PostureState.BERSERK)
        {
            postureCurr -= Damage;
            if (0 <= postureCurr)
            {
                EnterBreak();
            }
        }
        // if can berserk it will turn berserk when posture reach max limit
        else if (canBerserk == true)
        {
            postureCurr += Damage;
            if (postureMax <= postureCurr)
            {
                EnterBerserk();
            }
        }
        // if take too much posture damage enter break state
        else if (postureState != PostureState.BREAK)
        {
            postureCurr -= Damage;
            if (postureCurr <= 0)
            {
                EnterBreak();
                Debug.Log(hero.name + "_ is in break");
            }
        }

        hero.life_Stats.postureCurr = postureCurr;
        hero.life_Stats.postureMax = postureMax;
        return postureCurr;
    }
     * 
     * 
     */

}
