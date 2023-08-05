using System;
using UnityEngine;
using MyBox;
using UnityEngine.UI;

public class ItemUIView : MonoBehaviour
{
    [SerializeField] private Transform _newItemLabel;
    [field:SerializeField] public Image Icon { get; private set; }
    [field:SerializeField] public Image IconBackground { get; private set; }


    public void DestroyItSelf()
    {
        Destroy(gameObject);

    }

    public void SetViewedStatus(bool status)
    {
        if (status)
        {
            _newItemLabel.gameObject.SetActive(true);
            return;
        }

        _newItemLabel.gameObject.SetActive(false);
    }





}