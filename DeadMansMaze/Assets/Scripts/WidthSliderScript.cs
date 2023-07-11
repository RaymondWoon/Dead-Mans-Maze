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
        _widthSlider.value = UIManager._mazeWidth;
        _widthSlidertext.text = UIManager._mazeWidth.ToString();

        _widthSlider.onValueChanged.AddListener((v) =>
        {
            _widthSlidertext.text = v.ToString("0");

            UIManager._mazeWidth = (int)v;
        });
    }


}
