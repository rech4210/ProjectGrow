using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitZone : MonoBehaviour, I_HitZone
{
    public RootCtrl rootCtrl;
    public RootCtrl RootCtrl => rootCtrl;
    public Faction Faction => rootCtrl.faction;
    public Collider2D hitBox;
    private void Start()
    {
        rootCtrl = GetComponentInParent<RootCtrl>();
        hitBox = GetComponent<Collider2D>();
        RootCtrl.lifeAction += () => { hitBox.enabled = true; };
        RootCtrl.deadAction += () => { hitBox.enabled = false; };
    }


    public void SetDamaged(float damage, RootCtrl attacker)
    {
        rootCtrl.hpCtrl.SetDamaged(damage, attacker);
    }

}
