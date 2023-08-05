using MyBox;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class EnemySettingsController : MonoBehaviour
{
    [SerializeField] private Hero[] _heroes;

    private EnemySkillsMathService _enemySkillsMathService;

    [Inject]
    private void Construct(

        EnemySkillsMathService enemySkillsMathService
    )
    {
        _enemySkillsMathService = enemySkillsMathService;

    }

    public CharacterSettings GetCharacterSettings()
    {
        CharacterSettings settings = new();

        var hero = _heroes.GetRandom();

        settings.Hero = hero;
        settings.Skills = _enemySkillsMathService.MathResultEnemySkills();

        return settings;
    }
}