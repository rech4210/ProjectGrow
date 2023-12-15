using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ItemCtrl : MonoBehaviour
{
    ///아이템 태그
    ///사용 함수 (루트 컨트롤 보내주세요
    ///들기 놓기
    ///


    //마우스 클릭의 아이템 사용 처리
    public abstract void UseCall(I_RootCtrl rootCtrl);
    //들기 놓기 처리
    public abstract void GrabToggle(I_RootCtrl rootCtrl, bool isGrab);

}
