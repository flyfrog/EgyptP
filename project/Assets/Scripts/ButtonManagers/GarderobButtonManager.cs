using UnityEngine;
using UnityEngine.UI;

public class GarderobButtonManager : MonoBehaviour
{
    [SerializeField] private Button _closeBut;
    [SerializeField] private Button _openInventarBut;
    [SerializeField] private BaseWindow _garderob;
    [SerializeField] private BaseWindow _inventar;
    private void Awake()
    {
        _closeBut.onClick.AddListener(HideGarderobWindow);
        _openInventarBut.onClick.AddListener(ShowInvenotrbWindow);

    }


    private void HideGarderobWindow()
    {
        _garderob.Hide();
    }

    private void ShowInvenotrbWindow()
    {
        _inventar.Show();
    }

}