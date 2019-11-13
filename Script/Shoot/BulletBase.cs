using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;



public class BulletBase : MonoBehaviour
{
    public int damage;
    public float speed;
    public float live_time;
    public BulletType bullet_type;
    public Texture2D bolt;
    public float sound_radius;
    public virtual void Hit()
    {


    }
}

public class PistolBullet:BulletBase
{
    public void InitPistolBullet(int Damage, float Speed, float Live_Time,float Sound_Radius)
    {
        damage = Damage;
        speed = Speed;
        live_time = Live_Time;
        bullet_type = BulletType.Pistol;
        sound_radius = Sound_Radius;
    }

    private void Awake()
    {
        DefaultPistolBullet();
        AudioSource aus =  gameObject.AddComponent<AudioSource>();
        aus.clip = AssetProxyMgr.instance.LoadAudioClip("Timer");
        aus.maxDistance = sound_radius;
    }

    public void DefaultPistolBullet()
    {
        damage = 10;
        speed = 20;
        live_time = 10;
        bullet_type = BulletType.Pistol;
        sound_radius = 20;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Ray ray = new Ray();
            ray.direction = GetComponent<Rigidbody>().velocity;
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, speed * live_time,(int)LayerMask.Wall))
            {
                Texture2D tex = (Texture2D)hit.collider.gameObject.GetComponent<Renderer>().material.mainTexture;
                ApplyBulletHole(tex, hit.textureCoord);
                DestroySelf();
            }
        }
    }

    private void ApplyBulletHole(Texture2D tex,Vector2 point)
    {
        point.x *= tex.width;
        point.y *= tex.height;
        bolt = AssetProxyMgr.instance.LoadTexture(AssetPath.tex2D_pistol_bolt);
        for (int i = 0; i < bolt.width; i++)
        {
            for (int j = 0; j < bolt.height; j++)
            {
                tex.SetPixel((int)point.x + i, (int)point.y + j, bolt.GetPixel(i, j));
            }
        }
        tex.Apply();
    }

    private void DestroySelf()
    {
      
    }
}

public class SubmachineBullet : BulletBase
{

}

public class RifleBullet : BulletBase
{


}

public class SniperBullet : BulletBase
{


}

public class MissileBullet : BulletBase
{


}