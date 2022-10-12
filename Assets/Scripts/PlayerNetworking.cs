using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AddressableAssets;

public sealed class PlayerNetworking : NetworkBehaviour
{
    public static PlayerNetworking Instance { get; private set; }
    StarterAssetsInputs _input;

    [SyncVar]
    public string username;

    [SyncVar]
    public bool isReady;

    [SyncVar]
    public PawnController controlledPawn;

    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        _input.cursorLocked = false;
		_input.cursorInputForLook = false;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        GameManager.Instance.players.Add(this);
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        GameManager.Instance.players.Remove(this);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!IsOwner) return;
        Instance = this;

        UIManager.Instance.Initialize();
    }

    void Update()
    {
        if (!IsOwner) return;

        // unexpected behavior here
        if (_input.ready && !isReady)
        {
            ServerSetIsReady(!isReady);
        }
    }

    public void StartGame()
    {
        GameObject pawnPrefab = Addressables.LoadAssetAsync<GameObject>("Pawn").WaitForCompletion();
        GameObject pawnInstance = Instantiate(pawnPrefab);
        Spawn(pawnInstance, Owner);

        controlledPawn = pawnInstance.GetComponent<PawnController>();
        _input.cursorLocked = true;
		_input.cursorInputForLook = true;
    }

    public void StopGame()
    {
        if (controlledPawn != null && controlledPawn.IsSpawned) controlledPawn.Despawn();
    }

    [ServerRpc(RequireOwnership = false)]
    public void ServerSetIsReady(bool value)
    {
        isReady = value;
    }
}
