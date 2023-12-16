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
        rootCtrl.lifeAction += () => { hp = maxHp; rootCtrl.stateCtrl.RemoveStunned(); };
        hp = maxHp;
    }

    public void SetDamaged(float damage, I_Attacker attacker)
    {
        hp -= damage;
        rootCtrl.aggroAction?.Invoke(attacker);
        //rootCtrl.stateCtrl.
        if (hp <= 0)
        {
            attacker.DeadEvent(rootCtrl);
            rootCtrl.deadAction.Invoke();
            //기절 후 회복 로직
        }
        else
        {
            rootCtrl.stateCtrl.HitedState();
        }
        // 어그로
    }
}


