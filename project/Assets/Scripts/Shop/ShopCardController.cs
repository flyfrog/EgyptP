using UnityEngine;
using Zenject;

public class ShopCardController : MonoBehaviour
{
    [SerializeField] private Transform _buyHeroCard;

    private ShopManager _shopManager;

    [Inject]
    private void Construct(
        ShopManager shopManager
    )
    {
        _shopManager = shopManager;
    }

    private void OnEnable()
    {
        SetVisibilityForBuyHeroCard();
    }

    private void SetVisibilityForBuyHeroCard()
    {
        _buyHeroCard.gameObject.SetActive(_shopManager.HasHeroForSell());
    }

}