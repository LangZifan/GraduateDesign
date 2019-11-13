using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;
using LitJson;

public enum ResType
{
    Asset,
    Json,
}

public class AssetProxyMgr : MonoBehaviour
{
    public static AssetProxyMgr instance;
    private UnityEngine.Object res = null;
    public static string pre_path = "Prefabs/";
    public static string json_path = Application.persistentDataPath+"Jsons/";
    public static string tex_path = "Textures";
    private Dictionary<string, UnityEngine.Object> res_dic = new Dictionary<string, UnityEngine.Object>();
    private List<string> keys_collection = new List<string>();
    public float loop_time = 30;
    public int loop_count = 20;
    private float current_time = 0;
    public int clear_state = 0;//0代表销毁区不在处理，1代表销毁区正在处理，-1代表正在加载资源

    private void Awake()
    {
        instance = this;
    }

    public UnityEngine.Object LoadAsset(string path)
    {
        lock (res_dic)
        {
            while (clear_state == 1) { }
            clear_state = -1;
            res = null;
            if (res_dic.ContainsKey(path))
                return res_dic[path];
            res = Resources.Load(pre_path + path);
            res_dic.Add(path, res);
            clear_state = 0;
            return res_dic[path];
        }
    }


    public Texture2D LoadTexture(string path)
    {
        Texture2D tex = null;
        tex = Resources.Load(path) as Texture2D;
        return tex;
    }


    public void UnLoadAsset(UnityEngine.Object asset)
    {
        Resources.UnloadAsset(asset);
    }

    public AudioClip LoadAudioClip(string path)
    {
        AudioClip clip = Resources.Load(path) as AudioClip;
        return clip;
    }

    private void Update()
    {
        current_time += Time.deltaTime;
        if (current_time > loop_time)
        {
            current_time = 0;
            if (res_dic.Count > loop_count)
            {
                keys_collection.Clear();
                foreach (string key in res_dic.Keys)
                {
                    keys_collection.Add(key);
                }

                foreach (string key in keys_collection)
                {
                    if (clear_state == 0)
                    {
                        clear_state = 1;
                        UnityEngine.Object obj = res_dic[key];
                        res_dic.Remove(key);
                        Resources.UnloadAsset(obj);
                        clear_state = 0;
                    }
                    else
                    {
                        break;
                    }
                }
                Resources.UnloadUnusedAssets();
            }
        }
    }
}


