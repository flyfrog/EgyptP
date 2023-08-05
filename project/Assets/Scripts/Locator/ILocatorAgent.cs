using UnityEngine;

public interface ILocatorAgent
{
    public LocatorType LocatorType { get; }
    public Transform Target { get; }
    public void LocatorAction();
}