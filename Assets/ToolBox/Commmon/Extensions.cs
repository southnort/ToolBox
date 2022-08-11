using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class Extensions
{
    public static Vector2 ToVector2(this Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.z);
    }
    public static Vector2 VectorDeviation(this Vector2 vector, float angle)
    {
        var radAngle = angle * Math.PI / 180;

        var x = (float)(vector.x * Math.Cos(radAngle) - vector.y * Math.Sin(radAngle));
        var y = (float)(vector.y * Math.Cos(radAngle) + vector.x * Math.Sin(radAngle));

        return new Vector2(x, y);
    }

    public static Vector2 VectorDeviation(this Vector2 vector, float cos, float sin)
    {
        var x = (vector.x * cos - vector.y * sin);
        var y = (vector.y * cos + vector.x * sin);

        return new Vector2(x, y);
    }

    public static float Remap(float iMin, float iMax, float oMin, float oMax, float value)
    {
        var t = Mathf.InverseLerp(iMin, iMax, value);
        return Mathf.Lerp(oMin, oMax, t);
    }


    public static Vector3 GetRandomCoordinatesAroundPoint(this Vector3 originalPoint, float radius,
            bool pointOnRadiusLine = false)
    {
        /// <summary>
        /// pointOnRadius - if false, return random point on radius 0-radius;
        /// вернуть точку на расстоянии от 0 до radius
        /// if true, return point on concrete radius
        /// </summary>

        float angle = UnityEngine.Random.Range(0, 360);
        float lenght = pointOnRadiusLine ? radius :
            (UnityEngine.Random.Range(0, radius));

        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * lenght;
        float z = Mathf.Sin(angle * Mathf.Deg2Rad) * lenght;


        Vector3 result = new Vector3(originalPoint.x + x, originalPoint.y, originalPoint.z + z);


        return result;

    }


    public static Vector3 GetRandomPointInsideRectangle(this Vector3 originalPoint, float x, float y)
    {
        var pX = UnityEngine.Random.Range(-x, x);
        var pY = UnityEngine.Random.Range(-y, y);

        return originalPoint + new Vector3(pX, pY, 0);
    }


    public static void ClearChildren(this Transform transform)
    {
        var count = transform.childCount;
        if (count > 0)
        {
            for (int i = count - 1; i >= 0; i--)
            {
                var child = transform.GetChild(i);
                child.SetParent(null);
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    public static void SetLayer(this Transform transform, string layerName, bool recursiveChangeChildren)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            child.gameObject.layer = LayerMask.NameToLayer(layerName);

            if (recursiveChangeChildren)
                child.SetLayer(layerName, recursiveChangeChildren);
        }
    }


    public static void SetLayer(this Transform transform, int layerIndex, bool recursiveChangeChildren)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            child.gameObject.layer = layerIndex;

            if (recursiveChangeChildren)
                child.SetLayer(layerIndex, recursiveChangeChildren);
        }
    }


    public static string ToIntString(this float value)
    {
        return ((int)value).ToString();
    }

    public static string ToDotString(this float value)
    {
        var str = value.ToString();
        return str.Replace(",", ".");
    }

    public static string ToUIntString(this float value)
    {
        var abs = Math.Abs((int)value);
        if (abs < 1) abs = 1;

        return abs.ToString();
    }

    public static string ToShortTimeString(this float timeValue)
    {
        var time = (int)timeValue + 1;

        var seconds = time % 60f;
        var minutes = time / 60;
        var hours = minutes / 60;
        minutes = minutes % 60;

        if (hours > 0) return hours.ToString("00") + "h ";
        if (minutes > 0) return minutes.ToString("00") + "m ";
        return seconds.ToString("00") + "s";
    }

    public static string ToShortMoneyString(this int value)
    {
        return ((float)value).ToShortMoneyString();
    }

    public static string ToShortMoneyString(this float value)
    {
        string[] prefix = { string.Empty, "K", "M", "B" };
        var absnum = Math.Abs(value);
        int add;
        if (absnum < 1)
        {
            add = (int)Math.Floor(Math.Floor(Math.Log10(absnum)) / 3);
        }
        else
        {
            add = (int)(Math.Floor(Math.Log10(absnum)) / 3);
        }

        var shortNumber = value / Math.Pow(10, add * 3);

        return string.Format("{0}{1}", shortNumber.ToString("0.#"), prefix[add]);
    }


    public static T GetRandomItem<T>(this List<T> list)
    {
        if (list.Count < 1) return default(T);

        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    public static T GetRandomItem<T>(this T[] list)
    {
        if (list.Length < 1) return default(T);

        return list[UnityEngine.Random.Range(0, list.Length)];
    }

    public static T GetLast<T>(this List<T> list)
    {
        if (list.Count < 1) return default(T);

        return list[list.Count - 1];
    }

    public static float CalculateAngle(Vector2 firstPoint, Vector2 secondPoint)
    {
        if (firstPoint == secondPoint) return 0;

        else
        {
            Vector2 curPos = firstPoint - secondPoint;
            float cos = curPos.y / Mathf.Sqrt((curPos.x * curPos.x + curPos.y * curPos.y));
            float angle = Mathf.Acos(cos) * Mathf.Rad2Deg;
            if (curPos.x > 0) angle *= -1;

            return angle + 180;
        }
    }

    public static float CalculateAngle(Vector2 secondPoint)
    {
        return CalculateAngle(Vector2.zero, secondPoint);
    }

    public static bool IsA<T>(this object obj)
    {
        return obj is T;
    }

}

#if UNITY_EDITOR
public class ReadOnlyAttribute : PropertyAttribute
{

}

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property,
                                            GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position,
                               SerializedProperty property,
                               GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}
#endif
