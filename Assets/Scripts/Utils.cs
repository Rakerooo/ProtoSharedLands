using UnityEngine;

public static class Utils
{
    public static bool IsMouseOverWindow(Camera camera)
    {
        var view = camera.ScreenToViewportPoint(Input.mousePosition);
        return view.x is >= 0 and <= 1 && view.y is >= 0 and <= 1;
    }
    
    public static bool CompareVectors3(Vector3 v1, Vector3 v2)
    {
        return Mathf.Approximately(Vector3.Distance(v1, v2), 0f);
    }
    
    public static bool CompareQuaternions(Quaternion q1, Quaternion q2)
    {
        return Mathf.Approximately(Mathf.Abs(Quaternion.Dot(q1, q2)), 1f);
    }
}
