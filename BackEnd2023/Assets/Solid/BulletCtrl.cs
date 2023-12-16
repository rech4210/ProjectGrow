
using UnityEngine;
using System.Collections;
using System;
public interface I_HitZone
{
    public Faction Faction => Faction.None;
    public RootCtrl RootCtrl => null;
    public void SetDamaged(float damage, I_Attacker attacker);
    public bool CheckHitLock(I_Attacker attacker);//true면 데미지 받지 않음
}
public class BulletCtrl : MonoBehaviour, I_Pool
{
    public int hitCount = 1;
    public int hitTemp;
    public I_Attacker attacker;
    public float damage;
    public float radius = 0.1f;
    public float offsetRange = 0f;
    public Vector3 dic;
    public float range;
    public float speed;
    public Action<I_Pool> disalbleAction;
    public Transform model;
    public float temp;
    public void SetPoolEvent(Action<I_Pool> poolevent)
    {
        disalbleAction += poolevent;
    }

    public void disable()
    {
        disalbleAction?.Invoke(this);
        attacker = null;
    }
    public void enable()
    {
        temp = range / speed;
        hitTemp = hitCount;
        this.transform.eulerAngles = Vector3.zero;
    }

    private void Update()
    {
        temp -= Time.deltaTime;
        float moveRange = Time.deltaTime * (range / speed);
        RaycastHit2D[] hits = Physics2D.CircleCastAll(this.transform.position, radius, dic, moveRange + offsetRange, LayerManager.Instance.HitZone);

        if (hits != null && hits.Length > 0)
        {
            //판정해야함
            foreach (RaycastHit2D hit in hits)
            {
                I_HitZone hitZone = hit.transform.GetComponentInParent<I_HitZone>();
                if (hitZone != null)
                {
                    if (attacker.Faction != hitZone.Faction && hitZone.CheckHitLock(attacker) == false)
                    {
                        hitZone.SetDamaged(damage, attacker);//데미지 작업해야함
                        hitTemp--;
                        if (hitTemp <= 0f)
                        {
                            disable();//피격했으니까 제거 - 탄마다 다르게 처리해야함
                            return;
                        }
                    }
                }
            }
        }
        this.transform.Translate(moveRange * dic, Space.World);
        model.transform.eulerAngles = SolidUtility.getAngle2D(dic);
        if (temp <= 0f)
        {
            this.gameObject.SetActive(false);
            this.disable();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(this.transform.position, this.transform.position + (dic * offsetRange));
    }
}
