using System.Collections.Generic;
using UnityEngine;

public class LocatorManager
{
    private readonly Dictionary<LocatorType, ILocatorAgent> _agents = new();

    public void Register(ILocatorAgent locatorAgent)
    {
        if (_agents.ContainsKey(locatorAgent.LocatorType))
        {
            Debug.LogError($"Dublicat {locatorAgent.LocatorType}");
            return;
        }

        if (locatorAgent.LocatorType == LocatorType.None)
        {
            Debug.LogError($"Locator.None");
            return;
        }

        _agents.Add(locatorAgent.LocatorType, locatorAgent);
    }


    public ILocatorAgent GetLocatorAgent(LocatorType locatorType)
    {
        if (!_agents.TryGetValue(locatorType, out var agent))
        {
            Debug.LogError($"Locator {locatorType} not found");
            return null;
        }

        return agent;
    }
}