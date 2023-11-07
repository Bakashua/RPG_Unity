using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    public GameEvent Event;


    public void _TriggerEvent()
    {
        Event.TriggerEvent();
    }


}
