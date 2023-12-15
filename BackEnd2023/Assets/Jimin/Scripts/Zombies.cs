using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Zombies : MonoBehaviour, I_Pool
{
    private Action<I_Pool> disableAction;

    public void SetPoolEvent(Action<I_Pool> poolevent)
    {
        disableAction += poolevent;
    }

    public void disable()
    {
        disableAction?.Invoke(this);
    }
}
