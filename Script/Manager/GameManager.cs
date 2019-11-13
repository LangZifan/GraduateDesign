using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Reflection;


public class GameManager:MonoBehaviour
{
    public static GameManager instance;
    public List<string> managers;
    private void Awake()
    {
        instance = this;
        managers = new List<string> {
            "CacheManger",
            "PlayerManager",
            "AssetProxyMgr",
        };
    }
    private void Start()
    {
        SyncLoadManagers();
    }

    private bool SyncLoadManagers()
    {
        try
        {
            foreach (string s in managers)
            {
                Type t = Type.GetType(s, true, true);
                if (gameObject.GetComponent(t) == null)
                    gameObject.AddComponent(t);
            }
            return true;
        }
        catch(Exception e)
        {
            Debug.Log(e);
            return false;
        }
    }
}
