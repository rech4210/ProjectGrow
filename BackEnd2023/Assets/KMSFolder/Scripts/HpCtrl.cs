using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpCtrl : MonoBehaviour, InitiallizeInterface
{
    RootCtrl rootCtrl;
    float hp;
    public float maxHp = 30;

    // 체력 관리, 피격 처리, 상태머신과 상호작용
    public void initiallize()
    {
        rootCtrl = gameObject.GetComponent<RootCtrl>();
        rootCtrl.lifeAction += () => { hp = maxHp; };
        hp = maxHp;
    }

    public void GetDamaged()
    {
        rootCtrl.stateCtrl.StateChange(stateEnum.Stunned);
        //rootCtrl.stateCtrl.StunState();
        // 플레이어 처리
    }

    public void SetDamaged(float damage, RootCtrl attacker)
    {
        hp -=damage;
        if(hp <= 0)
        {
            rootCtrl.deadAction.Invoke();
        }
        // 어그로
    }
}
