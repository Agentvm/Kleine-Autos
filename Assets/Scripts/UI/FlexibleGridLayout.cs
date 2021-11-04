using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (GridLayoutGroup))]
public class FlexibleGridLayout : MonoBehaviour
{
    private GridLayoutGroup _gridLayoutGroup = null;
    private RectTransform _gridRectTransform = null;

    private async void Start ()
    {
        // Get Components
        _gridLayoutGroup = this.GetComponent<GridLayoutGroup> ();
        _gridRectTransform = this.GetComponent<RectTransform> ();

        MenuPanelPlayer.MenuPanelCountChanged += RefreshLayoutGroup;

        // Start first layout (wait for the GridLayoutGroup to resize)
        await Task.Delay (50);
        RefreshLayoutGroup ();
    }

    // Start is called before the first frame update
    void RefreshLayoutGroup ()
    {
        // Measure self
        float gridHeight = _gridRectTransform.rect.height;
        float gridWidth = _gridRectTransform.rect.width;

        // Count today's Children
        List<RectTransform> childRects = new List<RectTransform> ();
        foreach (Transform transform in this.transform)
            childRects.Add (transform as RectTransform);

        // Get Items per row & Spacing
        int itemsPerColumn = Mathf.CeilToInt (Mathf.Sqrt (childRects.Count));
        float spacing = Mathf.Max (80f / itemsPerColumn, 10f);


        //  Get resulting CellSize (count one space less than items per row)
        float cellSizeX = (gridWidth - (itemsPerColumn - 1) * spacing) / itemsPerColumn;
        float cellSizeY = (gridHeight - (itemsPerColumn - 1) * spacing) / itemsPerColumn;

        // Configure Grid
        _gridLayoutGroup.spacing = new Vector2 (spacing, spacing);
        _gridLayoutGroup.cellSize = new Vector2 (cellSizeX, cellSizeY);
        _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        _gridLayoutGroup.constraintCount = itemsPerColumn;

        LayoutRebuilder.ForceRebuildLayoutImmediate (_gridRectTransform);
    }
}
