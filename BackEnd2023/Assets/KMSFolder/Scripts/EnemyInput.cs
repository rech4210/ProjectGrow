using UnityEngine;

public class EnemyInput : InputCtrl
{
    


    private void Update()
    {
        //transform.position = targetPlayer.position;

        if (rootCtrl.targetTran != null)
        {
            Vector3 direction = rootCtrl.targetTran.position - transform.position;
            direction.Normalize();
          horizontal = direction.x;
        vertical = direction.y;
        }

    }

    public override void initiallize()
    {
        base.initiallize();
        //Hp Ctrl aggro reference use
        rootCtrl.aggroAction += (attacker) =>
        {
            rootCtrl.WeaponCtrl.targetTran = attacker.transform;
        };

        //
    }

    // ai가 타켓을 가지고 있는데 타겟이 변경될때마다
    // weapon ctrl에 타켓 트랜스폼이 있는데 그걸 최신화 시켜줘야함
    // 액션을 만든다면 변경된 적 타켓의 transform을 weapon에 넣어줘야함.
}
