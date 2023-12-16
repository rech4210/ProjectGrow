using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableManager : MonoBehaviour
{
    public static ScriptableManager instance;

    public Scriptable_Object[] scriptablelist;

    public Scriptable_Object getTable(string name)
    {
        Scriptable_Object selectedscriptable;

        foreach (var scriptable in scriptablelist)
        {
            if(scriptable.name == "name")
            {
                selectedscriptable = scriptable;
                return selectedscriptable;
            }

        }

        return null;
    }

}
