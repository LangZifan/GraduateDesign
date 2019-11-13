using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;




public class PhysicsTool:MonoBehaviour
{

    public static float accumulate_effect = 1;

    private static Dictionary<string, List<Rigidbody>> rig_dic = new Dictionary<string, List<Rigidbody>>();
    private static Dictionary<string,float> rig_mass_dic = new Dictionary<string, float>();
    public static void AddForce(string rig_tag, Vector3 direction, ForceMode force_mode = ForceMode.Acceleration)
    {
        foreach (Rigidbody rig in rig_dic[rig_tag])
        {
            rig.AddForce(direction, force_mode);
        }
    }

    public static void AddGravityController(string rig_tag,GameObject G)
    {
        Rigidbody rig = null;
        if (G.GetComponent<Rigidbody>() == null)
        {
            G.AddComponent<Rigidbody>();
        }
        rig = G.GetComponent<Rigidbody>();
        if (rig_dic.ContainsKey(rig_tag) == false)
        {
            List<Rigidbody> rig_list = new List<Rigidbody>();
            rig_list.Add(rig);
            rig_dic.Add(rig_tag, rig_list);
        }
    }

    public static void ChangeGravityEffect(string rig_tag, float effect)
    {
        if (rig_dic.ContainsKey(rig_tag))
        {
            for (int i = 0; i < rig_dic.Count; i++)
            {
               rig_dic[rig_tag][i].mass *= effect ;
            }
        }
    }

    public static void RecoverGravityEffect(string rig_tag)
    {
        if (rig_dic.ContainsKey(rig_tag))
        {
            float mass = rig_mass_dic[rig_tag];
            for (int i = 0; i < rig_dic[rig_tag].Count; i++)
            {
                rig_dic[rig_tag][i].mass = mass;
            }
        }
    }

    public static void ChangeDefaultGravity(string rig_name, float value)
    {
        if (rig_mass_dic.ContainsKey(rig_name))
        {
            rig_mass_dic[rig_name] = value;
        }
    }

    public static void ChangeVelocity(string rig_tag, Vector3 velocity)
    {
        if (rig_dic.ContainsKey(rig_tag))
        {
            for (int i = 0; i < rig_dic[rig_tag].Count; i++)
            {
                rig_dic[rig_tag][i].velocity = velocity; 
            }                
        }
    }
    
}

public class CameraTool : MonoBehaviour
{

    private void LateUpdate()
    {

    }

}
