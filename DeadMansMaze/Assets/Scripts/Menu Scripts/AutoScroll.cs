using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AutoScroll : MonoBehaviour
{
    // Reference:
    // https://www.youtube.com/watch?v=9a6GyAbhLUk

    [SerializeField] private Text _textToScroll;
    [SerializeField] private bool _isLooping = false;

    // Scroll parameters
    private readonly float _speed = 75f;
    private readonly float _startTextPos = -1000f;
    private readonly float _endTextPos = 1000f;

    private RectTransform _rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = gameObject.GetComponent<RectTransform>();
        StartCoroutine(AutoScrollText());
    }

    IEnumerator AutoScrollText()
    {
        while (_rectTransform.localPosition.y < _endTextPos)
        {
            //Debug.Log(_rectTransform.localPosition.y);

            // Move text up at specified speed
            _rectTransform.Translate(_speed * Time.deltaTime * Vector3.up);

            // if text has reached the end position
            if (_rectTransform.localPosition.y > _endTextPos - 5.0f)
            {
                if (_isLooping)
                {
                    // reset local position y to bottom of screen
                    _rectTransform.localPosition = new Vector3(_rectTransform.localPosition.x, _startTextPos, _rectTransform.localPosition.z);
                }
                else
                {
                    // exit loop
                    break;
                }
            }

            yield return null;
        }
    }

}
