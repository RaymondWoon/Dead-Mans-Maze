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
        _depthSlider.value = UIManagerX._mazeDepth;
        _depthSlidertext.text = UIManagerX._mazeDepth.ToString();

        _depthSlider.onValueChanged.AddListener((v) =>
        {
            _depthSlidertext.text = v.ToString("0");

            UIManagerX._mazeDepth = (int)v;
        });
    }
}
