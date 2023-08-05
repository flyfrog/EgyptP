using MyBox;
using TMPro;
using UnityEngine;
using Zenject;

public class GemUI : LocatorAgent
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private AnimationStateReference _addCoinAnimation;
    [SerializeField] private GameObject _Fxprefab;
    [SerializeField] private Transform _FxParent;
    [SerializeField] private Transform _FxSpawnPoint;


    private int _value;
    private WalletManager _walletManager;

    [Inject]
    private void Init(
        WalletManager walletManager
    )
    {
        _value = walletManager.Gem;
        DrawCoinValue();
    }

    public override void LocatorAction()
    {
        _addCoinAnimation.Play();
        Instantiate(_Fxprefab,_FxSpawnPoint.position , Quaternion.identity, _FxParent);
      //  _value += value;
        DrawCoinValue();
    }

    private void DrawCoinValue()
    {
        _text.text = _value.ToString();
    }

}