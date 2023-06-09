using TMPro;
using UnityEngine;

public class GameClock : MonoBehaviour
{
    //=========================================游戏时钟UI内容===========================================================//
    [SerializeField] private TextMeshProUGUI timeText = null;
    [SerializeField] private TextMeshProUGUI dateText = null;
    [SerializeField] private TextMeshProUGUI seasonText = null;
    [SerializeField] private TextMeshProUGUI yearText = null;
    //==================================================================================================================//

    private void OnEnable()
    {
        EventHandler.AdvanceGameMinuteEvent += UpdateGameTime;
    }

    private void OnDisable()
    {
        EventHandler.AdvanceGameMinuteEvent -= UpdateGameTime;
    }

    //更新游戏时钟UI显示
    private void UpdateGameTime(int gameYear, Season gameSeason, int gameDay, string gameDayOfWeek, int gameHour, int gameMinute, int gameSecond)
    {
        gameMinute = gameMinute - (gameMinute % 10);

        string ampm = "";
        string minute = "";

        if (gameHour >= 12)
        {
            ampm = "pm";
            if (gameHour >= 13)
            {
                gameHour -= 12;
            }
        }
        else
        {
            ampm = "am";
        }
        if (gameMinute < 10)
        {
            minute = "0" + gameMinute.ToString();
        }
        else
        {
            minute = gameMinute.ToString();
        }

        string time = gameHour.ToString() + " : " + minute + ampm;
        timeText.SetText(time);
        dateText.SetText(gameDayOfWeek + " : " + gameDay.ToString());
        seasonText.SetText(gameSeason.ToString());
        yearText.SetText("Year " + gameYear);
    }
}
