using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Prefabs", menuName = "Scriptable_Object/Prefabs")]
public class Scriptable_Object : ScriptableObject
{
    [System.Serializable]
    public class PrefabInfo
    {
        public string Item_Name;
        public GameObject Prefabs;
    }

    public List<PrefabInfo> prefabInfo;


    public GameObject getPrefab(string prefabs)
    {
        foreach(var tmp in prefabInfo)
        {
            if(tmp.Item_Name == prefabs)
            {
                return tmp.Prefabs;
            }
        }

        return null;
    }
}
