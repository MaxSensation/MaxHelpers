using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MaxHelpers
{
    public static class Helper
    {
        // Cache the main Camera for everywhere use
        private static Camera _camera;
        public static Camera Camera
        {
            get
            {
                if (_camera == null) _camera = Camera.main;
                return _camera;
            }
        }
        
        // Cache each WaitForSeconds to minimize the allocation
        private static readonly Dictionary<float, WaitForSeconds> WaitDictionary = new Dictionary<float, WaitForSeconds>();
        public static WaitForSeconds GetWait(float time)
        {
            if (WaitDictionary.TryGetValue(time, out var wait)) return wait;
            WaitDictionary[time] = new WaitForSeconds(time);
            return WaitDictionary[time];
        }
        
        // Check if the mouse is hovering on UI element
        private static PointerEventData _eventDataCurrentPosition;
        private static List<RaycastResult> _results;

        public static bool IsOverUi()
        {
            _eventDataCurrentPosition = new PointerEventData(EventSystem.current) {Mouse}
        }
    }
}