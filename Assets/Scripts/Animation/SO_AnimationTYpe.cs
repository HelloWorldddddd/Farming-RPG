using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="so_AnimationTYpe",menuName ="Scriptable Objects/Animation/Animation Type")]
public class SO_AnimationTYpe : ScriptableObject
{
    public AnimationClip animationClip;
    public AnimationName animationName;     //动画名称
    public CharacterPartAnimator characterPart;  //动画应用的身体部位
    public PartVariantColour partVariantColour;  //动画颜色变体
    public PartVariantType partVariantType;  //动画的类型变体（携带、拿起工具等）
}

