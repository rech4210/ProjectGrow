using System;
using UnityEngine;

public class InteractionCtrl : MonoBehaviour, I_Interaction
{

    public RootCtrl rootCtrl;
    public Transform grabPivot;


    [HideInInspector]
    public ItemCtrl selectItemCtrl;
    [HideInInspector]
    public ItemCtrl grabItemCtrl;
    public ItemCtrl GrabItemCtrl;

    public void initiallize()
    {
        rootCtrl = GetComponentInParent<RootCtrl>();
    }

    public void InteractionEnter()
    {
        if (grabItemCtrl != null)
        {
            grabItemCtrl.UseCall(rootCtrl, UseState.Start);
        }
        else
        {
            selectItemCtrl = getSelectItemCtrl(rootCtrl.transform, checkUse);
            if (selectItemCtrl != null)
            {
                selectItemCtrl.UseCall(rootCtrl, UseState.Start);
            }
        }
    }
    public void InteractionStay()
    {
        if (grabItemCtrl != null)
        {
            grabItemCtrl.UseCall(rootCtrl, UseState.Ing);
        }
        else if (selectItemCtrl != null)
        {
            selectItemCtrl.UseCall(rootCtrl, UseState.Ing);
        }
    }
    public void InteractionExit()
    {
        if (grabItemCtrl != null)
        {
            grabItemCtrl.UseCall(rootCtrl, UseState.End);
        }
        else if (selectItemCtrl != null)
        {
            selectItemCtrl.UseCall(rootCtrl, UseState.End);
        }
        selectItemCtrl = null;
    }


    public void interactionGrap()
    {
        if (grabItemCtrl != null)
        {
            grabItemCtrl.transform.SetParent(null);//Todo 아이템 풀링에 접근해서 부모 찾아서 사용 or 풀링에서 회수될때 부모 재설정
            grabItemCtrl.transform.rotation = Quaternion.identity;
            interactionGrabOff();
        }
        else
        {
            grabItemCtrl = getSelectItemCtrl(rootCtrl.transform, checkGrab);
            if (grabItemCtrl != null)
            {
                grabItemCtrl = grabItemCtrl.GrabToggle(rootCtrl, true);
                if (grabItemCtrl != null)
                {
                    grabItemCtrl.transform.SetParent(grabPivot);
                    grabItemCtrl.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                }
            }
        }
    }


    public bool checkUse(ItemCtrl itemCtrl)
    {
        return itemCtrl.IsInterLock == false;
    }
    public bool checkGrab(ItemCtrl itemCtrl)
    {
        return itemCtrl.IsGrabLock == false;
    }







    public static ItemCtrl getSelectItemCtrl(Transform pivotTran, Func<ItemCtrl, bool> checkUse)
    {
        Collider2D[] potList = Physics2D.OverlapCircleAll(pivotTran.position, 1f, LayerManager.Instance.ItemInterObj);
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

    public void interactionThrow()
    {
    }

    public void interactionGrabOff()
    {
        grabItemCtrl?.GrabToggle(rootCtrl, false);

        grabItemCtrl = null;
    }
}
