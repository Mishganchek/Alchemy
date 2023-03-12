using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Grid : IEnumerable<Vector2>
{
    [SerializeField] private Vector2 _startPosition;
    [SerializeField] private Vector2 _size;
    [SerializeField] private Vector2 _step;

    public IEnumerator<Vector2> GetEnumerator() => Create(_step, _startPosition, _size).GetEnumerator();

    private IEnumerable<Vector2> Create(Vector2 step, Vector2 startPosition, Vector2 size)
    {
        for (int y = 0;  y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                yield return new Vector2(x * step.x, y * step.y) + startPosition;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}