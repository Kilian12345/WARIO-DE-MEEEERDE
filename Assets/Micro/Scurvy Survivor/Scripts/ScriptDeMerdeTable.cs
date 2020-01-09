using UnityEngine;

namespace Game.ScurvySurvivor
{
    public class ScriptDeMerdeTable : MonoBehaviour
    {
        [SerializeField] Material MatDeMerde;
        public float test = 10;

        Rigidbody2D RB;

        private Color mouseOverColor = Color.blue;
        private Color originalColor = Color.yellow;
        private bool dragging = false;
        private float distance;
        SpriteRenderer renderer;

        MaterialPropertyBlock propBlock;
        public Color ObjectColor;

        GameObject table;

        bool end = false;

        private void Start()
        {
            renderer = GetComponentInChildren<SpriteRenderer>();
            RB = GetComponent<Rigidbody2D>();

            propBlock = new MaterialPropertyBlock();

            renderer.GetPropertyBlock(propBlock);
            propBlock.SetColor("_ColorSprite", ObjectColor);
            renderer.SetPropertyBlock(propBlock);

            table = GameObject.Find("TableDemerde");
        }

        private void LateUpdate()
        {
            ShitScale();
            ShitMouv();
            ShitDragAndDrop();
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

        void ShitDragAndDrop()
        {
            if (dragging)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 rayPoint = ray.GetPoint(distance);
                transform.position = rayPoint;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.layer == 4 && end == false)
            {
                Macro.Lose();
                Macro.EndGame();
                end = true;
            }
        }


        void OnMouseEnter()
        {
            renderer.material.SetColor("_Color" , Color.red );
        }

        void OnMouseExit()
        {
            renderer.material.SetColor("_Color", new Color(1,1,1,0));
        }

        void OnMouseDown()
        {
            distance = Vector3.Distance(transform.position, Camera.main.transform.position);
            dragging = true;
        }

        void OnMouseUp()
        {
            dragging = false;
        }

    }
}