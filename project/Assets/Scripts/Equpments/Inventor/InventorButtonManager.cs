using UnityEngine;
using UnityEngine.UI;

public class InventorButtonManager : MonoBehaviour
{
    [SerializeField] private Button _closeBut;

    [SerializeField] private BaseWindow _inventar;
    private void Awake()
    {
        _closeBut.onClick.AddListener(HideInventorWindow);

    }


    private void HideInventorWindow()
    {
        _inventar.Hide();
    }



}