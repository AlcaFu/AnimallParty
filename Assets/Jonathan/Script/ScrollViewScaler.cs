using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;  // Make sure you have DOTween installed

public class ScrollViewScaler : MonoBehaviour
{
    public ScrollRect scrollRect;          // The ScrollRect component
    public RectTransform targetObject;     // The GameObject whose scale will change
    public float scaleAtTop = 0.2f;        // Scale when at the top of the ScrollView
    public float scaleWhenScrolled = 0.1f; // Scale when the ScrollView is scrolled down
    public float animationDuration = 0.3f; // Duration of the scaling animation

    private float currentScale;            // Track the current scale to avoid unnecessary scaling

    void Start()
    {
        // Set the initial scale based on the ScrollView's starting position
        SetScale(scaleAtTop);
    }

    void Update()
    {
        // Check if the scroll view is at the top (with a small tolerance for float precision)
        if (scrollRect.content.anchoredPosition.y < 0.0005f)
        {
            // If at the top and not already scaled to scaleAtTop, animate it to scaleAtTop
            if (Mathf.Abs(currentScale - scaleAtTop) > 0.0005f)
            {
                AnimateScale(scaleAtTop);
            }
        }
        else
        {
            // If scrolled down and not already scaled to scaleWhenScrolled, animate it to scaleWhenScrolled
            if (Mathf.Abs(currentScale - scaleWhenScrolled) > 0.0005f)
            {
                AnimateScale(scaleWhenScrolled);
            }
        }
    }

    // Method to animate the scaling of the target object
    private void AnimateScale(float targetScale)
    {
        targetObject.DOScale(targetScale, animationDuration).SetEase(Ease.InOutQuad);
        currentScale = targetScale;  // Update the current scale
    }

    // Method to set the scale instantly (used initially)
    private void SetScale(float scaleValue)
    {
        targetObject.localScale = new Vector3(scaleValue, scaleValue, 1);
        currentScale = scaleValue;  // Update the current scale
    }
}
