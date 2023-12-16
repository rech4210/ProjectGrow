using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ScriptableInfo
{

}

public interface I_Scriptable
{
    public string Name => null;
    T getPrefab<T>(string name) where T : class, ScriptableInfo;
}

public class ScriptableManager : MonoBehaviour
{
    public static ScriptableManager instance;

    public List<I_Scriptable> scriptablelist = new List<I_Scriptable>();

    public void Initalize()
    {
        ScriptableMonsterInfo monsterscriptable = Resources.Load<ScriptableMonsterInfo>("MonsterScriptable");
        Scriptable_Object scriptable = Resources.Load<Scriptable_Object>("Scriptable");
        ScriptableWeaponInfo Weaponscriptable = Resources.Load<ScriptableWeaponInfo>("WeaponScriptable");

        scriptablelist.Add(monsterscriptable);
        scriptablelist.Add(scriptable);
        scriptablelist.Add(Weaponscriptable);
    }

    public I_Scriptable getTable(string name)
    {
        I_Scriptable selectedscriptable = null;

        foreach (var scriptable in scriptablelist)
        {
            if(scriptable.Name == "name")
            {
                selectedscriptable = scriptable;
                return selectedscriptable;
            }

        }

        return null;
    }

}
