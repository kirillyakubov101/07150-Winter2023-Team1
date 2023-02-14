using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Utils
{
    //Creating Text in the World *Grid Map
    public static TextMesh CreateWorldText(string text, Transform parent, Vector3 localPosition, int fontsize, Color color, TextAnchor textAnchor, TextAlignment textAlignment)
    {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontsize;
        textMesh.color = color;
        return textMesh;
    }

    //Get Mouse World Position
    public static Vector3 GetMouseWorldPosition()
    {
        return GetMouseWorldPosition(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPosition(Camera worldCamera)
    {
        return GetMouseWorldPosition(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPosition(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

    //Function to transform a Vector3 into a EulerAngle
    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

}
