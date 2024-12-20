using UnityEngine;

namespace Instruments
{
    public static class LayerMaskUtil
    {
        public static bool ContainsLayer(this LayerMask layerMask, GameObject gameObject)
        {
            return (layerMask.value & (1 << gameObject.layer)) > 0;
        }
    }
}