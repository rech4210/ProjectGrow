using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInput : InputCtrl
{
    //hit 피격시 행동 추가하기 + 상태 애니메이션
    
    [SerializeField]Transform targetPlayer;
    private void Update()
    {
        //transform.position = targetPlayer.position;
    }
}
