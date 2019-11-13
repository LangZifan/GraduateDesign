using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class CharactorBase : MonoBehaviour
{
    public AIStateController controller;
    public AITriggerBase trigger;
    public float life;
    public string Type;
    public Animator anim;
    public AudioSource au;
}


public class CharactorBaseInfo
{
    public string name;
    public string desc;
}







