using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitZone : MonoBehaviour, I_HitZone
{
    public RootCtrl rootCtrl;
    public RootCtrl RootCtrl => rootCtrl;

    private void Start()
    {
        rootCtrl = GetComponentInParent<RootCtrl>();

    }


    public void SetDamaged(float damage, RootCtrl attacker)
    {
        //rootCtrl.hpCtrl.SetDamaged(damage, attacker);
    }


}
