using UnityEngine;
using Yarn.Unity;

public enum TimeSlot { Morning, Lunchtime, Afternoon, SunSet, Evening}

public class TimeManager : MonoBehaviour
{
    TimeSlot currentTime;
    
    Calendar calendar;

    [SerializeField]
    private TimeUI timeUI;

    [SerializeField]
    private int startingMonth;

    void Start()
    {
        if(timeUI == null) { timeUI = GetComponent<TimeUI>(); }

        calendar = new Calendar(startingMonth);
        currentTime = TimeSlot.Morning;
        timeUI.SetTimeAndDate(calendar.currentMonth, calendar.currentDay, currentTime);
    }

    //Handles the time change based on the parameter increments
    public void AdvanceTimeBySlots(int num)
    {
        int timeIndex = (int)currentTime;

        while(num > 0)
        {
            if(timeIndex == System.Enum.GetValues(typeof(TimeSlot)).Length - 1)
            {
                timeIndex = 0;
            }
            else
            {
                timeIndex++;
            }

            
            num--;

            currentTime = (TimeSlot)timeIndex;
            if(currentTime == TimeSlot.Morning)
            {
                calendar.ChangeDate();
            }
        }

        currentTime = (TimeSlot)timeIndex;

        timeUI.SetTimeAndDate(calendar.currentMonth, calendar.currentDay, currentTime);
    }

    [YarnCommand("AdvanceTimeSlot")]
    public void YarnAdvanceTime(int num)
    {
        /*
         * 1 slot = rest
         * 2 slots = Skill-based activity
         * 2 slots = confidant interaction
         * 5 slots = Scenario (card game)
         * 5 slots = major confidant event
         */
        AdvanceTimeBySlots(num);
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
        if(currentDay == maxDays)
        {
            currentDay = 1;
            if(currentMonth == 12)
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
            //31 days
            case 1:
            case 3:
            case 5:
            case 7:
            case 8:
            case 10:
            case 12:
                maxDays = 31;
                break;
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
        }
    }
}