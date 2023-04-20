//所有动画名称
public enum AnimationName
{
    idleDown,
    idleUp,
    idleLeft,
    idleRight,
    walkUp,
    walkDown,
    walkLeft,
    walkRight,
    runUp,
    runDown,
    runLeft,
    runRight,
    useToolUp,
    useToolDown,
    useToolLeft,
    useToolRight,
    swingToolUp,
    swingToolDown,
    swingToolLeft,
    swingToolRight,
    liftToolUp,
    liftToolDwon,
    liftToolLeft,
    liftToolRight,
    holdToolUp,
    holdToolDown,
    holdToolLeft,
    holdToolRight,
    pickUp,
    pickDown,
    pickLeft,
    pickRight,
    count  //用来计数
}

//需要覆盖动画的角色身体部位
public enum CharacterPartAnimator
{
    Body,
    Arms,
    Hair,
    Tool,
    Hat,
    count
}

//需要改变的颜色
public enum PartVariantColour
{
    none,
    count
}

//覆盖动画的类型（携带、拿起工具等）
public enum PartVariantType
{
    none,
    carry,
    hoe,
    pickaxe,
    axe,
    scythe,
    wateringCan,
    count
}

public enum InventoryLocation
{
    player,
    chest,
    count  //用来计数
}

//工具效果
public enum ToolEffect
{
    none,
    watering
}

//角色移动方向
public enum Direction
{
    up,
    down,
    left,
    right,
    none 
}

//物品类型
public enum ItemType
{
    Seed,
    Commodity,
    Watering_tool,
    Hoeing_tool,
    Chopping_tool,
    Breaking_tool,
    Reaping_tool,
    Collecting_tool,
    Reapable_scenary,
    Furniture,
    none,
    count   //用来计数
}

//季节
public enum Season
{
    Spring,
    Summer,
    Autumn,
    Winter,
    none,
    count   //用来计数
}

public enum SceneName
{
    Scene1_Farm,
    Scene2_Field,
    Scene3_Cabin
}