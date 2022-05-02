using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField] private List<Route> parentRoute = new List<Route>();
    [SerializeField] private List<Route> nextRoutes = new List<Route>();
    [SerializeField] private bool isEmpty;
    [SerializeField] private bool isLast;

    public bool IsEmpty
    {
        get => isEmpty;
        set => isEmpty = value;
    }

    public bool IsLast
    {
        get => isLast;
        set => isLast = value;
    }

    public Area Area { get; private set; }

    public List<Route> NextRoutes => nextRoutes;

    private void Awake()
    {
        RouteManager.Instance.Subscribe(this);
    }

    private void Start()
    {
        Area = GetComponent<Area>();
    }

    private void OnDestroy()
    {
        if(RouteManager.InstanceExists)
            RouteManager.Instance.UnSubscribe(this);
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        foreach (Route route in nextRoutes)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, route.transform.position);
        }
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 1f);
#endif
    }
}
