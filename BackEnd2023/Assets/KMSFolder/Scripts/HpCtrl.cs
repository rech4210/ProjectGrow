using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpCtrl : MonoBehaviour, InitiallizeInterface
{
    RootCtrl rootCtrl;

    // 체력 관리, 피격 처리, 상태머신과 상호작용
    public void initiallize()
    {
        rootCtrl = gameObject.GetComponent<RootCtrl>();
    }

    public void GetDamaged()
    {
        rootCtrl.stateCtrl.StateChange(stateEnum.Stunned);
        //rootCtrl.stateCtrl.StunState();
        // 플레이어 처리
    }
}
