using UnityEngine;
using System;
using System.Collections;

public class MaterialFader : MonoBehaviour
{
    [Tooltip("Docelowa przezroczystość (0 = całkowicie przezroczysty, 1 = pełna widoczność)")]
    public float fadedAlpha = 0f;

    [Tooltip("Szybkość zanikania")]
    public float fadeSpeed = 1f;

    private Renderer objectRenderer;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();

    }

    public void StartFade()
    {

        if (objectRenderer != null)
        {
            StartCoroutine(FadeOutObject(objectRenderer, fadedAlpha, fadeSpeed, () =>
            {
                Debug.Log("Fading completed!");
            }));
        }
        else
        {
            Debug.LogWarning("Brak komponentu Renderer!");
        }
    }

    private IEnumerator FadeOutObject(Renderer fadingObject, float fadedAlpha, float fadeSpeed, Action onFadeComplete = null)
    {
        if (fadingObject == null || fadingObject.materials.Length == 0)
            yield break;

        float time = 0f;
        float initialAlpha = fadingObject.materials[0].color.a;

        while (true)
        {
            bool anyMaterialUpdated = false;

            foreach (Material material in fadingObject.materials)
            {
                if (material.HasProperty("_Color"))
                {
                    Color currentColor = material.color;
                    float newAlpha = Mathf.Lerp(initialAlpha, fadedAlpha, time * fadeSpeed);
                    material.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
                    anyMaterialUpdated = true;
                }
            }

            if (!anyMaterialUpdated)
                yield break;

            if (Mathf.Abs(fadingObject.materials[0].color.a - fadedAlpha) < 0.01f)
                break;

            time += Time.deltaTime;
            yield return null;
        }

        foreach (Material material in fadingObject.materials)
        {
            if (material.HasProperty("_Color"))
            {
                Color finalColor = material.color;
                material.color = new Color(finalColor.r, finalColor.g, finalColor.b, fadedAlpha);
            }
        }

        onFadeComplete?.Invoke();
    }
}
