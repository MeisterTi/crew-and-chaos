using Unity.Netcode;

public class HandsmenUI : BaseUI
{
    private void Start()
    {
        PlayerData playerData = PlayerManager.Instance.GetPlayerDataByClientId(NetworkManager.Singleton.LocalClientId);
        if (playerData.SailingRole == SailingRole.HELMSMAN)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
}
