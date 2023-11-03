
using UnityEngine;

namespace EcoMine.Common.Utils
{
    public class ScreenUtils
    {
        private const int screenHeightBase = 640;
        private const int screenWidthBase = 360;
        
        public static float GetRateMutiply()
        {
            Vector2 referenceResolution = new Vector2(screenHeightBase, screenWidthBase); 
            Vector2 screenSize = new Vector2(Screen.height, Screen.width); 

            float rateCanvas = referenceResolution.x / referenceResolution.y;
            float rateScreen = screenSize.x / screenSize.y;

            return rateScreen / rateCanvas;
        }
        
        public static Vector2 ConvertWorldToScreenPoint(RectTransform parentRect, Vector3 pos, Camera _camera)
        {
            var screenPoint = _camera.WorldToScreenPoint(pos);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, screenPoint, _camera,
                out var localPoint);
            return localPoint;
        }
    }
}