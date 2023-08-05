using System;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class LobbyButtonManager:MonoBehaviour
{
    [SerializeField] private Button _playGameBut;
    [SerializeField] private Button _ruletkaOpenBut;
    [SerializeField] private Button _ruletkaCloseBut;
    [SerializeField] private Button _garderobBut;
    [SerializeField] private Button _shopBut;
    [SerializeField] private BaseWindow _selectTypeWindow;
    [SerializeField] private BaseWindow _ruletka;
    [SerializeField] private BaseWindow _garderob;
    [SerializeField] private BaseWindow _shop;

    private void Awake()
    {
        _playGameBut.onClick.AddListener(ShowSelectWindow);
        _ruletkaOpenBut.onClick.AddListener(ShowRuletkaWindow);
        _ruletkaCloseBut.onClick.AddListener(HideRuletkaWindow);
        _garderobBut.onClick.AddListener(ShowGarderobWindow);
        _shopBut.onClick.AddListener(ShowShopWindow);
    }

    private void ShowShopWindow()
    {
        _shop.Show();    }


    private void ShowSelectWindow()
    {
        //открыть окно с выбором режима игры
        _selectTypeWindow.Show();
    }


    private void ShowRuletkaWindow()
    {
        _ruletka.Show();
    }

    private void HideRuletkaWindow()
    {
        _ruletka.Hide();
    }

    private void ShowGarderobWindow()
    {
        _garderob.Show();
    }
}