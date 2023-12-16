using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "Prefabs", menuName = "Scriptable_Object/Prefabs")]
public class Scriptable_Object : ScriptableObject, I_Scriptable
{
    public string Name => this.name;
    [System.Serializable]
    public class PrefabInfo:ScriptableInfo
    {
        public string Item_Name;
        public GameObject Prefabs;
    }

    public List<PrefabInfo> prefabInfo;

    public List<T> Prefablist<T>() where T : class, ScriptableInfo
    {
        return prefabInfo.ConvertAll((x) => x as T);
    }
    public T getPrefab<T>(string prefabs) where T : class, ScriptableInfo
    {
        foreach(var tmp in prefabInfo)
        {
            if(tmp.Item_Name == prefabs)
            {
                return tmp as T;
            }
        }

        return null;
    }
}
