using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
public class ButtonManager : AutoSingleton<ButtonManager>
{
    [SerializeField] private List<ButtonController> buttonControllers = new List<ButtonController>();

    public void Subscribe(ButtonController bc)
    {
        buttonControllers.Add(bc);
    }

    public void UnSubscribe(ButtonController bc)
    {
        buttonControllers.Remove(bc);
    }

    public ButtonController GetClosestButton(Transform spawnPoint)
    {
        float minDistance = Mathf.Infinity;
        ButtonController tempRoute = null;
        foreach (ButtonController rt in buttonControllers)
        {
            
            float distance = Vector3.Distance(spawnPoint.position, rt.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                tempRoute = rt;
            }
        }
        return tempRoute;
    }
}
