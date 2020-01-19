using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.ScurvySurvivor
{
    [ExecuteInEditMode]
    public class Pixel : MonoBehaviour
    {

        public Material pixelMaterial;

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            Graphics.Blit(source, destination, pixelMaterial);
        }
    }
}
