using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ShootForce:MonoBehaviour
{
    public class RandomShootSiumulation
    {
        public bool isShoot = false;
        public float max_radius = 0;
        public float change_speed = 1;
        public float current_radius = 0;
        public float currnet_radius = 0;//当前位置和原点之间连线的平面距离
        public float org_offset = 0.4f;//静止时的半径
        public RandomShootSiumulation(float Radius,float ChangeSpeed)
        {
            max_radius = Radius;
            change_speed = ChangeSpeed;
        }
        public Vector3 current_value = Vector3.zero;
        public Vector3 target = Vector3.zero;

        public void UpdatePos(float deltaTime)
        {
            target = GetRandomTarget();
            current_value = Vector3.Lerp(current_value, target, deltaTime*change_speed);
        }

        public Vector3 GetRandomTarget()
        {
            currnet_radius = Vector3.Distance(current_value, new Vector3(0,0,current_value.z));
            float spread = currnet_radius / max_radius;
            if (isShoot == false)
            {
                current_radius = spread * max_radius;
                return Vector3.zero;
            }
            else
            {
                float l = UnityEngine.Random.Range(spread*max_radius, max_radius * (spread+org_offset));
                float angle = UnityEngine.Random.Range(0, 180) * Mathf.Deg2Rad;

                float x_offset = l * Mathf.Cos(angle);
                float y_offset = l * Mathf.Sin(angle);
                float z_offset = l * spread;//按照弧度去算，越来越接近1:1
                current_radius = spread * max_radius;
                return new Vector3(x_offset, y_offset,z_offset);
            }
        }

    }

    public class SniperShootSimulation
    {
        public Vector3 offset = Vector3.zero;
        public Vector3 current_value = Vector3.zero;
        public float delta_time;
        public bool recover = false;
        public SniperShootSimulation(Vector3 Offset,float Delta_Time)
        {
            offset = Offset;
            delta_time = Delta_Time;
        }

        public void UpdatePos()
        {
            if (Mathf.Abs(current_value.z - offset.z) < Mathf.Epsilon && recover == false)
            {
                recover = true;
            }
            else
            {
                if (recover == true)
                {
                    current_value = Vector3.Lerp(current_value, Vector3.zero, delta_time);
                }
                else
                {
                    current_value = Vector3.Lerp(current_value, offset, delta_time);
                }
            }

        }
    }
}
