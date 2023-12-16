using UnityEngine;

public enum Faction
{
    None = 0,
    Player = 1,
    Pot = 2,
    Enemy = 10,
}
public class WeaponCtrl : MonoBehaviour, InitiallizeInterface
{


    RootCtrl rootCtrl;
    public Transform targetTran;


    //공격 호출

    //마우스 조준 처리

    //스킬 사용 처리
    public void initiallize()
    {
        rootCtrl = gameObject.GetComponent<RootCtrl>();
    }

    private Transform AimPoint()
    {
        return targetTran;
    }

    public void AttackCommand()
    {
        switch (rootCtrl.inputCtrl.attackState)
        {
            case BtnState.None:
                break;
            case BtnState.Down:
                rootCtrl.interaction.InteractionEnter();
                break;
            case BtnState.Stay:
                rootCtrl.interaction.InteractionStay();
                break;
            case BtnState.Up:
                rootCtrl.interaction.InteractionExit();
                break;
        }
    }
    private void Update()
    {
        AttackCommand();
    }

}
