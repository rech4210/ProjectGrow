using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SeedKind
{
    None = 0,
    Revolver = 1,
    Minigun = 2,
    Firebat = 3,
    Electric = 4,
    Water = 5,
    Tower = 6,
    Pot = 7,
}

public class Item_Weapon : ItemCtrl
{


    public override ItemKind itemKind => ItemKind.Weapon;

    public SeedKind weaponKind;


    public ScriptableWeaponInfo.PrefabInfo weaponInfo;

    public float nowGuage;//남은 게이지
    public int nowAmmo;//남은 탄
    public float attackDelay;
    public bool isAttackLock;

    public FireCtrl fireCtrl;

    public SpriteRenderer model;
    public Sprite[] gunSprites;


    public void OnEnable()
    {
        weaponInfo = ScriptableManager.instance.getTable(ScriptableManager.WeaponScriptableTag).getPrefab<ScriptableWeaponInfo.PrefabInfo>(weaponKind.ToString());
        fireCtrl.setWeapon(this);

        switch (weaponKind)
        {
            case SeedKind.None:
                break;
            case SeedKind.Revolver:
                model.sprite = gunSprites[0];
                break;
            case SeedKind.Minigun:
                model.sprite = gunSprites[1];
                break;
            case SeedKind.Firebat:
                model.sprite = gunSprites[2];
                break;
            case SeedKind.Electric:
                model.sprite = gunSprites[3];
                break;
            case SeedKind.Water:
                break;
            case SeedKind.Tower:
                break;
            case SeedKind.Pot:
                break;
            default:
                break;
        }
        switch (weaponInfo.EnergyType)
        {
            case EnergyTypeEnum.Bullet:
                nowAmmo = weaponInfo.BulletCount;
                break;
            case EnergyTypeEnum.Gauge:
                nowGuage = weaponInfo.GaugeTime;
                break;
            case EnergyTypeEnum.Null:
                break;
            default:
                break;
        }
    }
    public void reload()
    {
        switch (weaponInfo.EnergyType)
        {
            case EnergyTypeEnum.Bullet:
                nowAmmo = weaponInfo.BulletCount;
                break;
            case EnergyTypeEnum.Gauge:
                nowGuage = weaponInfo.GaugeTime;
                break;
            case EnergyTypeEnum.Null:
                break;
            default:
                break;
        }
    }
    public override bool checkUse(ItemCtrl nowItem)
    {//무기는 화분에 사용할경우? 놉 내려놓을때 근처에 타워식물이 있으면 자동으로 장착, 줍기로 회수 
        switch (nowItem.itemKind)
        {
            case ItemKind.Pot:
                Item_Pot pot = nowItem as Item_Pot;
                if (pot != null)
                {
                    if (pot.nowSeed != null && pot.isWood && pot.nowSeed.seedKind == SeedKind.Tower)
                    {
                        return true;
                    }
                }
                break;
        }
        return false;
    }


    public override ItemCtrl GrabToggle(RootCtrl rootCtrl, bool isGrab)
    {
        this.isGrab = isGrab;
        if (isGrab == false)
        {
            switch (weaponKind)
            {
                case SeedKind.None:
                    break;
                case SeedKind.Water:
                    break;
                case SeedKind.Tower:
                    break;
                case SeedKind.Revolver:
                case SeedKind.Minigun:
                case SeedKind.Firebat:
                case SeedKind.Electric:
                    //내려놯을때 주변에 타워있는지 체크해서 쥐어주기 
                    ItemCtrl itemCtrl = InteractionCtrl.getSelectItemCtrl(this.transform, checkUse);
                    if (itemCtrl != null)
                    {
                        Item_Pot pot = itemCtrl as Item_Pot;
                        if (pot != null)
                        {
                            if (pot.nowSeed != null && pot.isWood && pot.nowSeed.seedKind == SeedKind.Tower)
                            {
                                pot.setWeapon(this);
                            }
                        }
                    }
                    break;
            }

        }
        return this;
    }
    private void Update()
    {
        if (attackDelay >= 0f)
        {
            attackDelay -= Time.deltaTime;
        }
    }
    public void UseCallAttack(I_Attacker attackCtrl, UseState useState, Action endAmmo)
    {
        switch (useState)
        {
            case UseState.None:
                break;
            case UseState.Start:
                switch (weaponKind)
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
                        break;
                    case SeedKind.Tower:
                        break;
                    case SeedKind.Pot:
                        break;
                    default:
                        break;
                }
                break;
            case UseState.Ing:
          
                break;
            case UseState.End:
                break;
        }
        if (attackDelay <= 0f)
        {
            attackDelay = weaponInfo.AttackSpeed;
            fireCtrl.Fire(attackCtrl);

            //공격 모션
            switch (weaponInfo.EnergyType)
            {
                case EnergyTypeEnum.Bullet:
                    --nowAmmo;
                    if (nowAmmo <= 0)
                    {
                        //모두 소모함
                        endAmmo?.Invoke();
                        //this.disable();
                    }
                    break;
                case EnergyTypeEnum.Gauge:
                    if (weaponKind == SeedKind.Water)
                    {//물은 무제한
                        return;
                    }
                    nowGuage -= Time.deltaTime;
                    if (nowGuage <= 0f)
                    {
                        //모두 소모함
                        endAmmo?.Invoke();
                        //this.disable();
                    }
                    break;
                case EnergyTypeEnum.Null:
                    break;
                default:
                    break;
            }

        }
    }
    public override void UseCall(RootCtrl rootCtrl, UseState useState)
    {
        switch (useState)
        {
            case UseState.None:
                break;
            case UseState.Start:
                switch (weaponKind)
                {
                    case SeedKind.None:
                        break;
                    case SeedKind.Revolver:
                        isAttackLock = true;
                        break;
                    case SeedKind.Minigun:
                        break;
                    case SeedKind.Firebat:
                        break;
                    case SeedKind.Electric:
                        break;
                    case SeedKind.Water:
                        break;
                    case SeedKind.Tower:
                        break;
                    case SeedKind.Pot:
                        break;
                    default:
                        break;
                }
                break;
            case UseState.Ing:
                if (isAttackLock)
                {
                    return;
                }
                break;
            case UseState.End:
                isAttackLock = false;
                break;
        }
        if (attackDelay <= 0f)
        {
            attackDelay = weaponInfo.AttackSpeed;
            fireCtrl.Fire(rootCtrl);

            //공격 모션
            switch (weaponInfo.EnergyType)
            {
                case EnergyTypeEnum.Bullet:
                    --nowAmmo;
                    if (nowAmmo <= 0)
                    {
                        //모두 소모함
                        rootCtrl.interaction.interactionGrabOff();
                        this.disable();
                    }
                    break;
                case EnergyTypeEnum.Gauge:
                    if (weaponKind == SeedKind.Water)
                    {//물은 무제한
                        return;
                    }
                    nowGuage -= Time.deltaTime;
                    if (nowGuage <= 0f)
                    {
                        //모두 소모함
                        rootCtrl.interaction.interactionGrabOff();
                        this.disable();
                    }
                    break;
                case EnergyTypeEnum.Null:
                    break;
                default:
                    break;
            }

        }
    }
}
