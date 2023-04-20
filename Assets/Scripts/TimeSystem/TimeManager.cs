using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{
    private int gameYear = 1;
    private Season gameSeason = Season.Spring;
    //private int gameMonth = 1;
    private int gameDay = 1;
    private int gameHour = 6;
    private int gameMinute = 30;
    private int gameSecond = 1;
    private string gameDayOfWeek = "Mon";
    private bool gameClockPaused = false;
    private float gameTick = 0f;
    private int gameTotalDays=1;

    private void Start()
    {
        EventHandler.CallAdvanceGameMinuteEvent(gameYear, gameSeason, gameDay, gameDayOfWeek,gameHour, gameMinute, gameSecond);
    }

    private void Update()
    {
        if (!gameClockPaused)
        {
            GameTick();
        }
    }

    //游戏时间流动方法
    private void GameTick()
    {
        gameTick += Time.deltaTime;
        if (gameTick >= Settings.secondsPerGameSecond)
        {
            gameTick -= Settings.secondsPerGameSecond;  //留下多出的部分用来下次递增
            UpdateGameTime();
        }
    }

    //游戏时间进位
    private void UpdateGameTime()
    {
        gameSecond++;

        if (gameSecond > 59)
        {
            gameSecond = 0;
            gameMinute++;

            if (gameMinute > 59)
            {
                gameMinute = 0;
                gameHour++;

                if (gameHour > 23)
                {
                    gameHour = 0;
                    gameDay++;
                    gameTotalDays++;

                    if (gameDay > 30)
                    {
                        gameDay = 1;
                        gameSeason++;
                        
                        if ((int)gameSeason > 3)
                        {
                            gameSeason = Season.Spring;
                            gameYear++;
                            if (gameYear > 9999)
                            {
                                gameYear = 1;
                            }

                            EventHandler.CallAdvanceGameYearEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);
                        }

                        EventHandler.CallAdvanceGameSeasonEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);
                    }
                    gameDayOfWeek = GetDayOfWeek();
                    EventHandler.CallAdvanceGameDayEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);
                }
                EventHandler.CallAdvanceGameHourEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);
            }
            EventHandler.CallAdvanceGameMinuteEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);
            //Debug.Log(" Game Year: " + gameYear + " Game Season: " + gameSeason + " Game Day: " + gameDay + " Game Day Of Week: " + gameDayOfWeek + " Game Hour: " + gameHour + " Game Minute: " + gameMinute);
        }
    }

    private string GetDayOfWeek()
    {
        int dayOfWeek = gameTotalDays % 7;
        switch (dayOfWeek)
        {
            case 1:
                return "Mon";
            case 2:
                return "Tue";
            case 3:
                return "Wed";
            case 4:
                return "Thu";
            case 5:
                return "Fri";
            case 6:
                return "Sat";
            case 0:
                return "Sun";
            default:return "";
        }
    }


    //以分钟进位测试时间
    public void TestAdvancedGameMinute()
    {
        for(int i = 0; i < 60; i++)
        {
            UpdateGameTime();
        }
    }

    //以天进位测试时间
    public void TestAdvancedGameDay()
    {
        for(int i = 0; i < 86400; i++)
        {
            UpdateGameTime();
        }
    }

}
