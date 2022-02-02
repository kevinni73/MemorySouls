using System.Collections.Generic;
using UnityEngine;

public class IndicatorManager : MonoBehaviour
{
    [SerializeField] GameObject IndicatorDot;

    const float kDotSpacing = 0.2f;
    List<SpriteRenderer> _dotRenderers;
    int _currentIndicatorIndex;

    public void Init(int size)
    {
        foreach (Transform child in transform)
        {
            // start fresh in case number of indicators changes
            Destroy(child.gameObject);
        }

        _dotRenderers = new List<SpriteRenderer>();

        float leftMostOffset = -(size - 1) / 2.0f * kDotSpacing;
        for (int i = 0; i < size; i++)
        {
            GameObject dot = Instantiate(IndicatorDot, transform.position, Quaternion.identity);
            SpriteRenderer dotRenderer = dot.GetComponent<SpriteRenderer>();
            dotRenderer.color = Color.gray;
            _dotRenderers.Add(dotRenderer);

            dot.transform.parent = this.transform;
            dot.transform.position = new Vector3(dot.transform.position.x + leftMostOffset + i * kDotSpacing, dot.transform.position.y, dot.transform.position.z);
        }

        // recover previous state of indicators
        int lastIndex = _currentIndicatorIndex;
        _currentIndicatorIndex = 0;
        while (_currentIndicatorIndex != lastIndex)
        {
            Increment();
        }
    }

    public void Increment()
    {
        if (_currentIndicatorIndex < _dotRenderers.Count)
        {
            _dotRenderers[_currentIndicatorIndex].color = Color.white;
            _currentIndicatorIndex++;
        }
    }

    public void Clear()
    {
        _currentIndicatorIndex = 0;
        foreach (SpriteRenderer renderer in _dotRenderers)
        {
            renderer.color = Color.gray;
        }
    }

    public void Incorrect()
    {
        for (int i = 0; i <= _currentIndicatorIndex; i++)
        {
            _dotRenderers[i].color = Color.red;
        }
    }

    public void Enable()
    {
        foreach (SpriteRenderer renderer in _dotRenderers)
        {
            renderer.enabled = true;
        }
    }

    public void Disable()
    {
        foreach (SpriteRenderer renderer in _dotRenderers)
        {
            renderer.enabled = false;
        }
    }
}
