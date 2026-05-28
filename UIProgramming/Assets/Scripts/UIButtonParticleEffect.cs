using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(RectTransform))]
public class UIButtonParticleEffect : MonoBehaviour
{
    private Button myButton;
    private RectTransform buttonRect;
    private ParticleSystem clickParticle;

    [Header("Tween")]
    [SerializeField] private float pressedScale = 0.9f;
    [SerializeField] private float duration = 0.1f;

    private Vector3 originalScale;

    private void Awake()
    {
        myButton = GetComponent<Button>();
        buttonRect = GetComponent<RectTransform>();
        clickParticle = GetComponentInChildren<ParticleSystem>(true);

        originalScale = buttonRect.localScale;

        myButton.onClick.AddListener(PlayClickEffect);

        if (clickParticle != null)
        {
            clickParticle.Stop(
                true,
                ParticleSystemStopBehavior.StopEmittingAndClear
            );
        }
    }

    private void OnDestroy()
    {
        if (myButton != null)
        {
            myButton.onClick.RemoveListener(PlayClickEffect);
        }
    }

    public void PlayClickEffect()
    {
        PlayTween();
        PlayParticle();
    }

    private void PlayTween()
    {
        buttonRect.DOKill();
        buttonRect.localScale = originalScale;

        Sequence sequence = DOTween.Sequence();

        sequence.Append(
            buttonRect.DOScale(originalScale * pressedScale, duration)
        );

        sequence.Append(
            buttonRect.DOScale(originalScale, duration)
        );
    }

    private void PlayParticle()
    {
        if (clickParticle == null)
            return;

        clickParticle.Stop(
            true,
            ParticleSystemStopBehavior.StopEmittingAndClear
        );

        clickParticle.Play();
    }
}