using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Wildboar : MonoBehaviour, I_Pool
{
    private Action<I_Pool> disableAction;

    public void SetPoolEvent(Action<I_Pool> poolevent)
    {
        disableAction += poolevent;
    }
    public Action<I_Pool> oneDisableCall;
    public void SetDisableOneEvent(Action<I_Pool> disableEvent)
    {
        oneDisableCall -= disableEvent;
        oneDisableCall += disableEvent;
    }
    public void disable()
    {
        disableAction?.Invoke(this);
        oneDisableCall?.Invoke(this);
        oneDisableCall = delegate { };
    }
}
