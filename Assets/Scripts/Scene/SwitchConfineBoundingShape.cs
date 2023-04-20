
using UnityEngine;
using Cinemachine;
using System;

public class SwitchConfineBoundingShape : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SwitchBoundingShape();
    }

    //切换智能相机移动范围碰撞体（限定相机可视边界范围）
    private void SwitchBoundingShape()
    {
        //查找限定边界碰撞体
        PolygonCollider2D polygonCollider2D = GameObject.FindGameObjectWithTag(Tags.BoundsConfiner).GetComponent<PolygonCollider2D>();
        //获取智能相机组件
        CinemachineConfiner cinemachineConfiner = GetComponent<CinemachineConfiner>();

        cinemachineConfiner.m_BoundingShape2D = polygonCollider2D;
        //清除缓存（手册建议）
        cinemachineConfiner.InvalidatePathCache();

    }
}
