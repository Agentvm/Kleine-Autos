using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class FlexibleGridLayout : MonoBehaviour
{
    private GridLayoutGroup _gridLayoutGroup = null;

    private async void Start()
    {
        _gridLayoutGroup = this.GetComponent<GridLayoutGroup>();
        await Task.Delay(50);
        InitializeLayoutGroup();
    }

    // Start is called before the first frame update
    void InitializeLayoutGroup()
    {
        // Measure self
        RectTransform gridRectTransform = this.GetComponent<RectTransform>();
        float gridHeight = gridRectTransform.rect.height;
        float gridWidth = gridRectTransform.rect.width;

        // Count today's Children
        List<RectTransform> childRects = new List<RectTransform>();
        foreach (Transform transform in this.transform)
            childRects.Add(transform as RectTransform);

        // Get Items per row & Spacing
        int itemsPerColumn = Mathf.CeilToInt(Mathf.Sqrt(childRects.Count));
        float spacing = Mathf.Max(80f / itemsPerColumn, 10f);


        //  Get resulting CellSize (count one space less than items per row)
        float cellSizeX = (gridWidth - (itemsPerColumn - 1) * spacing) / itemsPerColumn;
        float cellSizeY = (gridHeight - (itemsPerColumn - 1) * spacing) / itemsPerColumn;

        // Configure Grid
        _gridLayoutGroup.spacing = new Vector2(spacing, spacing);
        _gridLayoutGroup.cellSize = new Vector2(cellSizeX, cellSizeY);
        _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        _gridLayoutGroup.constraintCount = itemsPerColumn;
    }
}
