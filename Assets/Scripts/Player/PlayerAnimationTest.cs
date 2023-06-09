using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTest : MonoBehaviour
{
    //玩家动画参数
    public float xInput;
    public float yInput;
    public bool isWalking;
    public bool isRunning;
    public bool isCarrying;
    public ToolEffect toolEffect;
    public bool isUsingToolRight;
    public bool isUsingToolLeft;
    public bool isUsingToolUp;
    public bool isUsingToolDown;
    public bool isLiftingToolRight;
    public bool isLiftingToolLeft;
    public bool isLiftingToolUp;
    public bool isLiftingToolDown;
    public bool isSwingingToolRight;
    public bool isSwingingToolLeft;
    public bool isSwingingToolUp;
    public bool isSwingingToolDown;
    public bool isPickingRight;
    public bool isPickingLeft;
    public bool isPickingUp;
    public bool isPickingDown;

    //共享动画参数
    public bool isIdle;
    public bool idleUp;
    public bool idleDown;
    public bool idleLeft;
    public bool idleRight;

    private void Update()
    {
        EventHandler.CallMovementEvent( xInput,  yInput,  isWalking,  isRunning,  isIdle,  isCarrying,  toolEffect,isUsingToolRight,  isUsingToolLeft,  isUsingToolUp,  isUsingToolDown,isLiftingToolRight,  isLiftingToolLeft,  isLiftingToolUp,  isLiftingToolDown,isPickingRight,  isPickingLeft,  isPickingUp,  isPickingDown,isSwingingToolRight,  isSwingingToolLeft,  isSwingingToolUp,  isSwingingToolDown,idleUp,  idleDown,  idleLeft,  idleRight);
    }
}

