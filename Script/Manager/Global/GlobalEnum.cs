using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GlobalEnum
{


}


public enum BulletType
{
    Pistol,
    Submachine,
    Rifle,
    Sniper,
    Missile
}


public enum LayerMask
{
    Player = 1,
    Wall = 2,
    Other = 4,
}

public static class AssetPath
{
    
    public const string tex2D_pistol_bolt = "bullet/pistol_bolt";
    public const string tex2D_submachine_bolt = "bullet/submachine_bolt";
    public const string tex2D_sniper_bolt = "bullet/sniper_bolt";
    public const string tex2D_rifle_bolt = "bullet/rifle_bolt";
    public const string tex2D_missile_bolt = "bullet/missile_bolt";


    public const string pre_pistol = "bullet/pre_pistol";
    public const string pre_submachine = "bullet/pre_submachine";
    public const string pre_sniper = "bullet/pre_sniper";
    public const string pre_rifle = "bullet/pre_rifle";
    public const string pre_missile = "bullet/pre_missle";
}

public static class FilePath
{
    public const string achrieve_path = "Achrieves/";
}


public static class RigTag
{
    public const string player = "player";
    public const string pistol_bullet = "";
    public const string bullet = "";
}