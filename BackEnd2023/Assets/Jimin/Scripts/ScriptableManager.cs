using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ScriptableInfo
{

}

public interface I_Scriptable
{
    public virtual string Name => null;
    public List<T> Prefablist<T>() where T : class, ScriptableInfo;
    T getPrefab<T>(string name) where T : class, ScriptableInfo;
}

public class ScriptableManager : MonoBehaviour
{
    public static ScriptableManager _instance;
    public static ScriptableManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ScriptableManager>();
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<ScriptableManager>();
                }
                _instance.Initalize();
            }
            return _instance;
        }
    }

    public List<I_Scriptable> scriptablelist = new List<I_Scriptable>();


    public void Initalize()
    {
        ScriptableMonsterInfo monsterscriptable = Resources.Load<ScriptableMonsterInfo>("MonsterScriptable");
        Scriptable_Object scriptable = Resources.Load<Scriptable_Object>("Scriptable");
        ScriptableWeaponInfo Weaponscriptable = Resources.Load<ScriptableWeaponInfo>("WeaponScriptable");
        ScriptablePlantInfo PlantScriptable = Resources.Load<ScriptablePlantInfo>("PlantScriptable");

        scriptablelist.Add(monsterscriptable);
        scriptablelist.Add(scriptable);
        scriptablelist.Add(Weaponscriptable);
        scriptablelist.Add(PlantScriptable);
    }

    public I_Scriptable getTable(string name)
    {
        I_Scriptable selectedscriptable = null;

        foreach (var scriptable in scriptablelist)
        {
            if (scriptable != null && scriptable.Name == "name")
            {
                selectedscriptable = scriptable;
                return selectedscriptable;
            }

        }

        return null;
    }

}
