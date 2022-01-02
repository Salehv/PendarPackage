using UnityEngine;

namespace Pender.ColorSettings
{
    public class PrColoredObject : MonoBehaviour
    {
        public PrColorId colorId;
        
        private void Start()
        {
            SetMaterial();            
        }

        private void SetMaterial()
        {
            var meshRenderer = GetComponentsInChildren<MeshRenderer>();
            foreach (var r in meshRenderer)
            {
                r.sharedMaterial = PendarColorSettings.GetSharedMaterial(colorId);                
            }
        }

        private void OnValidate()
        {
            SetMaterial();
        }
        
        
    }
}
