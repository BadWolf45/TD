using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonController : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button startButton;

    private void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            startButton.interactable = false; // ��ư ��Ȱ��ȭ
        }
    }
}
