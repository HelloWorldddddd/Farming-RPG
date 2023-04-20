using System.Collections.Generic;
using UnityEngine;

public class AnimationOverrides : MonoBehaviour
{
    [SerializeField] private GameObject character = null;   //（在Inspector面板拖动）玩家游戏对象
    [SerializeField] private SO_AnimationTYpe[] so_AnimationTypeArray = null;

    private Dictionary<AnimationClip, SO_AnimationTYpe> animationTypeDictionaryByAnimation;
    private Dictionary<string, SO_AnimationTYpe> animationTypeDictionaryByCompositeAttributeKey;    //复合名称字典

    private void Start()
    {
        animationTypeDictionaryByAnimation = new Dictionary<AnimationClip, SO_AnimationTYpe>();
        
        foreach(SO_AnimationTYpe item in so_AnimationTypeArray)
        {
            animationTypeDictionaryByAnimation.Add(item.animationClip, item);
        }

        animationTypeDictionaryByCompositeAttributeKey = new Dictionary<string, SO_AnimationTYpe>();

        foreach (SO_AnimationTYpe item in so_AnimationTypeArray)
        {
            string key = item.characterPart.ToString() + item.partVariantColour.ToString() + item.partVariantType.ToString() + item.animationName.ToString();
            animationTypeDictionaryByCompositeAttributeKey.Add(key, item);
        }

    }


    //动画替换方法，参数为需要替换动画的部位列表
    public void ApplyCharacterCustomisationParameters(List<CharacterAttribute> characterAttributeList)
    {
        foreach(CharacterAttribute characterAttribute in characterAttributeList)
        {
            Animator currentAnimator = null;    //需要替换部位的动画控制器
            List<KeyValuePair<AnimationClip, AnimationClip>> animsKeyValuePairList = new List<KeyValuePair<AnimationClip, AnimationClip>>();    //动画替换键值对列表，第一个AnimationClip是原动画，第二个AnimationClip是替换动画
            string animatorSOAssetName = characterAttribute.characterPart.ToString();
            Animator[] animatorsArray = character.GetComponentsInChildren<Animator>();

            //找到场景对象的替换部位并引用
            foreach(Animator animator in animatorsArray)
            {
                //Debug.Log(animator.name);
                //Debug.Log(animatorSOAssetName);
                if (animatorSOAssetName == animator.name)
                {
                    currentAnimator = animator;
                    break;
                }
            }

            AnimatorOverrideController animatorOverrrideController = new AnimatorOverrideController(currentAnimator.runtimeAnimatorController);     //将场景对象部位运行时动画控制器赋值给临时动画覆盖控制器
            List<AnimationClip> animationsList = new List<AnimationClip>(animatorOverrrideController.animationClips);
            
            foreach(AnimationClip animationClip in animationsList)
            {
                SO_AnimationTYpe so_AnimationTYpe;      //原动画类
                bool foundAnimation = animationTypeDictionaryByAnimation.TryGetValue(animationClip, out so_AnimationTYpe);
                if (foundAnimation)
                {
                    
                    string key = characterAttribute.characterPart.ToString() + characterAttribute.partVariantColour.ToString()+characterAttribute.partVariantType.ToString()+so_AnimationTYpe.animationName.ToString();

                    SO_AnimationTYpe swapSO_AnimationType;      //替换动画类
                    bool foundSwapAnimation = animationTypeDictionaryByCompositeAttributeKey.TryGetValue(key, out swapSO_AnimationType);
                    if (foundSwapAnimation)
                    {
                        AnimationClip swapAnimationClip = swapSO_AnimationType.animationClip;       //替换动画
                        animsKeyValuePairList.Add(new KeyValuePair<AnimationClip, AnimationClip>(animationClip, swapAnimationClip));        //向替换动画列表中添加新的替换键值对
                    }
                }
            }
            animatorOverrrideController.ApplyOverrides(animsKeyValuePairList);
            currentAnimator.runtimeAnimatorController = animatorOverrrideController;
        }
    }
}

