using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "Prefabs", menuName = "ScriptableMonsterInfo/Prefabs")]
public class ScriptableMonsterInfo : ScriptableObject, I_Scriptable
{
    public string Name => this.name;
    [System.Serializable]
    public class PrefabInfo : ScriptableInfo
    {
        public MonsterKind Name;
        public MonsterTypeEnum Type;
        public float HP;
        public float Atk;
        public float AttackSpeed;
        public float MoveSpeed;
        public float ActiveRange;
        public float SightRange;
        public bool ChaseT_F;
        public float ChaseTime;
        public bool FirstAttack;

        public GameObject prefab;
    }

    public List<PrefabInfo> prefabInfo;

    public List<T> Prefablist<T>() where T:class,  ScriptableInfo
    {
        return prefabInfo.ConvertAll((x)=>x as T);
    }
    public T getPrefab<T>(string prefabs) where T : class, ScriptableInfo
    {
        foreach (var tmp in prefabInfo)
        {
            if (tmp.Name.ToString() == prefabs)
            {
                return tmp as T;
            }
        }

        return null;
    }

}

public enum MonsterTypeEnum
{
    normal,
    Tanker
}