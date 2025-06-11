using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IntroductionCutscene : MonoBehaviour
{
    [SerializeField] private Image[] _images; // Array of individual images
    [SerializeField] private float _lerpFadeSpeed = 1f; // Speed of fade
    [SerializeField] private Image _background; // Background image

    private int _currentImage = 0; // Index of the current image

    // Method to be called to transition to the next image
    public void NextImage()
    {
        if (_currentImage < _images.Length)
        {
            // Fade in the current image
            StartCoroutine(FadeInImage(_images[_currentImage]));

            // Fade out the previous image, if any
            if (_currentImage > 0)
            {
                StartCoroutine(FadeOutImage(_images[_currentImage - 1]));
            }

            _currentImage++; // Move to the next image
        }

        // Check if it's the last image, then fade out the background
        
    }

    public void Clear()
    {
        StartCoroutine(FadeOutImage(_images[_images.Length-1]));
        StartCoroutine(FadeOutImage(_background));
    }
    // Coroutine to fade in a single image
    private IEnumerator FadeInImage(Image image)
    {
        Color color = image.color;
        color.a = 0f;
        image.color = color;

        float progress = 0f;
        while (progress < 1f)
        {
            color.a = Mathf.Lerp(0f, 1f, progress);
            image.color = color;
            progress += Time.deltaTime * _lerpFadeSpeed;
            yield return null;
        }

        color.a = 1f;
        image.color = color;
    }

    // Coroutine to fade out a single image
    private IEnumerator FadeOutImage(Image image)
    {
        Color color = image.color;
        float progress = 0f;
        while (progress < 1f)
        {
            color.a = Mathf.Lerp(1f, 0f, progress);
            image.color = color;
            progress += Time.deltaTime * _lerpFadeSpeed;
            yield return null;
        }

        color.a = 0f;
        image.color = color;
    }
}
