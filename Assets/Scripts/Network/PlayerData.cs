using System;
using Unity.Collections;
using Unity.Netcode;

public struct PlayerData : INetworkSerializable, IEquatable<PlayerData>
{
    public ulong ClientId;
    public FixedString32Bytes PlayerName;
    public SailingRole SailingRole;

    public PlayerData(ulong clientId, string playerName)
    {
        ClientId = clientId;
        PlayerName = playerName;
        SailingRole = SailingRole.NONE;
    }

    public bool Equals(PlayerData other)
    {
        return ClientId == other.ClientId &&
            PlayerName == other.PlayerName &&
            SailingRole == other.SailingRole;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref ClientId);
        serializer.SerializeValue(ref PlayerName);
        serializer.SerializeValue(ref SailingRole);
    }
}