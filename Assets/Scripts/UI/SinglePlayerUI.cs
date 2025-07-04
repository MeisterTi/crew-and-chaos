using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SinglePlayerUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private Button helmsmanRoleButton;
    [SerializeField] private Button sailTrimmerRoleButton;
    [SerializeField] private Button lookoutRoleButton;
    [SerializeField] private Button navigatorRoleButton;

    private void Awake()
    {
        helmsmanRoleButton.interactable = false;
        sailTrimmerRoleButton.interactable = false;
        lookoutRoleButton.interactable = false;
        navigatorRoleButton.interactable = false;
        helmsmanRoleButton.onClick.AddListener(() => SetPlayerSailingRole(SailingRole.HELMSMAN));
        sailTrimmerRoleButton.onClick.AddListener(() => SetPlayerSailingRole(SailingRole.SAIL_TRIMMER));
        lookoutRoleButton.onClick.AddListener(() => SetPlayerSailingRole(SailingRole.LOOKOUT));
        navigatorRoleButton.onClick.AddListener(() => SetPlayerSailingRole(SailingRole.NAVIGATOR));
    }

    /// <summary>
    /// Setter for the player name
    /// </summary>
    /// <param name="playerName"></param>
    public void SetPlayerName(string playerName)
    {
        playerNameText.text = playerName;
    }

    /// <summary>
    /// Sets the interacability for the roles not assigned
    /// </summary>
    /// <param name="takenRoles">Contains an hashset of roles, that are already assigned</param>
    public void SetAvailableRoles(HashSet<SailingRole> takenRoles)
    {
        helmsmanRoleButton.interactable = !takenRoles.Contains(SailingRole.HELMSMAN);
        sailTrimmerRoleButton.interactable = !takenRoles.Contains(SailingRole.SAIL_TRIMMER);
        lookoutRoleButton.interactable = !takenRoles.Contains(SailingRole.LOOKOUT);
        navigatorRoleButton.interactable = !takenRoles.Contains(SailingRole.NAVIGATOR);
    }

    private void SetPlayerSailingRole(SailingRole sailingRole)
    {
        PlayerManager.Instance.SetPlayerSailingRoleServerRpc(sailingRole);
    }
}
