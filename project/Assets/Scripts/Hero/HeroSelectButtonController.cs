using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HeroSelectButtonController : MonoBehaviour
{
    [SerializeField] private Button _next;
    [SerializeField] private Button _prev;
    [SerializeField] private Animation _noMoreHeroesAnimation;

    private HeroController _heroController;

    [Inject]
    private void Construct(
        HeroController heroController
    )
    {
        _heroController = heroController;
    }


    private void Awake()
    {
        _next.onClick.AddListener(NextHero);
        _prev.onClick.AddListener(PrevHero);
    }

    private void NextHero()
    {
        if (_heroController.AvailibleHeroisCount == 1)
        {
            _noMoreHeroesAnimation.Play();
            return;
        }

        _heroController.NextHero();
    }

    private void PrevHero()
    {

        if (_heroController.AvailibleHeroisCount == 1)
        {
            _noMoreHeroesAnimation.Play();
            return;
        }

        _heroController.PrevHero();
    }

}