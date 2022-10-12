using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Linq;
using UnityEngine;

public sealed class GameManager : NetworkBehaviour
{
    public static GameManager Instance { get; private set; }

    [SyncObject]
    public readonly SyncList<PlayerNetworking> players = new SyncList<PlayerNetworking>();

    [SyncVar]
    public bool canStart;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (!IsServer) return;

        if (players.Count > 0)
        {
            canStart = players.All(player => player.isReady);
        }
    }

    [Server]
    public void StartGame()
    {
        if (!canStart) return;
        foreach (PlayerNetworking player in players)
        {
            player.StartGame();
        }
    }

    [Server]
    public void StopGame()
    {
        foreach (PlayerNetworking player in players)
        {
            player.StopGame();
        }
    }
}
