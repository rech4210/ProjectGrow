using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "Prefabs", menuName = "ScriptableMonsterInfo/Prefabs")]
public class ScriptableMonsterInfo : ScriptableObject, I_Scriptable
{
    public string Name => this.name;
    [System.Serializable]
    public class PrefabInfo : ScriptableInfo
    {
        public MonsterKind name;
        public MonsterTypeEnum Type;
        public int HP;
        public int Atk;
        public int AttackSpeed;
        public int MoveSpeed;
        public int ActiveRange;
        public int SightRange;
        public bool ChaseT_F;
        public int ChaseTime;
        public bool FirstAttack;
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
            if (tmp.name.ToString() == prefabs)
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