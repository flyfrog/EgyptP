using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class HeroSkillsPreview : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _speed;
    [SerializeField] private TextMeshProUGUI _rotation;

    private HeroSkillsMathService _heroSkillsMathService;
    private HeroController _heroController;

    [Inject]
    public void  Construct(
        HeroSkillsMathService heroSkillsMathService,
        HeroController heroController
    )
    {
        _heroSkillsMathService = heroSkillsMathService;
        _heroController = heroController;

        _heroController.OnHeroChanged += DrawSkills;
    }

    private void Start()
    {
        DrawSkills(_heroController.CurentHero);
    }

    private void DrawSkills(Hero hero)
    {
        var resultSkills = _heroSkillsMathService.MathResultHeroSkills(hero);

        _speed.text = resultSkills.MoveSpeed.ToString();
        _rotation.text = resultSkills.RotationSpeed.ToString();

    }
}