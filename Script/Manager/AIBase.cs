using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public enum HandAction
{
    Relax,
    Hold_Weapon,
    Fire,
    Hit,
    Shake
}

public enum BodyAction
{
    Stop,
    Walk,
    Run,
    Crouch,
    Hide_Wall,
    Hide_Down,
}

public enum HeadAction
{
    Stop,
    Turn_Back,
    Stun
}

public enum ActionState
{
    Stand,
    Idle_No_Weapon,
    Idle_Weapon,
    Walk_No_Weapon,
    Walk_Weapon,
    Run_No_Weapon,
    Run_Weapon,
    Hide_Wall_NO_Weapon,
    Hide_Wall_Weapon,
    Crouch_No_Weapon,
    Crouch_Weapon,
    Patrol_No_Weapon,
    Patrol_Weapon,
}

public class AIStateBase
{
    public ActionState type;
    public HandAction hand_action;
    public BodyAction body_action;
    public HeadAction head_action;

    public virtual void EnterState()
    {


    }

    public void ExitState()
    {


    }

    public void UpdateState()
    {


    }
}
public class AIStateController
{
    public AIStateBase currentState;
    public Dictionary<string, AIStateBase> state_dic;
    public bool updateing;

    public void Execute()
    {
        if (currentState != null && updateing)
            currentState.UpdateState();
    }

    public void ChangeState(string state)
    {
        updateing = false;
        if (currentState!=null)
        {
            currentState.ExitState();
        }
        if (!state_dic.ContainsKey(state))
        {
            try
            {
                Type t = Type.GetType(state, true, true);
                state_dic[state] = Activator.CreateInstance(t) as AIStateBase;
            }
            catch(Exception e)
            {
                Debug.LogError(e);
            }
        }
        currentState = state_dic[state];
        currentState.EnterState();
        updateing = true;
    }
}

public class AITriggerBase:MonoBehaviour
{
    public virtual void UpdateTrigger()
    {
                   
    }
}

public class AIHumanoidTrigger : AITriggerBase
{
    public string state;
    public override void UpdateTrigger()
    {
          

    }

    public void SightTrigger()
    {
                                         

    }

    public void SoundTrigger()
    {


    }

    public void FriendTrigger()
    {


    }

    public void HelpTrigger()
    {


    }
}


public class AIRobotTrigger : AITriggerBase
{
    public string state;

    public void ScanTrigger()
    {


    }


    public void SoundTrigger()
    {


    }
}