using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCtrl : MonoBehaviour
{

    public I_RootCtrl rootCtrl;

    public static ItemCtrl selectItemCtrl(Transform pivotTran, Func<ItemCtrl, bool> checkUse)
    {
        Collider2D[] potList = Physics2D.OverlapCircleAll(pivotTran.position, 1f, ItemCtrl.ItemInterObj);
        ItemCtrl hitCtrl = null;

        if (potList != null)
        {
            float minDis = 0f;
            for (int i = 0; i < potList.Length; i++)
            {
                ItemCtrl item = potList[i].GetComponent<ItemCtrl>();
                if (item != null)
                {
                    if (checkUse(item))
                    {
                        float dis = Vector2.Distance(pivotTran.position, item.transform.position);
                        if (dis < minDis || hitCtrl == null)
                        {
                            hitCtrl = item;
                            minDis = dis;
                        }
                    }
                }
            }

        }
        return hitCtrl;
    }
    public void InteractionEnter()
    {

    }
    public void InteractionStay()
    {

    }
    public void InteractionExit()
    {

    }
    public void InteractionGrab()
    {

    }

}
