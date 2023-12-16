using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemKind
{
    None = 0,
    Slot = 1,
    Pot = 2,
    Seed = 3,//씨앗
    Weapon = 5,//무기
}
public abstract class ItemCtrl : MonoBehaviour, I_Pool
{
    public static ItemCtrl newItem(ItemKind itemKind, string tag)
    {
        switch (itemKind)
        {
            case ItemKind.Pot:
                return newItem(ScriptableManager.instance.getTable("Scriptable").getPrefab<Scriptable_Object.PrefabInfo>("Pot").Prefabs.GetComponent<ItemCtrl>());
            case ItemKind.Seed:
                return newItem(ScriptableManager.instance.getTable("PlantScriptable").getPrefab<ScriptablePlantInfo.PrefabInfo>(tag).prefab.GetComponent<ItemCtrl>());
            case ItemKind.Weapon:
                return newItem(ScriptableManager.instance.getTable("WeaponScriptable").getPrefab<ScriptablePlantInfo.PrefabInfo>(tag).prefab.GetComponent<ItemCtrl>());
        }
        return null;
    }
    public static ItemCtrl newItem(ItemCtrl prefab)
    {
        if (poolDic.ContainsKey(prefab.itemKind) == false)
        {
            poolDic[prefab.itemKind] = new ObjectPooling<ItemCtrl>();
            poolDic[prefab.itemKind].Initialize(prefab, GameManager.instance.transform, 10);
        }
        return poolDic[prefab.itemKind].GetObject(prefab);
    }
    public static Dictionary<ItemKind, ObjectPooling<ItemCtrl>> poolDic = new Dictionary<ItemKind, ObjectPooling<ItemCtrl>>();

    protected Action<I_Pool> disableAction;
    public void SetPoolEvent(Action<I_Pool> poolevent)
    {
        disableAction += poolevent;
    }
    public virtual void disable()
    {
        //풀링 회수 
        disableAction?.Invoke(this);
    }
    ///아이템 태그
    ///사용 함수 (루트 컨트롤 보내주세요
    ///들기 놓기

    public virtual ItemKind itemKind => ItemKind.None;

    //public bool isGrab;//그랩상태 필요한가?
    [SerializeField]
    public bool isGrabLock;//그랩 가능 불가능
    public virtual bool IsGrabLock => isGrabLock;
    [SerializeField]
    public bool isInterLock;//인터렉션 잠금
    public virtual bool IsInterLock => isInterLock;


    //마우스 클릭의 아이템 사용 처리
    public abstract void UseCall(RootCtrl rootCtrl, UseState useState);
    //들기 놓기 처리
    public abstract ItemCtrl GrabToggle(RootCtrl rootCtrl, bool isGrab);


    public abstract bool checkUse(ItemCtrl nowItem);




}
