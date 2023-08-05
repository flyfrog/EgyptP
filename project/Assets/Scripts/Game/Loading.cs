using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{

    public event Action OnLoadingEnded;
    [SerializeField] private Transform _loadingWindow;
    [SerializeField] private float _loadingTime = 1f;
    [SerializeField] private Slider _slider;

    private float _value;

    public void StartLoading()
    {
        _loadingWindow.gameObject.SetActive(true);
        StartCoroutine(LoadingUpdate());

    }

    private IEnumerator LoadingUpdate()
    {
        while (_value<_loadingTime)
        {
            _value += Time.deltaTime;

            Redraw();
            yield return new WaitForNextFrameUnit();
        }

        EndLoading();
    }

    private void Redraw()
    {
        _slider.value = Mathf.InverseLerp(0f, _loadingTime, _value);
    }

    private void EndLoading()
    {
        _value = 0;
        Redraw();
        _loadingWindow.gameObject.SetActive(false);
        OnLoadingEnded?.Invoke();
    }

}