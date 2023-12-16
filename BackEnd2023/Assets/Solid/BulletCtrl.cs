
using UnityEngine;
using System.Collections;
using System;
public interface I_HitZone
{
    public RootCtrl RootCtrl => null;
    public void SetDamaged(float damage, RootCtrl attacker);

}
public class BulletCtrl : MonoBehaviour, I_Pool
{
    public RootCtrl attacker;
    public float radius = 0.1f;
    public Vector3 dic;
    public float range;
    public float speed;
    public Action<I_Pool> disalbleAction;
    public Transform model;
    public void SetPoolEvent(Action<I_Pool> poolevent)
    {
        disalbleAction += poolevent;
    }

    public void disable()
    {
        disalbleAction?.Invoke(this);
    }

    private void Update()
    {
        float moveRange = Time.deltaTime * speed;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(this.transform.position, radius, dic, moveRange, WeaponCtrl.HitZone);

        if (hits != null && hits.Length > 0)
        {
            //판정해야함
            foreach (RaycastHit2D hit in hits)
            {
                I_HitZone hitZone = hit.transform.GetComponentInParent<I_HitZone>();
                if (hitZone != null)
                {
                    if (attacker.WeaponCtrl.faction != hitZone.RootCtrl.WeaponCtrl.faction)
                    {
                        //rootCtrl.hpCtrl.SetDamaged(float damage, RootCtrl attacker);//데미지 작업해야함
                        disable();//피격했으니까 제거 - 탄마다 다르게 처리해야함
                        return;
                    }
                }
            }
        }
        this.transform.Translate(moveRange * dic, Space.World);
        model.transform.eulerAngles = SolidUtility.getAngle2D(dic);

    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawLine(this.transform.position, this.transform.position + dic);
    //}
}
