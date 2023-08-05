using MyBox;
using UnityEngine;

public class BaseWindow : MonoBehaviour
{
    [SerializeField] private AnimationStateReference _show;
    [SerializeField] private AnimationStateReference _hide;

    /// <summary>
    /// Вызывается из анимации
    /// </summary>
    public void EndShowAnimation()
    {

    }

    /// <summary>
    /// Вызывается из анимации
    /// </summary>
    public void EndHideAnimation()
    {
        _hide.Animator.gameObject.SetActive(false);
    }

    public void Show()
    {
        _show.Animator.gameObject.SetActive(true);
        _show.Play();
        _show.Animator.Update(0);
    }


    public void Hide()
    {
        _hide.Play();
        _hide.Animator.Update(0);
    }




}