using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightCells : MonoBehaviour
{
    [SerializeField] private Sprite blinkingSprite;
    [SerializeField] private float blinkInterval = 0.5f;
    private Dictionary<SpriteRenderer, Coroutine> _blinkingCoroutines = new Dictionary<SpriteRenderer, Coroutine>();
    private Dictionary<SpriteRenderer, Sprite> _originalSprites = new Dictionary<SpriteRenderer, Sprite>();

    public void StartBlinking(SpriteRenderer spriteRenderer)
    {
        if (!_blinkingCoroutines.ContainsKey(spriteRenderer))
        {
            _originalSprites[spriteRenderer] = spriteRenderer.sprite;
            Coroutine coroutine = StartCoroutine(BlinkSprite(spriteRenderer));
            _blinkingCoroutines[spriteRenderer] = coroutine;
        }
    }

    public void StopBlinking(SpriteRenderer spriteRenderer)
    {
        if (_blinkingCoroutines.ContainsKey(spriteRenderer))
        {
            StopCoroutine(_blinkingCoroutines[spriteRenderer]);
            _blinkingCoroutines.Remove(spriteRenderer);

            // Revert the specific sprite to its original state
            if (_originalSprites.ContainsKey(spriteRenderer))
            {
                spriteRenderer.sprite = _originalSprites[spriteRenderer];
                _originalSprites.Remove(spriteRenderer);
            }
        }
    }

    private IEnumerator BlinkSprite(SpriteRenderer spriteRenderer)
    {
        bool isBlinking = false;
        while (true)
        {
            isBlinking = !isBlinking;
            spriteRenderer.sprite = isBlinking ? blinkingSprite : _originalSprites[spriteRenderer];
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
