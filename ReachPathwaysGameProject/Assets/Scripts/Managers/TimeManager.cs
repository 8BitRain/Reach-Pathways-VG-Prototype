using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public enum TimeSlot { Morning, Afternoon, Evening, Night, Midnight }

public class TimeManager : MonoBehaviour
{
    TimeSlot currentTime;

    Calendar calendar;

    [SerializeField]
    private TimeUI timeUI;

    [SerializeField]
    private int startingMonth;

    public static TimeManager Instance;

    [SerializeField]
    private GameObject UICanvas;

    [SerializeField]
    private bool resetTime, canRest;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
    }

    void Start()
    {
        if(resetTime)
        {
            ResetTimeData();
        }

        if( UICanvas == null) { UICanvas = transform.GetChild(0).gameObject; }
        if (timeUI == null) { timeUI = GetComponent<TimeUI>(); }

        int temp = 0;
        LoadTimeData(ref temp);
        calendar = new Calendar(temp);

        timeUI.SetTimeAndDate(calendar.currentMonth, calendar.currentDay, currentTime, calendar.currentWeekday);

        UICanvas.SetActive(false);

        canRest = true;
    }

    private void OnDestroy()
    {
        if (resetTime)
        {
            ResetTimeData();
        }
        SaveTimeData();
    }

    //Handles the time change based on the parameter increments
    //Call this within a scene that isn't being handles through Yarnspinner
    public void AdvanceTimeBySlots(int num)
    {
        int timeIndex = (int)currentTime;

        while (num > 0)
        {
            if (timeIndex == System.Enum.GetValues(typeof(TimeSlot)).Length - 1)
            {
                timeIndex = 0;
                if(canRest)
                {
                    Stress.Instance.IncreaseStress();
                    //TODO: if stress is max, skip to next day and reset stress to 0
                }
                else
                {
                    canRest = true;
                }
                
                timeUI.SetButtonCondition(true);
            }
            else
            {
                timeIndex++;
            }
            num--;

            currentTime = (TimeSlot)timeIndex;
            if (currentTime == TimeSlot.Morning)
            {
                calendar.ChangeDate();
            }
        }

        currentTime = (TimeSlot)timeIndex;

        timeUI.SetTimeAndDate(calendar.currentMonth, calendar.currentDay, currentTime, calendar.currentWeekday);
    }

    public void Rest()
    {
        if(canRest)
        {
            AdvanceTimeBySlots(1);
            Stress.Instance.DescreaseStress();
            timeUI.SetButtonCondition(false);
            canRest = false;
        }
        
    }

    public void TimeCanvasDisplay(bool condition)
    {
        StartCoroutine(WaitTimer(condition));
    }

    private IEnumerator WaitTimer(bool condition)
    {
        yield return new WaitForSeconds(FadeTransition.Instance.fadeTimer);
        UICanvas.SetActive(condition);
    }

    public TimeSlot GetTime()
    {
        return currentTime;
    }    

    public string GetWeekday()
    {
        return calendar.currentWeekday;
    }

    //Updating data
    private void SaveTimeData()
    {
        TimeData timeData = new TimeData(calendar.currentMonth, calendar.currentDay, currentTime, calendar.currentWeekday);
        string jsonData = JsonUtility.ToJson(timeData);
        string timeKey = "TimeDataFile";

        PlayerPrefs.SetString(timeKey, jsonData);
        PlayerPrefs.Save();
    }

    private void LoadTimeData(ref int getMonth)
    {
        string timeKey = "TimeDataFile";
        if (PlayerPrefs.HasKey(timeKey))
        {
            string jsonData = PlayerPrefs.GetString(timeKey);
            TimeData timeData = JsonUtility.FromJson<TimeData>(jsonData);

            calendar = new Calendar(timeData.monthNum);

            calendar.currentMonth = timeData.monthNum;
            calendar.currentDay = timeData.dayNum;
            currentTime = timeData.timeName;

            getMonth = timeData.monthNum;
        }
        else
        {
            calendar = new Calendar(startingMonth);
            currentTime = TimeSlot.Morning;
            getMonth = startingMonth;
        }
    }

    private void ResetTimeData()
    {
        calendar = new Calendar(startingMonth);
        currentTime = TimeSlot.Morning;

        SaveTimeData();
    }

    private void OnApplicationQuit()
    {
        SaveTimeData();
    }

}

public class Calendar
{
    private int maxDays; //30, 31, and 28

    public int currentDay { get; set; }
    public int currentMonth { get; set; }

    string[] week = new string[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"  };
    
    public string currentWeekday { get; set; }

    private int position;

    public Calendar(int getMonth)
    {
        currentMonth = getMonth;

        SetMaxDaysForMonth(currentMonth);
        currentDay = 1;
        position = 0;
        currentWeekday = week[position];
    }

    public void ChangeDate()
    {
        if (currentDay == maxDays)
        {
            currentDay = 1;
            
            if (currentMonth == 12)
            {
                currentMonth = 1;
            }
            else
            {
                currentMonth++;
            }
            SetMaxDaysForMonth(currentMonth);
        }
        else
        {
            currentDay++;
        }

        ChangeWeekDay();
    }

    private void ChangeWeekDay()
    {
        position = (position + 1) % week.Length;

        currentWeekday = week[position];
    }

    private void SetMaxDaysForMonth(int getMonth)
    {
        switch (getMonth)
        {
            //30 days
            case 4:
            case 6:
            case 9:
            case 11:
                maxDays = 30;
                break;
            //28 days
            case 2:
                maxDays = 28;
                break;
            //31 days
            default:
                maxDays = 31;
                break;
        }
    }
}

[System.Serializable]
public class TimeData
{
    public int monthNum;
    public int dayNum;
    public TimeSlot timeName;
    public string weekDay;

    public TimeData(int month, int day, TimeSlot time, string week)
    {
        monthNum = month;
        dayNum = day;
        timeName = time;
        weekDay = week;
    }
}