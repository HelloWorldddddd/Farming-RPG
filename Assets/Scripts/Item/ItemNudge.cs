using System;
using System.Collections;

using UnityEngine;

//玩家触碰效果
public class ItemNudge : MonoBehaviour
{
    //每次旋转间隔
    private WaitForSeconds pause;

    private bool isAnimating = false;

    private void Awake()
    {
        pause = new WaitForSeconds(0.04f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (false == isAnimating)
        {
            if (gameObject.transform.position.x < collision.gameObject.transform.position.x)
            {
                //玩家在左边，逆时针旋转
                StartCoroutine(RotateAntiClock());
            }
            else
            {
                //玩家在右边，顺时针旋转
                StartCoroutine(RotateClock());
            }
        }
    }

    private IEnumerator RotateAntiClock()
    {
        isAnimating = true;
        for(int i = 0; i < 4; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0f, 0f, 2f);
            yield return pause;
        }
        for(int i = 0; i < 5; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0f, 0f, -2f);
            yield return pause;
        }
        gameObject.transform.GetChild(0).Rotate(0f, 0f, 2f);
        yield return pause;
        isAnimating = false;
    }

    private IEnumerator RotateClock()
    {
        isAnimating = true;
        for (int i = 0; i < 4; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0f, 0f, -2f);
            yield return pause;
        }
        for (int i = 0; i < 5; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0f, 0f, 2f);
            yield return pause;
        }
        gameObject.transform.GetChild(0).Rotate(0f, 0f, -2f);
        yield return pause;
        isAnimating = false;
    }
}

