using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotHpCtrl : MonoBehaviour, I_HitZone
{
    public Item_Pot myPot;
    public Faction Faction => Faction.Pot;

    public float nowHp;
    public float maxHp = 40f;
    public Collider2D hitBox;

    private void Awake()
    {
        myPot = GetComponentInParent<Item_Pot>();
        if (hitBox == null)
        {
            hitBox = GetComponent<Collider2D>();
        }
    }

    public bool CheckHitLock(I_Attacker attacker)
    {
        if (attacker is RootCtrl)
        {
            RootCtrl rootCtrl = attacker as RootCtrl;
            if (rootCtrl != null && rootCtrl.interaction.GrabItemCtrl != null && rootCtrl.interaction.GrabItemCtrl.itemKind == ItemKind.Weapon)
            {
                switch ((rootCtrl.interaction.GrabItemCtrl as Item_Weapon).weaponKind)
                {
                    case SeedKind.None:
                    case SeedKind.Revolver:
                    case SeedKind.Minigun:
                    case SeedKind.Firebat:
                    case SeedKind.Electric:
                        return true;
                    case SeedKind.Water:
                        return myPot.isWood;
                }
            }
        }
        return false;
    }

    public void SetDamaged(float damage, I_Attacker attacker)
    {
        if (attacker is RootCtrl)
        {
            RootCtrl rootCtrl = attacker as RootCtrl;
            switch (attacker.Faction)
            {
                case Faction.None:
                    break;
                case Faction.Player:
                    if (rootCtrl.interaction.GrabItemCtrl != null && rootCtrl.interaction.GrabItemCtrl.itemKind == ItemKind.Weapon)
                    {
                        switch ((rootCtrl.interaction.GrabItemCtrl as Item_Weapon).weaponKind)
                        {
                            case SeedKind.None:
                            case SeedKind.Revolver:
                            case SeedKind.Minigun:
                            case SeedKind.Firebat:
                            case SeedKind.Electric:
                                Debug.Log("피격은 아닌데");
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
                        //파괴
                        attacker.DeadEvent(myPot);

                        if (myPot.nowSeed != null)
                        {
                            myPot.nowSeed.disable();
                            myPot.nowSeed = null;
                        }
                        if (myPot.weapon != null)
                        {
                            myPot.weapon.disable();
                            myPot.weapon = null;
                        }
                        hitBox.enabled = false;
                        GameManager.instance.DeleteTransformlist(myPot);
                    }
                    break;
            }
        }
    }

    public void lifeOn()
    {
        nowHp = 40;
        hitBox.enabled = true;
        GameManager.instance.AddtoTransformlist(myPot);
    }
}
