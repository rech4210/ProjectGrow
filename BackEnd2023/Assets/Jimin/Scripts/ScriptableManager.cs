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

    public List<I_Scriptable> scriptablelist;

    public List<ScriptableObject> scriptableobjedctlist;

    public void Initalize()
    {
        ScriptableMonsterInfo monsterscriptable = Resources.Load<ScriptableMonsterInfo>("MonsterScriptable");
        Scriptable_Object scriptable = Resources.Load<Scriptable_Object>("Scriptable");

        scriptablelist.Add(monsterscriptable);
        scriptablelist.Add(scriptable);
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
