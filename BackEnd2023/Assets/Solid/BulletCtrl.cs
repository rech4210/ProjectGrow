
using UnityEngine;
using System.Collections;
using System;

public class BulletCtrl : MonoBehaviour, I_Pool
{
    public float radius = 0.1f;
    public Vector2 dic;
    public float range;
    public float speed;
    public Action<I_Pool> disalbleAction;
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
        RaycastHit[] hits;
        float moveRange = Time.deltaTime * speed;
        Physics2D.CircleCastAll(this.transform.position, radius, dic, moveRange);
    }
}
