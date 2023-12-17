using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAuto : MonoBehaviour, I_Pool
{

    public float delay = 1f;
    private float delayTemp;
    private void Update()
    {
        if (delay > 0f)
        {
            delayTemp -= Time.deltaTime;
            if (delayTemp <= 0f)
            {
                disable();
            }
        }
    }
    private void OnEnable()
    {
        delayTemp = delay;
    }

    protected Action<I_Pool> disableAction;
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
    public virtual void disable()
    {
        //풀링 회수 
        this.transform.SetParent(GameManager.poolParent);
        this.gameObject.SetActive(false);
        disableAction?.Invoke(this);
        oneDisableCall?.Invoke(this);
        oneDisableCall = delegate { };
    }
}
