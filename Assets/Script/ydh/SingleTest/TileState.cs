public enum TileState
{
    None,           // �ƹ��͵� ����
    Installable,     // ��ġ ����
    Uninstallable,   // ��ġ �Ұ�
    Installed,        // �̹� ��ġ��
    StartPoint,    // ��� ����
    EndPoint,        //��������
    MasterInstallable,     // ���������� Ÿ��
    ClientInstallable       //Ŭ���̾�Ʈ ���� Ÿ��
}
public enum TileAccessType
{
    Everyone,
    MasterOnly,
    ClientOnly
}
public enum EditMode
{
    TileStateEdit,     // ��ġ ���� ���� ���
    AccessTypeEdit     // ���� ���� ���� ���
}

