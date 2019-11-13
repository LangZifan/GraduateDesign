using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharactorControllerBase : MonoBehaviour {
    public Vector2 normal_speed;  //正常移动速度
    public Vector2 run_speed; //跑步移动速度
    public Vector2 current_speed;//目前速度
    public float rotate_speed; // 镜头旋转速度
    public float gravity;   //重力
    public float jump_power;
    public bool isGround;   //是否着地
    public float follow_speed; // 镜头跟随速度
    public float speed_up_time;
    public int run_state = 0; // 速度变化的状态

    public Animator anim;
    public AudioSource aud;
    public Rigidbody rig;


    private float input_x;
    private float input_y;
    // Use this for initialization
    void Start () {
        		
	}

    // Update is called once per frame
    void Update() {
        AnimatorStateInfo body_layer = anim.GetCurrentAnimatorStateInfo(0);
        //AnimatorStateInfo left_arm_layer = anim.GetCurrentAnimatorStateInfo(1);
        //AnimatorStateInfo right_arm_layer = anim.GetCurrentAnimatorStateInfo(1);
        //AnimatorStateInfo foot_layer = anim.GetCurrentAnimatorStateInfo(2);
        //AnimatorStateInfo head_layer = anim.GetCurrentAnimatorStateInfo(3);
        if (run_state == 1)
        {



        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (Vector3.Distance(current_speed, run_speed) > Mathf.Epsilon)
            {
                current_speed = Vector3.Lerp(current_speed, run_speed, speed_up_time * Time.deltaTime);
            }
            else
            {
                current_speed = run_speed;
            }
            UpdateXY(current_speed);
        }
        else
        {
            if(Vector3.Distance)
            current_speed = Vector3.Lerp(current_speed, normal_speed, speed_up_time * Time.deltaTime);
            

        }

        if (isGround)
        {
            input_x = anim.GetFloat("InputHorizontal");
            input_y = anim.GetFloat("InputVertical");
            if (input_x > Mathf.Epsilon || input_y > Mathf.Epsilon)
            {
                
            }
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGround)
            {
                rig.AddForce(transform.up * jump_power);
                isGround = false;
            }
        }

        if (rig.velocity.z < Mathf.Epsilon)
        {
            if (isGround == false) isGround = true; 
        }
    }

    private void LateUpdate()
    {
            
    }

    private void UpdateXY(Vector2 velocity)
    {
        transform.Translate(transform.forward * input_y * velocity.y + transform.right * input_x * velocity.x);
    }
}
