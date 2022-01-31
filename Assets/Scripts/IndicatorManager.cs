using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorManager : MonoBehaviour
{
    [SerializeField] GameObject IndicatorDot;
    [SerializeField] float _dotSpacing = 0.1f;

    int _currentIndicatorIndex;

    List<SpriteRenderer> _dotRenderers = new List<SpriteRenderer>();

    public void Init(int numDots)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        float leftMostOffset = -(numDots - 1) / 2.0f * _dotSpacing;
        for (int i = 0; i < numDots; i++)
        {
            GameObject dot = Instantiate(IndicatorDot, transform.position, Quaternion.identity);
            SpriteRenderer dotRenderer = dot.GetComponent<SpriteRenderer>();
            dotRenderer.color = Color.gray;
            _dotRenderers.Add(dotRenderer);

            dot.transform.parent = this.transform;
            dot.transform.position = new Vector3(dot.transform.position.x + leftMostOffset + i * _dotSpacing, dot.transform.position.y, dot.transform.position.z);
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
