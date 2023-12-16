using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Weapon : ItemCtrl
{

    private Transform fireTran;

    public override bool checkUse(ItemCtrl nowItem)
    {//무기는 화분에 사용할경우? 놉 내려놓을때 근처에 타워식물이 있으면 자동으로 장착, 줍기로 회수 

        return true;
    }

    public override void GrabToggle(RootCtrl rootCtrl, bool isGrab)
    {
        if (isGrab==false)
        {
            //내려놯을때 주변에 타워있는지 체크해서 쥐어주기 
        }
    }

    public override void UseCall(RootCtrl rootCtrl, UseState useState)
    {
        //공격 모션
        //Vector2 dic = rootCtrl.WeaponCtrl.
    }
}
