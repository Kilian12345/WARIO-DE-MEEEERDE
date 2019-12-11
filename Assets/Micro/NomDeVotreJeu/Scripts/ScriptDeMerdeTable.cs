using UnityEngine;

namespace Game.ScurvySurvivor
{
    public class ScriptDeMerdeTable : MonoBehaviour
    {
        [SerializeField] Material MatDeMerde;
        public float test;

        private void Start()
        {
			
        }

        private void Update()
        {
            float viewPos = (MatDeMerde.GetFloat("_Vertex")* 4 - 4 );

            Vector3 scale = transform.localScale;
            scale.x = (20 + (transform.localPosition.x * viewPos));
            scale.y = (20 + (transform.localPosition.x * viewPos));
            transform.localScale = scale;

            Debug.Log(viewPos);
        }
    }
}