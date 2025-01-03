using UI.Rect.Enums;
using UnityEngine;

namespace UI.Rect
{
    public static class RectTransformExtensions
    {
        public static void SetAnchorPreset(this RectTransform rectTransform, AnchorPresets preset)
        {
            switch (preset)
            {
                case AnchorPresets.TopLeft:
                    SetAnchors(rectTransform, new Vector2(0, 1), new Vector2(0, 1));
                    break;
                case AnchorPresets.TopRight:
                    SetAnchors(rectTransform, new Vector2(1, 1), new Vector2(1, 1));
                    break;
                case AnchorPresets.BottomLeft:
                    SetAnchors(rectTransform, new Vector2(0, 0), new Vector2(0, 0));
                    break;
                case AnchorPresets.BottomRight:
                    SetAnchors(rectTransform, new Vector2(1, 0), new Vector2(1, 0));
                    break;
                case AnchorPresets.StretchAll:
                    SetAnchors(rectTransform, new Vector2(0, 0), new Vector2(1, 1));
                    rectTransform.offsetMin = Vector2.zero;
                    rectTransform.offsetMax = Vector2.zero;
                    break;
                case AnchorPresets.Centered:
                    SetAnchors(rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f));
                    break;
                case AnchorPresets.TopCentered:
                    SetAnchors(rectTransform, new Vector2(0.5f, 1), new Vector2(0.5f, 1));
                    break;
                case AnchorPresets.BottomCentered:
                    SetAnchors(rectTransform, new Vector2(0.5f, 0), new Vector2(0.5f, 0));
                    break;
                default:
                    Debug.LogWarning("Anchor preset not recognized.");
                    break;
            }
        }

        private static void SetAnchors(RectTransform rectTransform, Vector2 anchorMin, Vector2 anchorMax)
        {
            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
        }
    }
}