using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemKind
{
    None = 0,
    Slot = 1,
    Pot = 2,
    Seed = 3,//씨앗

}
public abstract class ItemCtrl : MonoBehaviour
{

    ///아이템 태그
    ///사용 함수 (루트 컨트롤 보내주세요
    ///들기 놓기

    public virtual ItemKind itemKind => ItemKind.None;

    //public bool isGrab;//그랩상태 필요한가?
    public bool isGrabLock;//그랩 가능 불가능
    public bool isInterLock;//인터렉션 잠금


    //마우스 클릭의 아이템 사용 처리
    public abstract void UseCall(RootCtrl rootCtrl, UseState useState);
    //들기 놓기 처리
    public abstract void GrabToggle(RootCtrl rootCtrl, bool isGrab);
    public abstract bool checkUse(ItemCtrl nowItem);


    public virtual void disable()
    {
        //풀링 회수 
    }
}
