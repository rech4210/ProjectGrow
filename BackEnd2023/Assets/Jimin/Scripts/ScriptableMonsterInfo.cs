using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "Prefabs", menuName = "ScriptableMonsterInfo/Prefabs")]
public class ScriptableMonsterInfo : ScriptableObject, I_Scriptable
{
    public string Name => this.name;
    [System.Serializable]
    public class PrefabInfo : ScriptableInfo
    {
        public string name;
        public enum Type
        {
            normal,
            Tanker
        }
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


    public T getPrefab<T>(string prefabs) where T : class, ScriptableInfo
    {
        foreach (var tmp in prefabInfo)
        {
            if (tmp.name == prefabs)
            {
                return tmp as T;
            }
        }

        return null;
    }
}