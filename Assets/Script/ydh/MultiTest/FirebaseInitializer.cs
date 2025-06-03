using Firebase;
using UnityEngine;

public class FirebaseInitializer : MonoBehaviour
{
    private void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var status = task.Result;
            if (status == DependencyStatus.Available)
            {
                Debug.Log("? Firebase �ʱ�ȭ �Ϸ�");
            }
            else
            {
                Debug.LogError($"? Firebase �ʱ�ȭ ����: {status}");
            }
        });
    }
}
