

using UnityEngine;

public static class MouseUtills
{
    public static Vector3 GetMousePositionInTheWorld(Camera camera)
    {
        if (GetPosition(camera, out var point))
        {
            return point;
        }

        return Vector3.zero;
    }

    public static Vector3 GetMousePositionInTheWorld(Camera camera, float y)
    {
        if (GetPosition(camera, out var point))
        {
            return new Vector3(point.x, y, point.z);
        }

        return Vector3.zero;
    }

    private static bool GetPosition(Camera camera, out Vector3 point)
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            point = ray.GetPoint(rayDistance);

            Debug.DrawLine(ray.origin, point, Color.red);

            return true;
        }

        point = Vector3.zero;
        return false;
    }
}