using UnityEngine;

namespace Game.ScurvySurvivor
{
    public class ScriptDeMerdeTable : MonoBehaviour
    {
        [SerializeField] Material MatDeMerde;

        private void Start()
        {
			
        }

        private void Update()
        {
            float viewPos = MatDeMerde.GetFloat("_Vertex");

            Vector3 scale = transform.localScale;
            scale.x = (viewPos * transform.position.x);
            scale.y = (viewPos * transform.position.x);
            transform.localScale = scale;
        }
    }
}