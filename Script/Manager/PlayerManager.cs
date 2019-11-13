using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class WeaponAttributeBase
{
    public int type;
}
        
public class WeaponBaseInfo
{
    public int id;
    public float Damage;
    public float Life;
    public List<WeaponAttributeBase> attributes;
}


public class PlayerBaseInfo
{
    public string name;
    public float life;
    public float shell;
    public float lev;
    public List<WeaponBaseInfo> weapons;
}

public class ShipComponentBase
{
    public string name;
    public bool is_lock;
    public int lev;
}

public class ShipBaseInfo
{
    public string name;
    public int lev;
    public int life;
    public int power;
    public List<ShipComponentBase> component_info;
}

public class PlayerData
{
    public PlayerBaseInfo base_info;
    public ShipBaseInfo ship_info;
}

public class PlayerManager
{
    public const string achrieves_path = "Files/Achrieve";
    public static PlayerManager instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {


    }
    private void LoadAchrieves()
    {
                   

    }
}
