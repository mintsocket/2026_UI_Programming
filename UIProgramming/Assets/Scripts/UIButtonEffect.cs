using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIButtonEffect : MonoBehaviour
{
    private Button myButton;
    private RectTransform buttonRect;
    [SerializeField] private float pressedScale = 0.9f;
    [SerializeField] private float duration = 0.1f;

    private Vector3 originalScale;

    public GameObject clickParticle;

    private void Awake()
    {
        clickParticle.SetActive(false);
        myButton = GetComponent<Button>();
        buttonRect = GetComponent<RectTransform>();
        originalScale = buttonRect.localScale;
        myButton.onClick.AddListener(PlayClickEffect);
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
        buttonRect.DOKill();
        buttonRect.localScale = originalScale;
        
        PlayParticleEffect();

        Sequence sequence = DOTween.Sequence();

        sequence.Append(
            buttonRect.DOScale(originalScale * pressedScale, duration)
        );

        sequence.Append(
            buttonRect.DOScale(originalScale, duration)
        );
    }

    public void PlayParticleEffect()
    {
        if(clickParticle == null)
            return;
        clickParticle.SetActive(false);
        clickParticle.transform.position = transform.position;
        clickParticle.SetActive(true);
    }
}

