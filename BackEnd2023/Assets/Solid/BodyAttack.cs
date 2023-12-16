using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyAttack : MonoBehaviour
{

    public bool isLock;
    public void FixedUpdate()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(this.transform.position, 0.25f);
        if (targets != null)
        {
            foreach (Collider2D target in targets)
            {
                BodyAttack body = target.GetComponent<BodyAttack>();
                if (body != null)
                {
                    if (body.isLock == false)
                    {
                        Vector3 dic = (body.transform.position - this.transform.position).normalized;
                        body.transform.Translate(dic * 0.2f);
                    }
                }
            }
        }
    }
}
