using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    private Item_Weapon itemWeapon;
    private Transform fireTran;

    public static Dictionary<SeedKind, ObjectPooling<BulletCtrl>> bulletPoolDic = new Dictionary<SeedKind, ObjectPooling<BulletCtrl>>();
    public BulletCtrl bullet;


    public void setWeapon(Item_Weapon itemWeapon)
    {
        this.itemWeapon = itemWeapon;
        bullet = itemWeapon.weaponInfo.bulletprefab.GetComponent<BulletCtrl>();
    }
    public void Fire(RootCtrl rootCtrl)
    {
        Vector2 dic = (rootCtrl.WeaponCtrl.targetTran.position - fireTran.position);
        float dis = dic.magnitude;
        dic = dic.normalized;
        //탄황 생성, 해당 방향으로 발사
        BulletCtrl newBullet = null;
        if (bulletPoolDic.ContainsKey(itemWeapon.weaponKind) == false)
        {
            bulletPoolDic[itemWeapon.weaponKind] = new ObjectPooling<BulletCtrl>();
            bulletPoolDic[itemWeapon.weaponKind].Initialize(bullet, null, 10);
        }
        newBullet = bulletPoolDic[itemWeapon.weaponKind].GetObject(bullet);
        newBullet.dic = dic;
        newBullet.range = itemWeapon.weaponInfo.AttackDistance;//사거리
        newBullet.speed = itemWeapon.weaponInfo.AttackSpeed;//속도
        newBullet.damage = itemWeapon.weaponInfo.AttackPoint;
        newBullet.gameObject.SetActive(true);
    }
}
