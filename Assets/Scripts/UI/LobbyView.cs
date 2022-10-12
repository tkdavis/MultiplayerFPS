using FishNet;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class LobbyView : View
{
    [SerializeField]
    private Button toggleReadyButton;

    [SerializeField]
    private TextMeshProUGUI toggleReadyButtonText;

    [SerializeField]
    private Button startGameButton;

    public override void Initialize()
    {
        toggleReadyButton.onClick.AddListener(() => PlayerNetworking.Instance.ServerSetIsReady(!PlayerNetworking.Instance.isReady));

        if (InstanceFinder.IsHost)
        {
            startGameButton.onClick.AddListener(() => GameManager.Instance.StartGame());
            startGameButton.gameObject.SetActive(true);
        } else
        {
            startGameButton.gameObject.SetActive(false);
        }

        base.Initialize();
    }

    void Update()
    {
        if (!Initialized) return;

        toggleReadyButtonText.color = PlayerNetworking.Instance.isReady ? Color.green : Color.red;
        startGameButton.interactable = GameManager.Instance.canStart;
    }
}
