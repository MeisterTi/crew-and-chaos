using Unity.Netcode;

public class NavigatorUI : BaseUI
{
    private void Start()
    {
        PlayerData playerData = PlayerManager.Instance.GetPlayerDataByClientId(NetworkManager.Singleton.LocalClientId);
        if (playerData.SailingRole == SailingRole.NAVIGATOR)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
}
