using UnityEngine;
using Zenject;

public class HeroLevelProgressTester : MonoBehaviour
{

    private HeroController _heroController;

    [Inject]
    private void Construct(
        HeroController heroController
    )
    {
        _heroController = heroController;
    }

    [MyBox.ButtonMethod()]
    public void Add()
    {
        _heroController.AddExpaForCurrentHero(1);
    }

}