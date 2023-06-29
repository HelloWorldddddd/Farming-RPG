using System.Collections;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneControllerManager : Singleton<SceneControllerManager>
{
    private bool isFading = false;  //是否正在淡化
    [SerializeField] private float fadeDuration = 1f;   //淡化效果持续时间
    [SerializeField] private CanvasGroup faderCanvasGroup = null;    //Fader的Canvas Group组件
    [SerializeField] private Image faderImage = null;   //Fader的Image
    public SceneName startingSceneName;     //开始场景

    //游戏开始加载场景
    private IEnumerator Start()
    {
        faderImage.color = new Color(0f, 0f, 0f, 1f);
        faderCanvasGroup.alpha = 1f;

        yield return StartCoroutine(LoadSceneAndSetActive(startingSceneName.ToString()));

        EventHandler.CallAfterSceneLoadEvent();
        StartCoroutine(Fade(0f));
    }

    //调用场景主方法，sceneName是目标场景
    public void FadeAndLoadScene(string sceneName,Vector3 spawnPosition)
    {
        if (!isFading)
        {
            StartCoroutine(FadeAndSwitchScenes(sceneName, spawnPosition));
        }
    }

    //淡化并切换场景
    private IEnumerator FadeAndSwitchScenes(string sceneName, Vector3 spawnPosition)
    {
        //淡出
        EventHandler.CallBeforeSceneUnloadFadeOutEvent();
        yield return StartCoroutine(Fade(1f));

        Player.Instance.gameObject.transform.position = spawnPosition;
        EventHandler.CallBeforeSceneUnloadEvent();

        //异步卸载场景
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        //异步加载场景并设置为活动场景
        yield return StartCoroutine(LoadSceneAndSetActive(sceneName));
        EventHandler.CallAfterSceneLoadEvent();

        //淡入
        yield return StartCoroutine(Fade(0f));
        EventHandler.CallAfterSceneLoadFadeInEvent();
    }

    //淡入淡出效果
    private IEnumerator Fade(float finalAlpha)
    {
        isFading = true;
        faderCanvasGroup.blocksRaycasts = true;

        float fadeSpeed = (faderCanvasGroup.alpha - finalAlpha) / fadeDuration;

        while (!Mathf.Approximately(faderCanvasGroup.alpha, finalAlpha))
        {
            faderCanvasGroup.alpha -= fadeSpeed * Time.deltaTime;
            yield return null;
        }

        isFading = false;

        faderCanvasGroup.blocksRaycasts = false;
    }

    //加载场景并设置为活动场景
    private IEnumerator LoadSceneAndSetActive(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

        SceneManager.SetActiveScene(newlyLoadedScene);
    }
}
