using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static Color RandomColor()
    {
        return new Color(
            Random.Range(0f, 1f), 
            Random.Range(0f, 1f), 
            Random.Range(0f, 1f),
            1f);
    }

    public static GameObject Nearest(Vector2 point, List<GameObject> objects)
    {
        GameObject nearest = null;
        float nearestDistance = 0;
        foreach (GameObject obj in objects)
        {
            float distance = Vector2.Distance(point, obj.transform.position);
            if (nearest == null || distance < nearestDistance)
            {
                nearestDistance = distance;
                nearest = obj;
            }
        }
        return nearest;
    }
}
