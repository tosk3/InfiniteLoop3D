using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    //dependencies
    [SerializeField] private DayData_SO dayData_SO;

    //memebers
    [SerializeField] private float m_dayTimer;
    [SerializeField] private int m_dayCount;
    public event System.EventHandler<OnDayPassedArgs> OnDayPassed;
    public class OnDayPassedArgs : System.EventArgs
    {
        public int dayCount;
    }
    private void Start()
    {
        dayData_SO = (DayData_SO)Resources.Load(typeof(DayData_SO).ToString());
    }
    // Update is called once per frame
    void Update()
    {
        RunDayTimer();
    }

    private void RunDayTimer()
    {
        if (dayData_SO == null) return;

        if(m_dayTimer > dayData_SO.dayLength)
        {
            AddDay();
            OnDayPassed?.Invoke(this, new OnDayPassedArgs() { dayCount = m_dayCount });
            m_dayTimer = 0f;
        }
        else
        {
            m_dayTimer += Time.deltaTime;
        }
    }
    private void AddDay()
    {
        m_dayCount++;
        //spawnEvent
    }

    public float DayProgress()
    {
        return m_dayTimer / dayData_SO.dayLength;
    }
}
