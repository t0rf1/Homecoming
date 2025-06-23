using UnityEngine;
using System;
using System.Collections;

public class MaterialFader : MonoBehaviour
{
    [Header("Docelowa przezroczystość (0 = całkowicie przezroczysty, 1 = pełna widoczność)")]
    public float fadedAlpha = 0f;

    [Header("Szybkość zanikania")]
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
            Debug.Log("FadeStart");

            foreach (var mat in objectRenderer.materials)
            {
                SetupMaterialForFade(mat);
            }
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

    private void SetupMaterialForFade(Material material)
    {
        if (material == null)
            return;

        // Ustaw render mode na transparent
        material.SetOverrideTag("RenderType", "Transparent");
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
    }

    private IEnumerator FadeOutObject(Renderer fadingObject, float fadedAlpha, float fadeSpeed, Action onFadeComplete = null)
    {
        string colorProperty = "_MainColor"; // Używaj dokładnie tej nazwy!

        if (fadingObject == null || fadingObject.materials.Length == 0)
            yield break;

        float time = 0f;

        // Zakładamy, że wszystkie materiały mają tę samą wartość alpha początkową
        float initialAlpha = fadingObject.materials[0].HasProperty(colorProperty)
            ? fadingObject.materials[0].GetColor(colorProperty).a
            : 1f;

        while (true)
        {
            bool anyMaterialUpdated = false;

            foreach (Material material in fadingObject.materials)
            {
                if (material.HasProperty(colorProperty))
                {
                    Color currentColor = material.GetColor(colorProperty);
                    float newAlpha = Mathf.Lerp(initialAlpha, fadedAlpha, time * fadeSpeed);
                    material.SetColor(colorProperty, new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha));
                    anyMaterialUpdated = true;
                }
            }

            if (!anyMaterialUpdated)
                yield break;

            if (Mathf.Abs(fadingObject.materials[0].GetColor(colorProperty).a - fadedAlpha) < 0.01f)
                break;

            time += Time.deltaTime;
            yield return null;
        }

        // Upewnij się, że końcowa alpha jest dokładna
        foreach (Material material in fadingObject.materials)
        {
            if (material.HasProperty(colorProperty))
            {
                Color finalColor = material.GetColor(colorProperty);
                material.SetColor(colorProperty, new Color(finalColor.r, finalColor.g, finalColor.b, fadedAlpha));
            }
        }

        onFadeComplete?.Invoke();
    }

}
