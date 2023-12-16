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
    
}
