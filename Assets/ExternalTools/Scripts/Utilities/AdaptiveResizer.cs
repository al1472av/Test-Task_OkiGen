using UnityEngine;

namespace Utilities
{
    [RequireComponent(typeof(RectTransform))]
    public class AdaptiveResizer : MonoBehaviour
    {
        private RectTransform panel;

        void Start()
        {
            panel = GetComponent<RectTransform>();
            ApplySafeArea(Screen.safeArea);
        }

        void ApplySafeArea (Rect r)
        {
            Vector2 anchorMin = r.position;
            Vector2 anchorMax = r.position + r.size;
            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;
            panel.anchorMin = anchorMin;
            panel.anchorMax = anchorMax;
        }
    }
}