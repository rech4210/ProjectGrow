using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotHpCtrl : MonoBehaviour, I_HitZone
{
    public Item_Pot myPot;

    public float nowHp;
    public float maxHp;

    private void Awake()
    {
        myPot = GetComponentInParent<Item_Pot>();
    }

    public bool CheckHitLock(RootCtrl attacker)
    {
        if (attacker.interaction.GrabItemCtrl.itemKind == ItemKind.Weapon)
        {
            switch ((attacker.interaction.GrabItemCtrl as Item_Weapon).weaponKind)
            {
                case SeedKind.None:
                    break;
                case SeedKind.Revolver:
                    break;
                case SeedKind.Minigun:
                    break;
                case SeedKind.Firebat:
                    break;
                case SeedKind.Electric:
                    break;
                case SeedKind.Water:
                    return true;
            }
        }
        return false;
    }

    public void SetDamaged(float damage, RootCtrl attacker)
    {
        switch (attacker.faction)
        {
            case Faction.None:
                break;
            case Faction.Player:
                if (attacker.interaction.GrabItemCtrl.itemKind == ItemKind.Weapon)
                {
                    switch ((attacker.interaction.GrabItemCtrl as Item_Weapon).weaponKind)
                    {
                        case SeedKind.None:
                            break;
                        case SeedKind.Revolver:
                            break;
                        case SeedKind.Minigun:
                            break;
                        case SeedKind.Firebat:
                            break;
                        case SeedKind.Electric:
                            break;
                        case SeedKind.Water:
                            myPot.waterValue = 5f;
                            break;
                    }
                }
                break;
            case Faction.Pot:
                break;
            case Faction.Enemy:
                nowHp -= damage;
                if (nowHp <= 0f)
                {
                    nowHp = 0f;
                    //ÆÄ±«
                    attacker.DeadEvent(myPot);
                }
                break;
        }

    }

    public void lifeOn()
    {

    }
}
