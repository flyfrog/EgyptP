using UnityEngine;

public class TrailService
{
    private TrailRenderer _trailRenderer;
    private bool _trailActive;


    public TrailService(
        TrailRenderer trailRenderer,
        CharacterSettings characterSettings
    )
    {
        _trailRenderer = trailRenderer;

        _trailRenderer.startColor = characterSettings.Hero.Custom.TrailColorGradient.StartColor;
        _trailRenderer.endColor = characterSettings.Hero.Custom.TrailColorGradient.EndColor;

        _trailRenderer.time = 0;
    }


    public void Update(bool isExploring)
    {
        if (isExploring && _trailActive == false)
        {
            _trailRenderer.time = 999;
            _trailActive = true;
        }

        if (!isExploring && _trailActive == true)
        {
            _trailRenderer.time = 0;
            _trailActive = false;
        }
    }
}