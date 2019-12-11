using UnityEngine;

namespace Game.ScurvySurvivor
{
    public class ScriptDeMerdeTable : MonoBehaviour
    {
        [SerializeField] Material MatDeMerde;
        public float test = 10;

        private void Start()
        {
			
        }

        private void Update()
        {
            ShitScale();
            ShitMouv();
        }

        void ShitScale()
        {
            float viewPos = (MatDeMerde.GetFloat("_Vertex") * 4 - 4);

            Vector3 scale = transform.localScale;
            scale.x = (test + (transform.localPosition.x * viewPos));
            scale.y = (test + (transform.localPosition.x * viewPos));
            transform.localScale = scale;
        }

        void ShitMouv()
        {
            float viewPos = (MatDeMerde.GetFloat("_Vertex") * 4 - 4);

            Vector3 scale = transform.localScale;
            scale.x = (test + (transform.localPosition.x * viewPos));
            scale.y = (test + (transform.localPosition.x * viewPos));
            transform.localScale = scale;
        }
    }
}