using UnityEngine;

public class TileEditManager : MonoBehaviour
{
    public EditMode editMode = EditMode.TileStateEdit;
    public TileState currentTileState = TileState.None;
    public TileAccessType currentAccessType = TileAccessType.Everyone;

    // ---------- ��ġ ���� ��� ���� ----------
    public void SelectInstallableMode()
    {
        editMode = EditMode.TileStateEdit;
        currentTileState = TileState.Installable;
    }

    public void SelectUninstallableMode()
    {
        editMode = EditMode.TileStateEdit;
        currentTileState = TileState.Uninstallable;
    }

    public void SelectStartPointMode_MasterOnly()
    {
        editMode = EditMode.TileStateEdit;
        currentTileState = TileState.StartPoint;
        currentAccessType = TileAccessType.MasterOnly;
    }
    public void SelectStartPointMode_ClientOnly()
    {
        editMode = EditMode.TileStateEdit;
        currentTileState = TileState.StartPoint;
        currentAccessType = TileAccessType.ClientOnly;
    }

    public void SelectEndPointMode()
    {
        editMode = EditMode.TileStateEdit;
        currentTileState = TileState.EndPoint;
    }

    // ---------- ���� ���� ��� ���� ----------
    public void SetAccessEveryone()
    {
        editMode = EditMode.AccessTypeEdit;
        currentAccessType = TileAccessType.Everyone;
    }

    public void SetAccessMasterOnly()
    {
        editMode = EditMode.AccessTypeEdit;
        currentAccessType = TileAccessType.MasterOnly;
    }

    public void SetAccessClientOnly()
    {
        editMode = EditMode.AccessTypeEdit;
        currentAccessType = TileAccessType.ClientOnly;
    }
}
