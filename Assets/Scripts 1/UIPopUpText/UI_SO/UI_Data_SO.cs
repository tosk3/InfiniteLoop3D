using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/UI_Data_SO")]
public class UI_Data_SO : ScriptableObject
{
    [Header("UI Animation Data")]
    public AnimationCurve entryCurve;
    public AnimationCurve exitCurve;
    public AnimationCurve bumpCurveUp;
    public AnimationCurve bumpCurveDown;
    public float uiAnimationSpeed;

    [Header("Popup Text Data")]
    public float disappearTimerLength;
    public float disappearingSpeed;
    public float moveUpSpeed;
    public Vector3 positionOffset;
    public Quaternion rotationOffset;
    public Color textPositiveColour;
    public Color textNegativeColour;

    [Header("Plant Progress Display Data")]
    public GameObject pf_progressDislay;
    public Vector3 progressSpawnOffset;
    public Color progressBarStartColor;
    public Color progressBarEndColor;
    public float progressSpeed;
    public float progressBarEndX;

}
