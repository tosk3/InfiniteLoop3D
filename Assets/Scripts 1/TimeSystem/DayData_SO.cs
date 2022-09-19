using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="systems/time/newDayData")]
public class DayData_SO : ScriptableObject
{
    public float dayLength;
    public Vector2 temporalStormGracePeriod;
    [Tooltip("Temporal Storm duration in days")]
    public int temporalStormDuration;

}
