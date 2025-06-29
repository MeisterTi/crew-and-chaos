using UnityEngine;
using UnityEngine.UI;

public class SailTrimmerUI : MonoBehaviour
{
    [SerializeField] private Button tieSailButton;
    [SerializeField] private Slider sailTensionSlider;
    private WindDrivenBoat boat;
    private bool isTied = false;

    void Start()
    {
        boat = WindDrivenBoat.Instance;

        if (tieSailButton != null)
            tieSailButton.onClick.AddListener(ToggleSailTie);

        if (sailTensionSlider != null)
        {
            sailTensionSlider.minValue = 0f;   // fully loose
            sailTensionSlider.maxValue = 1f;   // fully pulled in
            sailTensionSlider.onValueChanged.AddListener(SetSailTension);
        }
    }

    void ToggleSailTie()
    {
        if (boat == null) return;

        isTied = !isTied;

        if (isTied)
        {
            boat.TieSail();
            tieSailButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Untie Sail";
        }
        else
        {
            boat.UntieSail();
            tieSailButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Tie Sail";
        }
    }

    void SetSailTension(float sliderValue)
    {
        if (boat != null)
            boat.SetSailTension(1f - sliderValue);
    }
}
