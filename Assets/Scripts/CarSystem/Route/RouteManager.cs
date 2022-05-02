using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class RouteManager : AutoSingleton<RouteManager>
{
    private List<Route> routes = new List<Route>();

    public void Subscribe(Route rt)
    {
        routes.Add(rt);
    }

    public void UnSubscribe(Route rt)
    {
        routes.Remove(rt);
    }

    public int GetRouteCount()
    {
        return routes.Count;
    }

    public Route GetClosestRoute(Transform carPosition)
    {
        float minDistance = Mathf.Infinity;
        Route tempRoute = null;
        foreach (Route rt in routes)
        {
            float distance = Vector3.Distance(carPosition.position, rt.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                tempRoute = rt;
            }
        }
        return tempRoute;
    }
}
