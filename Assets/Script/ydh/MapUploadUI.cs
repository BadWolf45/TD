using UnityEngine;
using TMPro;

public class MapUploadUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField fileNameInput;
    [SerializeField] private FirebaseMapUploader uploader;
    [SerializeField] private GridManager gridManager;

    public async void OnClickUploadButton()
    {
        string fileName = fileNameInput.text;
        if (string.IsNullOrWhiteSpace(fileName))
        {
            Debug.LogWarning("���� �̸��� ��� �ֽ��ϴ�.");
            return;
        }

        string json = gridManager.GetCurrentMapJson();
        await uploader.UploadMap(fileName + ".json", json);
    }
}
