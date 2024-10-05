using UnityEngine;
using UnityEngine.UI;

namespace FX
{
    public class UIProgressMaterialAnimator : ProgressMaterialAnimator
    {
        public override Material GetMaterial()
        {
            var image = GetComponent<Image>();
            return image.material;
        }
    }
}