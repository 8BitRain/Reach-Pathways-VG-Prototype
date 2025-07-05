using UnityEngine;
using Yarn.Unity;

public enum TimeSlot { Morning, Lunchtime, Afternoon, SunSet, Evening }

public class TimeManager : MonoBehaviour
{
    TimeSlot currentTime;

    Calendar calendar;

    [SerializeField]
    private TimeUI timeUI;

    [SerializeField]
    private int startingMonth;

    public static TimeManager Instance;

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
        if (timeUI == null) { timeUI = GetComponent<TimeUI>(); }

        calendar = new Calendar(startingMonth);
        currentTime = TimeSlot.Morning;
        timeUI.SetTimeAndDate(calendar.currentMonth, calendar.currentDay, currentTime);
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

        timeUI.SetTimeAndDate(calendar.currentMonth, calendar.currentDay, currentTime);
    }
}

public class Calendar
{
    private int maxDays; //30, 31, and 28

    public int currentDay { get; private set; }
    public int currentMonth { get; private set; }

    public Calendar(int getMonth)
    {
        currentMonth = getMonth;

        SetMaxDaysForMonth(currentMonth);
        currentDay = 1;

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
