using System;
using UnityEngine;


//订阅者类
public class MovementAnimationParameterControl : MonoBehaviour
{

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EventHandler.MovementEvent += SetAnimationParameters;
    }

    private void OnDisable()
    {
        EventHandler.MovementEvent -= SetAnimationParameters;
    }


    //订阅事件发生后所执行的方法
    private void SetAnimationParameters(float inputX, float inputY, bool isWalking, bool isRunning, bool isIdle, bool isCarrying, ToolEffect toolEffect, bool isUsingToolRight, bool isUsingToolLeft, bool isUsingToolUp, bool isUsingToolDown, bool isLiftingToolRight, bool isLiftingToolLeft, bool isLiftingToolUp, bool isLiftingToolDown, bool isPickingRight, bool isPickingLeft, bool isPickingUp, bool isPickingDown, bool isSwingingToolRight, bool isSwingingToolLeft, bool isSwingingToolUp, bool isSwingingToolDown, bool idleUp, bool idleDown, bool idleLeft, bool idleRight)
    {
        animator.SetFloat(Settings.xInput, inputX);
        animator.SetFloat(Settings.yInput, inputY);
        animator.SetBool(Settings.isWalking, isWalking);
        animator.SetBool(Settings.isRunning, isRunning);

        animator.SetInteger(Settings.toolEffect, (int)toolEffect);

        if (isUsingToolDown) animator.SetTrigger(Settings.isUsingToolDown);
        if (isUsingToolLeft) animator.SetTrigger(Settings.isUsingToolLeft);
        if (isUsingToolRight) animator.SetTrigger(Settings.isUsingToolRight);
        if (isUsingToolUp) animator.SetTrigger(Settings.isUsingToolUp);

        if (isLiftingToolDown) animator.SetTrigger(Settings.isLiftingToolDown);
        if (isLiftingToolLeft) animator.SetTrigger(Settings.isLiftingToolLeft);
        if (isLiftingToolRight) animator.SetTrigger(Settings.isLiftingToolRight);
        if (isLiftingToolUp) animator.SetTrigger(Settings.isLiftingToolUp);

        if (isSwingingToolDown) animator.SetTrigger(Settings.isSwingingToolDown);
        if (isSwingingToolLeft) animator.SetTrigger(Settings.isSwingingToolLeft);
        if (isSwingingToolRight) animator.SetTrigger(Settings.isSwingingToolRight);
        if (isSwingingToolUp) animator.SetTrigger(Settings.isSwingingToolUp);

        if (isPickingDown) animator.SetTrigger(Settings.isPickingDown);
        if (isPickingLeft) animator.SetTrigger(Settings.isPickingLeft);
        if (isPickingRight) animator.SetTrigger(Settings.isPickingRight);
        if (isPickingUp) animator.SetTrigger(Settings.isPickingUp);

        if (idleDown) animator.SetTrigger(Settings.idleDown);
        if (idleLeft) animator.SetTrigger(Settings.idleLeft);
        if (idleRight) animator.SetTrigger(Settings.idleRight);
        if (idleUp) animator.SetTrigger(Settings.idleUp);
    }


    //播放脚步声音
    private void AnimationEventPlayFootstepSound()
    {

    }
}

