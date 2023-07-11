using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DepthSliderScript : MonoBehaviour
{
    [SerializeField]
    private Slider _depthSlider;
    [SerializeField]
    private TextMeshProUGUI _depthSlidertext;

    // Start is called before the first frame update
    void Start()
    {
        _depthSlider.value = UIManager._mazeDepth;
        _depthSlidertext.text = UIManager._mazeDepth.ToString();

        _depthSlider.onValueChanged.AddListener((v) =>
        {
            _depthSlidertext.text = v.ToString("0");

            UIManager._mazeDepth = (int)v;
        });
    }
}
