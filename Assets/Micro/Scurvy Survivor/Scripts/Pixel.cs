using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.ScurvySurvivor
{
    [ExecuteInEditMode]
    public class Pixel : MonoBehaviour
    {

        public Material pixelMaterial;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            Graphics.Blit(source, destination, pixelMaterial);
        }
    }
}