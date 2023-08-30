using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

//..http://dotween.demigiant.com/documentation.php
public class UI_Tweener : MonoBehaviour
{
    [Header("....")]
    public bool showOnEnable;
    public bool showOnDisable;

    public enum UIAnimation
    {        Move, Rotate, Scale, Fade, Color, Jump, Punch, Shake, Follow, Path  }
    public GameObject objectToAnimate;

    [Header("Basic")]
    public UIAnimation animationType;
    public Ease easetype;
    public LoopType loopType;
    public float duration;
    public float delay;
    public int numberOfLoop;

    [Header("Movement")]
    public Vector3 to;

    [Header("Bool")]
    public bool from;

    [Header("Color")]
    public bool grandientColor;
    public bool doFill;
    public Color color;
    public Gradient gradColor;
    public float fillValue;

    [Header("Jump")]
    public float jumpPower;
    public int numJumps;  
    
    [Header("Punch")]
    public int vibration;
    public float elasticity;
    public bool position;
    public bool rotation;
    public bool scale;

    [Header("Shake")]
    public float strength;
    public int vibrato;
    public float randomness;
    public bool fadeOut;
    public bool position_Shake;
    public bool rotation_Shake;
    public bool scale_Shake;

    [Header("Fade")]
    [Tooltip("Only work with Image; Between 0 and 1")]
    public float fadeRate;

    
    [Header("Path")]
    public PathType pathType = PathType.CatmullRom;
    public Vector3[] waypoints;// = new List<Vector3>();
    //public Vector3[] waypoints = new[] {};

    [Header("Follow")]
    public Transform target; // Target to follow
    Vector3 targetLastPos;
    Tweener tween;


    public void OnEnable()
    {

       // objectToAnimate.transform.DO;
        if (showOnEnable) { Show();   }
            
    }

    public void Show()
    {        HandleTween();    }

    public void HandleTween()
    {
        if(objectToAnimate == null) { objectToAnimate = gameObject; }
        
        switch(animationType)
        {

            case UIAnimation.Color: ChangeColor(); break;
            case UIAnimation.Fade: Fade(); break;
            case UIAnimation.Follow: FollowTarget(); break;
            case UIAnimation.Jump: DoJump(); break;
            case UIAnimation.Move: DOMove(); break;
            case UIAnimation.Path: Path(); break;
            case UIAnimation.Punch: DoPunch(); break;
            case UIAnimation.Rotate: DORotate(); break;
            case UIAnimation.Scale: DoScale(); break;
            case UIAnimation.Shake: DoShake(); break;
        }
    }

    public void Fade()
    {
     objectToAnimate.GetComponent<Image>().DOFade(fadeRate, duration);
     objectToAnimate.GetComponent<Material>().DOFade(fadeRate, duration);
    }

    public void DOMove()
    {
        if (from)
        {
            objectToAnimate.transform.DOMove((objectToAnimate.transform.position + to), duration, false).SetDelay(delay).SetEase(easetype).SetLoops(numberOfLoop, loopType).From();
            //objectToAnimate.transform.DOLocalMove(to, duration, false).SetDelay(delay).SetEase(easetype).SetLoops(numberOfLoop, loopType).From();
        }
        else 
        { 
        objectToAnimate.transform.DOMove((objectToAnimate.transform.position + to), duration).SetDelay(delay).SetEase(easetype).SetLoops(numberOfLoop, loopType);
        }
    }

    public void DORotate()
    {
        if (from)
        {
            objectToAnimate.transform.DOLocalRotate(to, duration).SetDelay(delay).SetEase(easetype).SetLoops(numberOfLoop, loopType).From();
        }
        else
        {
            objectToAnimate.transform.DOLocalRotate(to, duration).SetDelay(delay).SetEase(easetype).SetLoops(numberOfLoop, loopType);
        }
    }

    public void DoJump()
    {
        if (from)
        {
            objectToAnimate.transform.DOLocalJump(to, jumpPower, numJumps, duration).SetDelay(delay).SetEase(easetype).SetLoops(numberOfLoop, loopType).PlayBackwards();
        }
        else
        {
            objectToAnimate.transform.DOLocalRotate(to, duration).SetDelay(delay).SetEase(easetype).SetLoops(numberOfLoop, loopType);
        }
    }

    public void DoScale()
    {
        if (from)
        {
            objectToAnimate.transform.DOScale(to, duration).SetDelay(delay).SetEase(easetype).SetLoops(numberOfLoop, loopType).From();
        }
        else
        {
            objectToAnimate.transform.DOScale(to, duration).SetDelay(delay).SetEase(easetype).SetLoops(numberOfLoop, loopType);
        }
    }

    public void DoPunch()
    {
        if (from)
        {
            if(position)
            {
            objectToAnimate.transform.DOPunchPosition(to, duration, vibration, elasticity).SetDelay(delay).SetEase(easetype).SetLoops(numberOfLoop, loopType).From();
            }
            if(rotation)
            {
            objectToAnimate.transform.DOPunchRotation(to, duration, vibration, elasticity).SetDelay(delay).SetEase(easetype).SetLoops(numberOfLoop, loopType).From();
            }
            if(scale)
            {
            objectToAnimate.transform.DOPunchScale(to, duration, vibration, elasticity).SetDelay(delay).SetEase(easetype).SetLoops(numberOfLoop, loopType).From();
            }
        }
        else
        {
            if (position)
            {
                objectToAnimate.transform.DOPunchPosition(to, duration, vibration, elasticity).SetDelay(delay).SetEase(easetype).SetLoops(numberOfLoop, loopType);
            }
            if (rotation)
            {
                objectToAnimate.transform.DOPunchRotation(to, duration, vibration, elasticity).SetDelay(delay).SetEase(easetype).SetLoops(numberOfLoop, loopType);
            }
            if (scale)
            {
                objectToAnimate.transform.DOPunchScale(to, duration, vibration, elasticity).SetDelay(delay).SetEase(easetype).SetLoops(numberOfLoop, loopType);
            }
        }
    }

    public void DoShake()
    {
        if (from)
        {
            if (position)
            {
                objectToAnimate.transform.DOShakePosition(duration, strength, vibrato, randomness, false, fadeOut).SetDelay(delay).SetEase(easetype).SetLoops(numberOfLoop, loopType).From();
            }
            if (rotation)
            {
                objectToAnimate.transform.DOShakeRotation(duration, strength, vibrato, randomness, fadeOut).SetDelay(delay).SetEase(easetype).SetLoops(numberOfLoop, loopType).From();
            }
            if (scale)
            {
                objectToAnimate.transform.DOShakeScale(duration, strength, vibrato, randomness, fadeOut).SetDelay(delay).SetEase(easetype).SetLoops(numberOfLoop, loopType).From();
            }
        }
        else
        {
            if (position)
            {
                objectToAnimate.transform.DOShakePosition(duration, strength, vibrato, randomness, false, fadeOut).SetDelay(delay).SetEase(easetype).SetLoops(numberOfLoop, loopType);
            }
            if (rotation)
            {
                objectToAnimate.transform.DOShakeRotation(duration, strength, vibrato, randomness, fadeOut).SetDelay(delay).SetEase(easetype).SetLoops(numberOfLoop, loopType);
            }
            if (scale)
            {
                objectToAnimate.transform.DOShakeScale(duration, strength, vibrato, randomness, fadeOut).SetDelay(delay).SetEase(easetype).SetLoops(numberOfLoop, loopType);
            }
        }
    }

    public void ChangeColor()
    {

        if (grandientColor)
        {
           // objectToAnimate.GetComponent<Renderer>().material.DOColor(color, duration).SetDelay(delay).SetEase(easetype).SetLoops(numberOfLoop, loopType); 
            objectToAnimate.GetComponent<Image>().material.DOColor(color, duration).SetDelay(delay).SetEase(easetype).SetLoops(numberOfLoop, loopType); 
        }
        else
        {
            //objectToAnimate.GetComponent<Renderer>().material.DOGradientColor(gradColor, duration).SetDelay(delay).SetEase(easetype).SetLoops(numberOfLoop, loopType);
            objectToAnimate.GetComponent<Image>().material.DOColor(color, duration).SetDelay(delay).SetEase(easetype).SetLoops(numberOfLoop, loopType); 
        }
        if (doFill && from)
        {
            objectToAnimate.GetComponent<Image>().DOFillAmount(fillValue, duration).SetDelay(delay).SetEase(easetype).SetLoops(numberOfLoop, loopType).From();
        }
        else { objectToAnimate.GetComponent<Image>().DOFillAmount(fillValue, duration).SetDelay(delay).SetEase(easetype).SetLoops(numberOfLoop, loopType); }
    }        

    public void FollowTarget()
    {
        tween = transform.DOMove(target.position, duration).SetAutoKill(false);
        targetLastPos = target.position;
        // Use an Update routine to change the tween's endValue each frame
        // so that it updates to the target's position if that changed
        if (targetLastPos == target.position) return;
        // Add a Restart in the end, so that if the tween was completed it will play again
        tween.ChangeEndValue(target.position, true).Restart();
        targetLastPos = target.position;
    }

    public void Path()
    {
        // Create a path tween using the given pathType, Linear or CatmullRom (curved).
        // Use SetOptions to close the path
        // and SetLookAt to make the target orient to the path itself
        // Tween t = objectToAnimate.transform.DOPath(waypoints, 4, pathType)
        Tween t = objectToAnimate.transform.DOPath(waypoints, duration, pathType)
        .SetOptions(true);
        //.SetLookAt(0.001f);
    }

    public void Fill()
    {

    }

}
