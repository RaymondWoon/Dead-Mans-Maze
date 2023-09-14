using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WidthSliderScript : MonoBehaviour
{
    [SerializeField]
    private Slider _widthSlider;
    [SerializeField]
    private TextMeshProUGUI _widthSlidertext;

    //private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _widthSlider.value = UIManagerX._mazeWidth;
        _widthSlidertext.text = UIManagerX._mazeWidth.ToString();

        _widthSlider.onValueChanged.AddListener((v) =>
        {
            _widthSlidertext.text = v.ToString("0");

            UIManagerX._mazeWidth = (int)v;
        });
    }


}
