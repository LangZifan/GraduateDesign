using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Reflection;

public class CacheManager:MonoBehaviour
{
    public static CacheManager instance;
    public float Timer = 2;
    public float currnet_time = 0;
    public bool is_clear;
    public int bullet_init_count = 30;
    public int max_des_count = 300;//控制一次销毁的对象数量
    public List<GameObject> destroy_cache_list = new List<GameObject>();
    private Dictionary<int, List<GameObject>> bullet_dic = new Dictionary<int, List<GameObject>>();
    public List<GameObject> destroy_list = new List<GameObject>();

    public void Awake()
    {
        instance = this;
        bullet_dic.Add((int)BulletType.Missile, new List<GameObject>());
        bullet_dic.Add((int)BulletType.Pistol, new List<GameObject>());
        bullet_dic.Add((int)BulletType.Sniper, new List<GameObject>());
        bullet_dic.Add((int)BulletType.Submachine, new List<GameObject>());
        bullet_dic.Add((int)BulletType.Rifle, new List<GameObject>());
    }

    private void Start()
    {
        
    }
    public void AddDestroyCache(GameObject game)
    {
        lock(destroy_cache_list)
        {
            destroy_cache_list.Add(game);
            game.SetActive(false);
        }
    }

    private void Update()
    {
        currnet_time += Time.deltaTime;
        if (currnet_time > Timer)
        {
            ClearCache();
            currnet_time = 0;
        }
    }

    public void CleanBulletBuffer(BulletType type,int Sum = -1)
    {
        List<GameObject> temp_list = bullet_dic[(int)type];
        GameObject gm = null; 
        lock (temp_list)
        {
            int count = Sum == -1?temp_list.Count:Sum;
            for (int i = 0; i < count; i++)
            {
                gm = temp_list[0];
                temp_list.RemoveAt(0);
                AddDestroyCache(gm);
            }
            gm = null;
        }
    }


    public void InitBulletBuffer(BulletType type)
    {
        List<GameObject> temp_list = new List<GameObject>();
        lock (temp_list)
        {
            StartCoroutine("InitBulletList", type);
        }
    }
   
    private void ClearCache()
    {
        int count = Mathf.Min(destroy_list.Count,max_des_count);
        for (int i = 0; i < count; i++)
        {
            GameObject g = destroy_cache_list[0];
            destroy_cache_list.RemoveAt(0);
            Destroy(g);
        }
    }

    public GameObject GetBullet(BulletType type)
    {
        GameObject gm = null;
        List<GameObject> temp_list = bullet_dic[(int)type];
        if (temp_list.Count > 0)
        {
            gm = temp_list[0];
            temp_list.RemoveAt(0);
            return gm;
        }
        switch (type)
        {
            case BulletType.Pistol:
                gm = Instantiate(AssetProxyMgr.instance.LoadAsset(AssetPath.pre_pistol)) as GameObject;
                break;
            case BulletType.Rifle:
                gm = Instantiate(AssetProxyMgr.instance.LoadAsset(AssetPath.pre_rifle)) as GameObject;
                break;
            case BulletType.Missile:
                gm = Instantiate(AssetProxyMgr.instance.LoadAsset(AssetPath.pre_missile) as GameObject);
                break;
            case BulletType.Sniper:
                gm = Instantiate(AssetProxyMgr.instance.LoadAsset(AssetPath.pre_sniper) as GameObject);
                break;
            case BulletType.Submachine:
                gm = Instantiate(AssetProxyMgr.instance.LoadAsset(AssetPath.pre_submachine) as GameObject);
                break;
        }
        return gm;
    }

    public void DestroyBullet(BulletBase bullet)
    {
        if (bullet.gameObject == null)
        {
            Debug.LogError("No Bullet GameObject");
            return;
        }
        bullet.gameObject.SetActive(false);
        bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
        bullet_dic[(int)bullet.bullet_type].Add(bullet.gameObject);
    }

    IEnumerator InitBulletList(BulletType type)
    {
        int index = 0;
        string path = "";
        List<GameObject> temp_list = bullet_dic[(int)type];
        switch (type)
        {
            case BulletType.Missile:
                path = AssetPath.pre_missile;
                break;
            case BulletType.Pistol:
                path = AssetPath.pre_pistol;
                break;
            case BulletType.Rifle:
                path = AssetPath.pre_rifle;
                break;
            case BulletType.Sniper:
                path = AssetPath.pre_sniper;
                break;
            case BulletType.Submachine:
                path = AssetPath.pre_submachine;
                break;
        }
        
        while (index<bullet_init_count)
        {
            GameObject gm = AssetProxyMgr.instance.LoadAsset(path) as GameObject;
            temp_list.Add(gm);
            index++;
            yield return new WaitForSeconds(0.05f);
        }
    }

}

