using UnityEngine;

namespace Game.ScurvySurvivor
{
    public class ScriptDeMerdeTable : MonoBehaviour
    {
        [SerializeField] Material MatDeMerde;
        public float test = 10;

        Rigidbody2D RB;

        private void Start()
        {
            RB = GetComponent<Rigidbody2D>();
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
            float viewPos2 = (MatDeMerde.GetFloat("_Vertex") * 4 - 4);
            Vector2 force = new Vector2(viewPos2 * -1, 0.0f);

            RB.AddForce(force, ForceMode2D.Force);
        }
    }
}