using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Prefabs", menuName = "ScriptablePlantInfo/Prefabs")]
public class ScriptablePlantInfo : ScriptableObject, I_Scriptable
{
    public string Name => this.name;
    [System.Serializable]
    public class PrefabInfo : ScriptableInfo
    {
        public PlantNameEnum Name;
        public PlantTypeEnum Type;
        public int GrowTime;
        public int FruitCount;
        public int Reusecount;
        public int SignIcon;
        public int PlantSillust;

        public GameObject prefab;
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
            if (tmp.Name.ToString() == prefabs)
            {
                return tmp as T;
            }
        }

        return null;
    }

}

public enum PlantNameEnum
{
    Rovolver,
    Minigun,
    flame_thrower,
    Lighting,
    Dionaea
}

public enum PlantTypeEnum
{
    Weapon,
    Tower
}
