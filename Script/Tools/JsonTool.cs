using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;
using UnityEngine;
using System.IO;
public static class JsonTool
{
    public static void SaveAsJson(string path, object data)
    {
        try
        {
            string js = JsonMapper.ToJson(data);
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(js);
            File.WriteAllBytes(path, bytes);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return;
        }
    }

    public static JsonData LoadFromJson(string path)
    {
        JsonData js_data = null;
        try
        {
            byte[] data = File.ReadAllBytes(path);
            string js = System.Text.Encoding.UTF8.GetString(data);
            js_data = JsonMapper.ToObject(js);
        }
        catch(Exception e)
        {
            Debug.LogError(e);
        }
        return js_data;
    }
}

