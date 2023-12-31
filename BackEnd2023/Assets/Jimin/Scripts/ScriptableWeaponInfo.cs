using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Prefabs", menuName = "ScriptableWeaponInfo/Prefabs")]
public class ScriptableWeaponInfo : ScriptableObject, I_Scriptable
{
    public string Name => this.name;
    [System.Serializable]
    public class PrefabInfo : ScriptableInfo
    {
        public SeedKind name;
        public TypeEnum Type;
        public EnergyTypeEnum EnergyType;
        public int BulletCount;
        public float GaugeTime;
        public float HP;
        public float AttackPoint;
        public float AttackSpeed;
        public float AttackDistance;
        public bool SpecialOption;
        public AbilityObjectEnum AbilityObject;
        public AbilityEnum Ability;
        public AbilityConditionEnum AbilityCondition;
        public int AbilityCount;
        public GameObject weaponPrefab;
        public GameObject bulletprefab;
    }
    public List<PrefabInfo> prefabInfo;
    public List<T> Prefablist<T>() where T : class, ScriptableInfo
    {
        return prefabInfo.ConvertAll((x) => x as T);
    }
    public T getPrefab<T>(string prefabs) where T : class, ScriptableInfo
    {
        foreach (var tmp in prefabInfo)
        {
            if (tmp.name.ToString() == prefabs)
            {
                return tmp as T;
            }
        }

        return null;
    }
}

public enum TypeEnum
{
    Weapon,
    Tower
}

public enum EnergyTypeEnum
{
    Bullet,
    Gauge,
    Null
}
public enum AbilityObjectEnum
{
    Null,
    Enemy,
    Pot
}
public enum AbilityEnum
{
    Null,
    LinkDamage,
    GaugeVitalization
}
public enum AbilityConditionEnum
{
    Null,
    Nearby,
    Hit
}