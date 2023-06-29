using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShiningController : MonoBehaviour
{
    public float minAlphaOfPressAnyButton;  //开始键最小透明度
    public float maxAlphaOfPressAnyButton;  //开始键最大透明度
    public float blinkingTime;  //闪烁效果时间
    public CanvasGroup shiningCanvasGroup;
    public CanvasGroup pressAnyButtonCanvasGroup;
    public CanvasGroup blackCanvasGroup;
    public AudioSource startingAudio;   //开始游戏音效

    private bool isStart; //是否开始
    private float midAlpha;    //透明度中间量
    private bool inputLock=false; //输入锁

    void Start()
    {
        isStart = false;
        midAlpha = 1f;
        shiningCanvasGroup.alpha = 0f;
        pressAnyButtonCanvasGroup.alpha = maxAlphaOfPressAnyButton;
        blackCanvasGroup.alpha = 0f;
        StartCoroutine(Blinking(blinkingTime));
    }

    // Update is called once per frame
    void Update()
    {
        if (false == inputLock && Input.anyKeyDown)
        {
            inputLock = true;
            StopAllCoroutines();
            startingAudio.Play();
            shiningCanvasGroup.alpha = 0f;
            StartCoroutine(BeforeSwitchToStartingScene());
            StartCoroutine(DelaySwitchScenes("PersistentScene",3.5f));
        }
    }

    private IEnumerator BeforeSwitchToStartingScene()
    {
        yield return StartCoroutine(Blinking(3f,blackCanvasGroup,1f));
        yield return StartCoroutine(Blinking(1f,pressAnyButtonCanvasGroup,0f));
        yield return StartCoroutine(Blinking(1f, shiningCanvasGroup, 0.6f));
    }

    //targetCanvasGroup参数为null时字体闪烁特效循环
    private IEnumerator Blinking(float blinkingTime, CanvasGroup targetCanvasGroup =null, float targetAlpha=-1)
    {
        if (null == targetCanvasGroup && false == isStart)
        {
            float blinkingSpeed = (shiningCanvasGroup.alpha - midAlpha) / blinkingTime;
            while (!Mathf.Approximately(shiningCanvasGroup.alpha, midAlpha))
            {
                shiningCanvasGroup.alpha -= blinkingSpeed * Time.deltaTime;
                yield return null;
            }
            midAlpha = 1 - midAlpha;
            StartCoroutine(Blinking(1));
        }
        else if (-1 != targetAlpha && null != targetCanvasGroup)
        {
            float blinkingSpeed = (targetCanvasGroup.alpha - targetAlpha) / blinkingTime;
            while (!Mathf.Approximately(targetCanvasGroup.alpha, targetAlpha))
            {
                targetCanvasGroup.alpha -= blinkingSpeed * Time.deltaTime;
                yield return null;
            }
        }
    }

    //延迟调用切换场景函数
    IEnumerator DelaySwitchScenes(string targetSceneName,float waittingTime)
    {
        yield return new WaitForSeconds(waittingTime);
       SceneManager.LoadSceneAsync(targetSceneName);
    }
}
