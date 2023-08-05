using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SelectTypeGameButtonManager : MonoBehaviour
{
    [SerializeField] private Button[] _close;
    [SerializeField] private Button[] _playClassicGame;
    [SerializeField] private BaseWindow _myWindow;

    private GameManager _gameManager;

    [Inject]
    private void Construct(
        GameManager gameManager
    )
    {
        _gameManager = gameManager;
    }

    private void Awake()
    {
        foreach (var but in _close)
        {
            but.onClick.AddListener(Close);
        }

        foreach (var but in _playClassicGame)
        {
            but.onClick.AddListener(OnClickPlayClassicGame);
        }
    }

    private void OnClickPlayClassicGame()
    {
        Close();
        //открыть окно с выбором режима игры
        _gameManager.StartClassicGame();
    }

    private void Close()
    {
        _myWindow.Hide();
    }
}