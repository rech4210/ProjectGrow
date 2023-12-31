using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    private Item_Weapon itemWeapon;
    private Transform fireTran;

    public float offset = 0f;

    public static Dictionary<SeedKind, ObjectPooling<BulletCtrl>> bulletPoolDic = new Dictionary<SeedKind, ObjectPooling<BulletCtrl>>();
    public BulletCtrl bullet;

    private void Awake()
    {
        if (fireTran == null)
        {
            fireTran = this.transform;
        }
    }
    public void setWeapon(Item_Weapon itemWeapon)
    {
        this.itemWeapon = itemWeapon;
        bullet = itemWeapon.weaponInfo.bulletprefab.GetComponent<BulletCtrl>();
        if (itemWeapon.weaponKind == SeedKind.Minigun)
        {
            offset = 0.4f;
        }
        else
        {
            offset = 0f;
        }
    }
    public void Fire(I_Attacker attacker)
    {
        Vector2 dic = (attacker.TargetTran.position - fireTran.position);

        float dis = dic.magnitude;
        dic = dic.normalized;
        if (offset > 0f)
        {
            dic.x += UnityEngine.Random.Range(-offset, offset);
            dic.y += UnityEngine.Random.Range(-offset, offset);
            dic = dic.normalized;
        }
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
        //newBullet.speed = 1f;
        //itemWeapon.weaponInfo.AttackSpeed;//초당 공격속도
        newBullet.damage = itemWeapon.weaponInfo.AttackPoint;
        newBullet.attacker = attacker;
        newBullet.gameObject.SetActive(true);
        newBullet.transform.position = fireTran.position;
        newBullet.transform.eulerAngles = SolidUtility.getAngle2D(dic);
        newBullet.enable();
    }
}
