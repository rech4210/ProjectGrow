using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item_Seed : ItemCtrl
{
    public override ItemKind itemKind => ItemKind.Seed;

    public float nowWeight;
    public float maxWeight => seedInfo.GrowTime;
    public float weightFill => nowWeight / maxWeight;

    public SeedKind seedKind;
    public ScriptablePlantInfo.PrefabInfo seedInfo;

    public Sprite[] seedSprite;
    public SpriteRenderer model;


    public float openTime = 1f;
    public float openTemp;

    private void OnEnable()
    {
        switch (seedKind)
        {
            case SeedKind.None:
                break;
            case SeedKind.Revolver:
                seedInfo = ScriptableManager.instance.getTable(ScriptableManager.PlantScriptableTag).getPrefab<ScriptablePlantInfo.PrefabInfo>(PlantNameEnum.Rovolver.ToString());
                model.sprite = seedSprite[0];
                break;
            case SeedKind.Minigun:
                seedInfo = ScriptableManager.instance.getTable(ScriptableManager.PlantScriptableTag).getPrefab<ScriptablePlantInfo.PrefabInfo>(PlantNameEnum.Minigun.ToString());
                model.sprite = seedSprite[1];
                break;
            case SeedKind.Firebat:
                seedInfo = ScriptableManager.instance.getTable(ScriptableManager.PlantScriptableTag).getPrefab<ScriptablePlantInfo.PrefabInfo>(PlantNameEnum.flame_thrower.ToString());
                model.sprite = seedSprite[2];
                break;
            case SeedKind.Electric:
                seedInfo = ScriptableManager.instance.getTable(ScriptableManager.PlantScriptableTag).getPrefab<ScriptablePlantInfo.PrefabInfo>(PlantNameEnum.Lighting.ToString());
                model.sprite = seedSprite[3];
                break;
            case SeedKind.Tower:
                seedInfo = ScriptableManager.instance.getTable(ScriptableManager.PlantScriptableTag).getPrefab<ScriptablePlantInfo.PrefabInfo>(PlantNameEnum.Dionaea.ToString());
                model.sprite = seedSprite[4];
                break;
            case SeedKind.Water:
                break;
            case SeedKind.Pot:
                model.sprite = seedSprite[4];
                break;
        }
    }
    public override void potOut()
    {
        OnEnable();
        base.potOut();
    }

    public override bool checkUse(ItemCtrl nowItem)
    {
        //화분에 입력 들어오는건 씨앗, 무기일경우 타워화분임
        switch (nowItem.itemKind)
        {
            case ItemKind.None:
                break;
            case ItemKind.Slot:
                if (seedKind == SeedKind.Pot)
                {
                    PotSlot slot = nowItem as PotSlot;
                    if (slot != null && slot.nowPot == null)
                    {
                        return true;
                    }
                }
                break;
            case ItemKind.Pot:
                Item_Pot pot = nowItem as Item_Pot;
                if (pot != null && pot.nowSeed == null)
                {
                    return true;
                }
                break;
        }
        return false;
    }
    public void useAction(ItemCtrl nowItem)
    {
        switch (nowItem.itemKind)
        {
            case ItemKind.None:
                break;
            case ItemKind.Slot:
                if (seedKind == SeedKind.Pot)
                {
                    PotSlot slot = nowItem as PotSlot;
                    if (slot != null && slot.nowPot == null)
                    {
                        Item_Pot newPot = ItemCtrl.newItem(ItemKind.Pot, "Pot") as Item_Pot;
                        FieldCtrl.Instance.setSlot(newPot, slot);
                        this.disable();
                    }
                }
                break;
            case ItemKind.Pot:
                if (seedKind != SeedKind.Pot)
                {
                    Item_Pot pot = nowItem as Item_Pot;
                    if (pot != null && pot.nowSeed == null)
                    {
                        pot.setSeed(this);
                    }
                }
                break;
        }
    }

    public override ItemCtrl GrabToggle(RootCtrl rootCtrl, bool isGrab)
    {
        this.isGrab = isGrab;
        return this;
    }

    public override void UseCall(RootCtrl rootCtrl, UseState useState)
    {

        switch (useState)
        {
            case UseState.None:
                break;
            case UseState.Start:
                openTemp = openTime;
                break;
            case UseState.Ing:
                openTemp -= Time.deltaTime;
                break;
            case UseState.End:
                Collider2D[] potList = Physics2D.OverlapCircleAll(rootCtrl.transform.position, 1f, LayerManager.Instance.ItemInterObj);

                if (potList != null)
                {
                    float minDis = 0f;
                    ItemCtrl hitCtrl = null;
                    for (int i = 0; i < potList.Length; i++)
                    {
                        ItemCtrl item = potList[i].GetComponent<ItemCtrl>();
                        if (item != null)
                        {
                            if (checkUse(item))
                            {
                                float dis = Vector2.Distance(rootCtrl.transform.position, item.transform.position);
                                if (dis < minDis || hitCtrl == null)
                                {
                                    hitCtrl = item;
                                    minDis = dis;
                                }
                            }
                        }
                    }
                    if (hitCtrl != null)
                    {
                        useAction(hitCtrl);
                        rootCtrl.interaction.interactionGrabOff();
                        //Todo 장착중인 아이템 해제함
                        //rootCtrl.weaponCtrl.ItemRemove();
                        //this.disable();//화분 설치할때 회수가 될필요 있나?
                    }
                }
                break;
        }
        //Debug.Log(openTemp);
        if (seedKind != SeedKind.Pot && openTemp <= 0f)
        {
            Item_Weapon weapon = ItemCtrl.newItem(ItemKind.Weapon, seedKind.ToString()) as Item_Weapon;
            weapon.weaponKind = seedKind;
            weapon.OnEnable();
            weapon.transform.position = this.transform.position;
            rootCtrl.interaction.interactionGrabOff();
            this.disable();
            return;
        }
        //주변에 Slot을 체크해서 가장 가까운 슬롯에 설치해줌


    }
    public void addWeight(float weight, Item_Pot pot)
    {
        nowWeight += weight;
        if (nowWeight >= maxWeight)
        {
            pot.woodOn();
        }
    }


}
