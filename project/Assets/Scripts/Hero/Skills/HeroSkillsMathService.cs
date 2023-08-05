using Zenject;

public class HeroSkillsMathService
{

    private ItemController _itemController;


    [Inject]
    public HeroSkillsMathService(
        ItemController itemController
    )
    {
        _itemController = itemController;
    }



    public HeroSkill MathResultHeroSkills(Hero hero)
    {
        HeroSkill resultSkills = hero.Custom.BaseSkills;
        var itemSkills = MathSkillsForDressedItem(hero);


        //ме
        resultSkills.MoveSpeed += itemSkills.MoveSpeed;
        resultSkills.RotationSpeed += itemSkills.RotationSpeed;

        return resultSkills;
    }

    public HeroSkill MathSkillsForDressedItem(Hero hero)
    {
        HeroSkill itemSkills = new ();

        var heroItems = _itemController.GetItemsForHero(hero);
        foreach (var item in heroItems)
        {
            itemSkills.MoveSpeed += item.Skills.MoveSpeed;
            itemSkills.RotationSpeed += item.Skills.RotationSpeed;
        }
        return itemSkills;
    }


}