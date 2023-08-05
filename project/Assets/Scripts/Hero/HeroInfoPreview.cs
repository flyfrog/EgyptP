using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Zenject;


public class HeroInfoPreview : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Slider _levelProgressSlider;
    [SerializeField] private TextMeshProUGUI _progressLevelText;
    [SerializeField] private ParticleSystem _FXSlider;

    private float _progressSpeed = 0.3f;
    private float _timeDelayBeforeDeactivateFx = 0.45f;
    private HeroController _heroController;
    private Coroutine _animationCoroutine;
    private float _curentSliderValue;
    private bool _permitForOnEnableRefresh;

    [Inject]
    private void Construct(
        HeroController heroController
    )
    {
        _heroController = heroController;
        _heroController.OnHeroChanged += HeroChanged;
        _heroController.OnLevelUp += UpdateLevelProgressForNewLevel;
        _heroController.OnUpdatedExpaCurrentHero += UpdateLevelProgress;
    }

    private void Start()
    {
        _permitForOnEnableRefresh = true;
        ForceRefresh(_heroController.CurentHero);
    }

    private void OnEnable()
    {
        if (!_permitForOnEnableRefresh)
        {
            return;
        }

        ForceRefresh(_heroController.CurentHero);
    }

    private void OnDisable()
    {
        StopAnimationCoroutine();
        DeactivateAnimationFX();
    }

    private void ForceRefresh(Hero hero)
    {
        _name.text = hero.Custom.Name;
        RedrawLevel(hero.LevelProgress.CurentLevel);
        RedrawExpaProgress(hero.LevelProgress);
    }

    private void HeroChanged(Hero hero)
    {
        if (!gameObject.activeSelf || !gameObject.activeInHierarchy)
        {
            return;
        }

        StopAnimationCoroutine();
        ForceRefresh(hero);
    }

    private void StopAnimationCoroutine()
    {
        if (_animationCoroutine != null)
        {
            StopCoroutine(_animationCoroutine);
            _animationCoroutine = null;
        }
    }

    private void StartAnimationCoroutine()
    {
        if (_animationCoroutine == null)
        {
            _animationCoroutine = StartCoroutine(AnimationRoutine());
        }
    }

    private void RedrawLevel(int level)
    {
        var lvl = level + 1;
        _levelText.text = lvl.ToString();
    }

    private void RedrawExpaProgress(LevelProgress progress)
    {
        DrawProgressBarExpaText(progress);
        _curentSliderValue = GetSliderValue(progress);
        SetSliderAndDraw(_curentSliderValue);
    }

    private void DrawProgressBarExpaText(LevelProgress progress)
    {
        var curentExpa = progress.CurentExpa;
        int expaForNextLevel = progress.ExpaForNextLevel;
        _progressLevelText.text = $"{curentExpa} / {expaForNextLevel}";
    }

    private void SetSliderAndDraw(float slidereValue)
    {
        _levelProgressSlider.value = slidereValue;
    }

    private float GetSliderValue(LevelProgress levelProgress)
    {
        float value = 0f;
        float sliderValuePerExpaPoint = 1f / levelProgress.ExpaForNextLevel;
        value = sliderValuePerExpaPoint * levelProgress.CurentExpa;
        return value;
    }

    private void UpdateLevelProgress(Hero hero)
    {
        if (!gameObject.activeSelf || !gameObject.activeInHierarchy)
        {
            return;
        }

        _targetSliderValue = GetSliderValue(_heroController.CurentHero.LevelProgress);
        DrawProgressBarExpaText(hero.LevelProgress);
        StartAnimationCoroutine();
    }

    private void UpdateLevelProgressForNewLevel(Hero hero)
    {
        if (!gameObject.activeSelf || !gameObject.activeInHierarchy)
        {
            return;
        }

        _curentSliderValue = 0f;
        _targetSliderValue = GetSliderValue(_heroController.CurentHero.LevelProgress);
        SetSliderAndDraw(_curentSliderValue);
        DrawProgressBarExpaText(hero.LevelProgress);
        RedrawLevel(hero.LevelProgress.CurentLevel);
        StartAnimationCoroutine();
    }

    private void ActivateAnimationFX()
    {
        if (_FXSlider == null)
        {
            return;
        }

        _FXSlider.Play();
    }

    private void DeactivateAnimationFX()
    {
        if (_FXSlider == null)
        {
            return;
        }


        _FXSlider.Stop();
    }

    private float _targetSliderValue;

    private IEnumerator AnimationRoutine()
    {
        ActivateAnimationFX();

        while (_curentSliderValue < _targetSliderValue)
        {
            _curentSliderValue += Time.deltaTime * _progressSpeed;
            SetSliderAndDraw(_curentSliderValue);
            yield return new WaitForNextFrameUnit();
        }


        yield return new WaitForSeconds(_timeDelayBeforeDeactivateFx);
        _animationCoroutine = null;
        DeactivateAnimationFX();
    }
}