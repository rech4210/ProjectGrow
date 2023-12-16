using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item_Seed : ItemCtrl
{
    public override ItemKind itemKind => ItemKind.Seed;

    public float nowWeight;
    public float maxWeight;

    public WeaponKind WeaponKind;

    public override bool checkUse(ItemCtrl nowItem)
    {
        //화분에 입력 들어오는건 씨앗, 무기일경우 타워화분임

        return false;
    }

    public override void GrabToggle(RootCtrl rootCtrl, bool isGrab)
    {

    }

    public override void UseCall(RootCtrl rootCtrl, UseState useState)
    {

        //주변에 Slot을 체크해서 가장 가까운 슬롯에 설치해줌
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
                //Todo 장착중인 아이템 해제함
                //rootCtrl.weaponCtrl.ItemRemove();
                //this.disable();//화분 설치할때 회수가 될필요 있나?
            }
        }

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
