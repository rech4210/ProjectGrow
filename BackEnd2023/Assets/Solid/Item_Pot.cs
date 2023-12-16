using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Pot : ItemCtrl
{

    public override ItemKind itemKind => ItemKind.Pot;

    public override bool IsGrabLock => base.IsGrabLock && (nowSeed.seedKind == SeedKind.Tower && weapon != null) == false;

    public Item_Seed nowSeed;
    public float waterValue;
    public bool isWood;//나무가 됐돠


    public Item_Weapon weapon;//타워일경우 들고있는 무기
    public int weaponUseCount;//무기 사용횟수


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
                    return true;
                }
                break;
            case ItemKind.Seed: //이부분은 seed에 옮겨야함
                Item_Seed seed = nowItem as Item_Seed;
                if (seed != null && nowSeed == null)
                {
                    //화분에 리소스 설정
                    switch (nowSeed.seedKind)
                    {
                        case SeedKind.None:
                        case SeedKind.Revolver:
                        case SeedKind.Minigun:
                        case SeedKind.Firebat:
                        case SeedKind.Electric:
                            return true;
                    }
                }
                break;
        }
        return false;
    }


    public void useAction(ItemCtrl nowItem)
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
                    return;
                }
                break;
            case ItemKind.Seed:
                break;
        }

    }
    public void setWeapon(Item_Weapon weapon)
    {
        if (nowSeed.seedKind == SeedKind.Tower)
        {
            this.weapon = weapon;
            switch (weapon.weaponKind)
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
            }
        }
    }
    public void setSeed(Item_Seed seed)
    {
        if (nowSeed == null)
        {
            isWood = false;
            nowSeed = seed;
            nowSeed.transform.SetParent(this.transform);
            nowSeed.transform.localPosition = Vector3.zero;
            nowSeed.gameObject.SetActive(false);//일단 꺼두자
                                                //화분에 리소스 설정
            switch (nowSeed.seedKind)
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
            }
        }
    }
    public override ItemCtrl GrabToggle(RootCtrl rootCtrl, bool isGrab)
    {
        if (nowSeed.seedKind == SeedKind.Tower)
        {
            ItemCtrl temp = weapon;
            weapon = null;
            return temp;
        }
        else
        {
            return this;
        }
    }

    public override void UseCall(RootCtrl rootCtrl, UseState useState)
    {

        //주변에 Slot을 체크해서 가장 가까운 슬롯에 설치해줌

        switch (useState)
        {
            case UseState.None:
                break;
            case UseState.Start:

                if (nowSeed != null)
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
                    ItemCtrl hitCtrl = InteractionCtrl.getSelectItemCtrl(rootCtrl.transform, checkUse);
                    if (hitCtrl != null)
                    {
                        //Todo 장착중인 아이템 해제함
                        //rootCtrl.weaponCtrl.ItemRemove();
                        useAction(hitCtrl);
                        rootCtrl.interaction.interactionGrabOff();
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
            if (waterValue > 0f && isWood == false)
            {
                waterValue -= Time.deltaTime;
                nowSeed.addWeight(Time.deltaTime, this);
                //게이지 표시
                //nowSeed.nowWeight / nowSeed.maxWeight;//현재 성장율
                //(nowSeed.nowWeight+waterValue) / nowSeed.maxWeight;//예상 성장률
            }
        }
    }

    public void woodOn()
    {
        isWood = true;
        //씨앗의 종류 별로 나무 이미지 변경
        switch (nowSeed.seedKind)
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
            case SeedKind.Tower:
                //Todo PlantInfo로 변경해야함
                weaponUseCount = ScriptableManager.instance.getTable("PlantScriptable").getPrefab<ScriptablePlantInfo.PrefabInfo>(nowSeed.seedKind.ToString()).Reusecount;
                break;
        }

    }



}
