using UnityEngine;

public class StageSorting : MonoBehaviour
{
    public float verticalOffset = 50f; // ���Ʒ��� �̵��� �Ÿ�

    void Start()
    {
        // Content�� �ڽ� ��ü ��������
        Transform[] children = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i);
        }

        // Zigzag ��ġ ����
        for (int i = 0; i < children.Length; i++)
        {
            RectTransform rectTransform = children[i].GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                // ���� ��ġ�� ������� ����
                Vector2 originalPosition = rectTransform.anchoredPosition;
                float yOffset = (i % 2 == 0) ? -verticalOffset : verticalOffset; // ¦���� ��, Ȧ���� �Ʒ�
                rectTransform.anchoredPosition = new Vector2(originalPosition.x, originalPosition.y + yOffset);
            }
        }
    }
}
