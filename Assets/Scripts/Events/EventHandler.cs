using System;
using System.Collections.Generic;

//订阅-发布中间件,即委托
public static class EventHandler
{
    //声明委托
    public delegate void MovementDelegate(float inputX, float inputY, bool isWalking, bool isRunning, bool isIdle, bool isCarrying, ToolEffect toolEffect,
    bool isUsingToolRight, bool isUsingToolLeft, bool isUsingToolUp, bool isUsingToolDown,
    bool isLiftingToolRight, bool isLiftingToolLeft, bool isLiftingToolUp, bool isLiftingToolDown,
    bool isPickingRight, bool isPickingLeft, bool isPickingUp, bool isPickingDown,
    bool isSwingingToolRight, bool isSwingingToolLeft, bool isSwingingToolUp, bool isSwingingToolDown,
    bool idleUp, bool idleDown, bool idleLeft, bool idleRight);

    //==============================================创建委托=============================================================
    //角色移动事件
    public static event MovementDelegate MovementEvent;

    //背包更新事件(内置Action，委托类型)
    public static event Action<InventoryLocation, List<InventoryItem>> InventoryUpdateEvent;

    //游戏分钟更新事件
    public static event Action<int, Season, int, string, int, int, int> AdvanceGameMinuteEvent;
    //游戏小时更新事件
    public static event Action<int, Season, int, string, int, int, int> AdvanceGameHourEvent;
    //游戏日期更新事件
    public static event Action<int, Season, int, string, int, int, int> AdvanceGameDayEvent;
    //游戏季节更新事件
    public static event Action<int, Season, int, string, int, int, int> AdvanceGameSeasonEvent;
    //游戏年份更新事件
    public static event Action<int, Season, int, string, int, int, int> AdvanceGameYearEvent;

    //场景卸载前的淡出事件
    public static event Action BeforeSceneUnloadFadeOutEvent;
    //场景卸载前事件
    public static event Action BeforSceneUnloadEvent;
    //场景卸载后淡入事件
    public static event Action AfterSceneLoadFadeInEvent;
    //场景卸载后事件
    public static event Action AfterSceneLoadEvent;

    //===================================================================================================================

    //for发布者
    public static void CallMovementEvent(float inputX, float inputY, bool isWalking, bool isRunning, bool isIdle, bool isCarrying, ToolEffect toolEffect,
    bool isUsingToolRight, bool isUsingToolLeft, bool isUsingToolUp, bool isUsingToolDown,
    bool isLiftingToolRight, bool isLiftingToolLeft, bool isLiftingToolUp, bool isLiftingToolDown,
    bool isPickingRight, bool isPickingLeft, bool isPickingUp, bool isPickingDown,
    bool isSwingingToolRight, bool isSwingingToolLeft, bool isSwingingToolUp, bool isSwingingToolDown,
    bool idleUp, bool idleDown, bool idleLeft, bool idleRight)
    {
        MovementEvent?.Invoke(inputX, inputY, isWalking, isRunning, isIdle, isCarrying, toolEffect,isUsingToolRight, isUsingToolLeft, isUsingToolUp, isUsingToolDown,isLiftingToolRight, isLiftingToolLeft, isLiftingToolUp, isLiftingToolDown,isPickingRight, isPickingLeft, isPickingUp, isPickingDown,isSwingingToolRight, isSwingingToolLeft, isSwingingToolUp, isSwingingToolDown,idleUp, idleDown, idleLeft, idleRight);
    }

    public static void CallInventoryUpdateEvent(InventoryLocation inventoryLocation, List<InventoryItem> inventoryList)
    {
        InventoryUpdateEvent?.Invoke(inventoryLocation, inventoryList);
    }

    public static void CallAdvanceGameMinuteEvent(int gameYear,Season gameSeason,int gameDay,string gameDayOfWeek,int gameHour,int gameMinute,int gameSecond)
    {
        AdvanceGameMinuteEvent?.Invoke(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);
    }

    public static void CallAdvanceGameHourEvent(int gameYear, Season gameSeason, int gameDay, string gameDayOfWeek, int gameHour, int gameMinute, int gameSecond)
    {
        AdvanceGameHourEvent?.Invoke(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);
    }

    public static void CallAdvanceGameDayEvent(int gameYear, Season gameSeason, int gameDay, string gameDayOfWeek, int gameHour, int gameMinute, int gameSecond)
    {
        AdvanceGameDayEvent?.Invoke(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);
    }

    public static void CallAdvanceGameSeasonEvent(int gameYear, Season gameSeason, int gameDay, string gameDayOfWeek, int gameHour, int gameMinute, int gameSecond)
    {
        AdvanceGameSeasonEvent?.Invoke(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);
    }

    public static void CallAdvanceGameYearEvent(int gameYear, Season gameSeason, int gameDay, string gameDayOfWeek, int gameHour, int gameMinute, int gameSecond)
    {
        AdvanceGameYearEvent?.Invoke(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);
    }

    public static void CallBeforeSceneUnloadFadeOutEvent()
    {
        BeforeSceneUnloadFadeOutEvent?.Invoke();
    }

    public static void CallBeforeSceneUnloadEvent()
    {
        BeforSceneUnloadEvent?.Invoke();
    }

    public static void CallAfterSceneLoadFadeInEvent()
    {
        AfterSceneLoadFadeInEvent?.Invoke();
    }

    public static void CallAfterSceneLoadEvent()
    {
        AfterSceneLoadEvent?.Invoke();
    }
}
