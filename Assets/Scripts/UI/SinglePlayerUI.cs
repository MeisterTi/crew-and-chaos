using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SinglePlayerUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private Button role0Button;
    [SerializeField] private Button role1Button;
    [SerializeField] private Button role2Button;
    [SerializeField] private Button role3Button;

    public void SetPlayerName(string playerName)
    {
        playerNameText.text = playerName;
    }
}
