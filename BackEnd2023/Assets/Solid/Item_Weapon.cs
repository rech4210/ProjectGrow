using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponKind
{
    None = 0,
    Revolver = 1,
    Minigun = 2,
    Firebat = 3,
    Electric = 4,
    Water = 5,
}

public class Item_Weapon : ItemCtrl
{
    public static Dictionary<WeaponKind, ObjectPooling<BulletCtrl>> bulletPoolDic = new Dictionary<WeaponKind, ObjectPooling<BulletCtrl>>();
    public BulletCtrl bullet;

    public override ItemKind itemKind => ItemKind.Weapon;

    public WeaponKind weaponKind;
    private Transform fireTran;

    private void Start()
    {
        //bullet = ScriptableManager.instance.getTable("Bullet").getPrefab<WeaponInfo>(weaponKind.ToString()).GetComponent<BulletCtrl>();
    }
    public override bool checkUse(ItemCtrl nowItem)
    {//무기는 화분에 사용할경우? 놉 내려놓을때 근처에 타워식물이 있으면 자동으로 장착, 줍기로 회수 

        return true;
    }

    public override void GrabToggle(RootCtrl rootCtrl, bool isGrab)
    {
        if (isGrab == false)
        {
            //내려놯을때 주변에 타워있는지 체크해서 쥐어주기 
        }
    }

    public override void UseCall(RootCtrl rootCtrl, UseState useState)
    {
        //공격 모션
        Vector2 dic = (rootCtrl.WeaponCtrl.targetTran.position - fireTran.position);
        float dis = dic.magnitude;
        dic = dic.normalized;
        //탄황 생성, 해당 방향으로 발사
        BulletCtrl newBullet = null;
        if (bulletPoolDic.ContainsKey(weaponKind) == false)
        {
            bulletPoolDic[weaponKind] = new ObjectPooling<BulletCtrl>();
            bulletPoolDic[weaponKind].Initialize(bullet, null, 10);
        }
        newBullet = bulletPoolDic[weaponKind].GetObject(bullet);
        newBullet.dic = dic;
        newBullet.range = 100f;
        newBullet.speed = 1f;
        newBullet.gameObject.SetActive(true);
    }
}
