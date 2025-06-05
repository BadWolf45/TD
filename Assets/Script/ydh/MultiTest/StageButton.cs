using TMPro;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;

public class StageButton : MonoBehaviour
{
    private TextMeshProUGUI buttonText;
    private void Awake()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnClickStage()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        string mapName = buttonText.text.Trim(); // 버튼에 써있는 맵 이름 사용

        Hashtable props = new Hashtable
        {
            { "MapName", mapName }
        };
        PhotonNetwork.CurrentRoom.SetCustomProperties(props);//포톤써버에 헤시테이블을 테그로 저장.

        PhotonNetwork.LoadLevel("PlayerTestScene");
    }
}
