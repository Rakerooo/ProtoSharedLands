using UnityEngine;

public static class Utils
{
    public static bool IsMouseOverWindow(UnityEngine.Camera camera)
    {
        var view = camera.ScreenToViewportPoint(Input.mousePosition);
        return view.x is >= 0 and <= 1 && view.y is >= 0 and <= 1;
    }
}
