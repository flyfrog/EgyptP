using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class PlayerSettingsController : MonoBehaviour
{

    private HeroController _heroController;
    private HeroSkillsMathService _heroSkillsMathService;

    [Inject]
    private void Construct(
        HeroController heroController,
        HeroSkillsMathService heroSkillsMathService
        )
    {
        _heroController = heroController;
        _heroSkillsMathService = heroSkillsMathService;
    }

    public CharacterSettings GetCharacterSettings()
    {
        CharacterSettings settings = new();
        var hero = _heroController.CurentHero;

        settings.Hero = hero;
        settings.Skills = _heroSkillsMathService.MathResultHeroSkills(hero);

        return settings;
    }


}