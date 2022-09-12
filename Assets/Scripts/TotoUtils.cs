using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TotoUtils
{

    #region World Mouse Position
    // Get Mouse Position in World with Z = 0f
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
    #endregion

    #region Text in World

    public const int sortingOrderDefault = 5000;
    /// <summary>
    /// Create text at worldPos
    /// </summary>
    /// <param name="text"></param>
    /// <param name="parent"></param>
    /// <param name="localPosition"></param>
    /// <param name="fontSize"></param>
    /// <param name="color"></param>
    /// <param name="textAnchor"></param>
    /// <param name="textAlignment"></param>
    /// <param name="sortingOrder"></param>
    /// <returns></returns>
    public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = sortingOrderDefault)
    {
        if (color == null) color = Color.white;
        return CreateWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
    }
    // Create Text in the World
    public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
    {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }

    public static TextMeshPro CreateWorldTextMesh(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TMPro.TextAlignmentOptions textAlignment = TMPro.TextAlignmentOptions.Left, int sortingOrder = sortingOrderDefault)
    {
        if (color == null) color = Color.white;
        return CreateWorldTextMesh(parent, text, localPosition, fontSize, (Color)color, textAlignment, sortingOrder);
    }
    // Create Text in the World
    public static TextMeshPro CreateWorldTextMesh(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TMPro.TextAlignmentOptions textAlignment, int sortingOrder)
    {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMeshPro));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMeshPro textMesh = gameObject.GetComponent<TextMeshPro>();
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }
    #endregion

    #region Random Vector3


    /// <summary>
    /// A random vector 3 between -1f and 1f. With a multiplier if needed.
    /// </summary>
    /// <param name="multiplier">Can be added to increase the amount. 1 for default.</param>
    /// <returns></returns>
    public static Vector3 RandomDirectionVector3(float multiplier)
    {
        return new Vector3(Random.Range(-1f, 1f) * multiplier, Random.Range(-1f, 1f) * multiplier, Random.Range(-1f, 1f) * multiplier);
    }


    /// <summary>
    /// A random vector 3 between -1f and 1f. With a multiplier if needed.
    /// </summary>
    /// <param name="multiplier">Can be added to increase the amount. 1 for default.</param>
    /// <param name="min">min value</param>
    /// <param name="max">max value</param>
    /// <returns></returns>
    public static Vector3 RandomDirectionVector3(float multiplier, float min, float max)
    {
        return new Vector3(Random.Range(min, max) * multiplier, Random.Range(min, max) * multiplier, Random.Range(min, max) * multiplier);
    }


    /// <summary>
    /// A random vector 3 between -1f and 1f. With a multiplier if needed.
    /// </summary>
    /// <param name="multiplier">Can be added to increase the amount. 1 for default.</param>
    /// <param name="ignoredAxis"> write X,Y,Z to choose an axis to be discarded</param>
    /// <returns></returns>
    public static Vector3 RandomDirectionVector3(float multiplier, char ignoredAxis)
    {
        switch (char.ToLower(ignoredAxis))
        {
            case 'x':
                return new Vector3(0, Random.Range(-1f, 1f) * multiplier, Random.Range(-1f, 1f) * multiplier);
            case 'y':
                return new Vector3(Random.Range(-1f, 1f) * multiplier, 0, Random.Range(-1f, 1f) * multiplier);
            case 'z':
                return new Vector3(Random.Range(-1f, 1f) * multiplier, Random.Range(-1f, 1f) * multiplier, 0);
            default:
                return Vector3.zero;
        }
    }


    /// <summary>
    /// A random vector 3 between -1f and 1f. With a multiplier if needed.
    /// </summary>
    /// <param name="multiplier">Can be added to increase the amount. 1 for default.</param>
    /// <param name="ignoredAxis"> write X,Y,Z to choose an axis to be discarded</param>
    /// <param name="min">min value</param>
    /// <param name="max">max value</param>
    /// <returns></returns>
    public static Vector3 RandomDirectionVector3(float multiplier, char ignoredAxis, float min, float max)
    {
        switch (char.ToLower(ignoredAxis))
        {
            case 'x':
                return new Vector3(0, Random.Range(min, max) * multiplier, Random.Range(min, max) * multiplier);
            case 'y':
                return new Vector3(Random.Range(min, max) * multiplier, 0, Random.Range(min, max) * multiplier);
            case 'z':
                return new Vector3(Random.Range(min, max) * multiplier, Random.Range(min, max) * multiplier, 0);
            default:
                return Vector3.zero;
        }
    }
    #endregion

    #region Simple Weight Calculations

    /// <summary>
    /// Gives a random entry of the weight table based on the given weights.
    /// </summary>
    /// <param name="weightTable"> A Vector 2 table of x - weight and y - value</param>
    /// <returns></returns>
    public static int RandomIntBasedOnWeight(List<Vector2> weightTable)
    {
        int sumOfWeights = 0;
        for (int i = 0; i < weightTable.Count; i++)
        {
            sumOfWeights += (int)weightTable[i].x; // calculate the weights
        }
        int randomWeight = UnityEngine.Random.Range(0, sumOfWeights); // chose a number for the weight
        int cumulativeWeight = 0;
        for (int i = 0; i < weightTable.Count; i++)
        {
            cumulativeWeight += (int)weightTable[i].x;

            if (cumulativeWeight < randomWeight)
            {
                return (int)weightTable[i].y;
            }
        }
        return (int)weightTable[0].y; // default happiness if something goes wrong;
    }

    public static int RandomIntBasedOnWeight(int[] weightTable, int defaultWeight)
    {
        int sumOfWeights = 0;
        for (int i = 0; i < weightTable.Length; i++)
        {
            sumOfWeights += (int)weightTable[i]; // calculate the weights
        }
        int randomWeight = UnityEngine.Random.Range(0, sumOfWeights);
        int cumulativeWeight = 0;
        for (int i = 0; i < weightTable.Length; i++)
        {
            cumulativeWeight += weightTable[i];

            if (cumulativeWeight > randomWeight)
            {
                return i;
            }
        }
        return defaultWeight; // default happiness if something goes wrong;
    }
    #endregion

    #region angle and other Math

    /// <summary>
    /// Returns a float angle between to points in space.
    /// </summary>
    /// <param name="position1">starting position</param>
    /// <param name="position2">target position</param>
    /// <returns></returns>
    public static float angleBetweenPoints(Vector2 position1, Vector2 position2)
    {
        var fromLine = position2 - position1;
        var toLine = new Vector2(1, 0);

        var angle = Vector2.Angle(fromLine, toLine);
        var cross = Vector3.Cross(fromLine, toLine);

        // did we wrap around?
        if (cross.z > 0)
            angle = 360f - angle;

        return angle;
    }

    #endregion

    public static bool RandomBool()
    {
        return Random.Range(0, 1 + 1) < 1 ? true : false;
    }
    public static bool RandomBoolBasedOnInt(int value)
    {
        return value < 1 ? false : true;
    }
    /// <summary>
    /// returns a random bool based on how likely it is out of a 100%
    /// </summary>
    /// <param name="percentageOutOf100">a value from 0-100</param>
    /// <returns></returns>
    public static bool RandomBoolBasedOnPercentage(int percentageOutOf100)
    {
        return percentageOutOf100 < UnityEngine.Random.Range(0, 100 + 1) ? false : true;
    }

}