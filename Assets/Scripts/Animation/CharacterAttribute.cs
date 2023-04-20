[System.Serializable]
public struct CharacterAttribute
{
    public CharacterPartAnimator characterPart;  //需要覆盖动画的角色身体部位
    public PartVariantColour partVariantColour;  //需要改变的颜色
    public PartVariantType partVariantType;  //覆盖动画的类型（携带、拿起工具等）

    public CharacterAttribute(CharacterPartAnimator characterPart, PartVariantColour partVariantColour, PartVariantType partVariantType)
    {
        this.characterPart = characterPart;
        this.partVariantColour = partVariantColour;
        this.partVariantType = partVariantType;
    }
}
