using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Pot : ItemCtrl
{

    public override ItemKind itemKind => ItemKind.Seed;

    public Item_Seed nowSeed;
    public float waterValue;
    public bool isWood;//나무가 됐돠


    public override bool checkUse(ItemCtrl nowItem)
    {
        //화분에 입력 들어오는건 씨앗, 무기일경우 타워화분임
        switch (nowItem.itemKind)
        {
            case ItemKind.None:
                break;
            case ItemKind.Slot:
                //밭에 화분 배치
                PotSlot slot = nowItem as PotSlot;
                if (slot != null && slot.nowItemCtrl == null)
                {
                    FieldCtrl.Instance.setSlot(this, slot);
                    return true;
                }
                break;
            case ItemKind.Seed:
                Item_Seed seed = nowItem as Item_Seed;
                if (seed != null && nowSeed == null)
                {
                    isWood = false;
                    nowSeed = seed;
                    return true;
                }
                break;
        }
        return false;
    }

    public override void GrabToggle(RootCtrl rootCtrl, bool isGrab)
    {

    }

    public override void UseCall(RootCtrl rootCtrl, UseState useState)
    {

        //주변에 Slot을 체크해서 가장 가까운 슬롯에 설치해줌

        switch (useState)
        {
            case UseState.None:
                break;
            case UseState.Start:

                if (nowSeed == null)
                {
                    if (isWood)
                    {
                        //열매 생성
                        //씨앗 회수
                        nowSeed.disable();
                        nowSeed = null;
                    }
                }
                else
                {//씨앗 없음, 설치 안됨
                    ItemCtrl hitCtrl = InteractionCtrl.selectItemCtrl(rootCtrl.transform, checkUse);
                    if (hitCtrl != null)
                    {
                        //Todo 장착중인 아이템 해제함
                        //rootCtrl.weaponCtrl.ItemRemove();
                    }
                }
                break;
            case UseState.Ing:
                break;
            case UseState.End:
                break;
        }
    }

    public void Update()
    {
        if (nowSeed != null)
        {
            if (waterValue > 0f)
            {
                waterValue -= Time.deltaTime;
                nowSeed.addWeight(Time.deltaTime, this);
            }
        }
    }

    public void woodOn()
    {
        isWood = true;
        //씨앗의 종류 별로 나무 이미지 변경
        switch (nowSeed.WeaponKind)
        {
            case WeaponKind.None:
                break;
            case WeaponKind.Revolver:
                break;
            case WeaponKind.Minigun:
                break;
            case WeaponKind.Firebat:
                break;
            case WeaponKind.Electric:
                break;
        }

    }



}
