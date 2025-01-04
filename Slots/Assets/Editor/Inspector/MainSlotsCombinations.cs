using Data;
using Game.UI.Inspector;
using UnityEditor;
using UnityEngine;

namespace Inspector
{
    [CustomEditor(typeof(SlotWinCombinations))]
    public class MainSlotsCombinations : Editor
    {
        private const int SpaceBetweenButtons = 10;
        private const int SpaceBetweenCombinations = 20;
        private const int CellSize = 50;
        
        private const int SaveButtonWidth = 200;
        private const int SaveButtonHeight = 20;

        private GUIStyle _cellStyle;
        private GUIStyle _saveButtonStyle;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            SetCellStyle();
            SetSaveButtonStyle();

            SlotWinCombinations buttonGrid = (SlotWinCombinations)target;

            if (buttonGrid.Rows == 0 & buttonGrid.Columns == 0)
            {
                return;
            }

            GUILayout.Space(SpaceBetweenButtons);

            for (int i = 0; i < buttonGrid.CombinationsCount; i++)
            {
                GUILayout.Label($"Combination: {i}");
                
                for (int r = 0; r < buttonGrid.Rows; r++)
                {
                    GUILayout.BeginHorizontal();

                    for (int c = 0; c < buttonGrid.Columns; c++)
                    {
                        if (buttonGrid.Cells == null)
                            return;

                        if (!buttonGrid.Cells.ContainsKey(new Position(i, c, r).GetHashCode()))
                            continue;

                        Position position = new Position(i, c, r);
                        
                        Cell cell = buttonGrid.Cells[position.GetHashCode()];
                        cell.IsPressed = GUILayout.Toggle(cell.IsPressed, "", _cellStyle);
                        cell.Combination = i;
                        cell.Position = position;
                    }

                    GUILayout.EndHorizontal();
                }
                
                GUILayout.Space(SpaceBetweenCombinations);
            }

            if (GUILayout.Button("Save",_saveButtonStyle))
            {
                buttonGrid.SaveCombinations();
            }
            
            if (GUILayout.Button("Redraw Combinations", _saveButtonStyle))
            {
                buttonGrid.RedrawCombinations();
            }
            
            EditorUtility.SetDirty(buttonGrid);
        }

        private void SetCellStyle()
        {
            _cellStyle = new GUIStyle(GUI.skin.button)
            {
                fixedWidth = CellSize,
                fixedHeight = CellSize
            };
        }
        
        private void SetSaveButtonStyle()
        {
            _saveButtonStyle = new GUIStyle(GUI.skin.button)
            {
                fixedWidth = SaveButtonWidth,
                fixedHeight = SaveButtonHeight
            };
        }
    }
}