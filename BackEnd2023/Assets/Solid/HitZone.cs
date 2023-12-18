using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitZone : MonoBehaviour, I_HitZone
{
    public RootCtrl rootCtrl;
    public RootCtrl RootCtrl => rootCtrl;
    public Faction Faction
    {
        get
        {
            if (rootCtrl == null)
            {
                rootCtrl = GetComponentInParent<RootCtrl>();
                hitBox = GetComponent<Collider2D>();
            }
            return rootCtrl.faction;
        }
    }
    public Collider2D hitBox;
    private void Start()
    {
        rootCtrl = GetComponentInParent<RootCtrl>();
        hitBox = GetComponent<Collider2D>();

        RootCtrl.lifeAction += () =>
        {
            hitBox.enabled = true;
            if (RootCtrl.Faction != Faction.Enemy)
            {
                GameManager.instance.AddtoTransformlist(rootCtrl);
            }
        };
        RootCtrl.deadAction += () =>
        {
            hitBox.enabled = false;
            if (RootCtrl.Faction != Faction.Enemy)
            {
                GameManager.instance.DeleteTransformlist(rootCtrl);
            }
        };
    }


    public void SetDamaged(float damage, I_Attacker attacker)
    {
        if (rootCtrl.stateCtrl.stateEnum != stateEnum.Dead && rootCtrl.stateCtrl.stateEnum != stateEnum.Stunned)
        {
            rootCtrl.hpCtrl.SetDamaged(damage, attacker);
        }
    }

    public bool CheckHitLock(I_Attacker attacker)
    {
        switch (RootCtrl.faction)
        {
            case Faction.None:
                break;
            case Faction.Player:
                if (attacker.Faction == Faction.Enemy)
                {
                    return false;
                }
                break;
            case Faction.Pot:
                break;
            case Faction.Enemy:
                if (attacker.Faction != Faction.Enemy)
                {
                    return false;
                }
                break;
        }
        return true;

    }

}
