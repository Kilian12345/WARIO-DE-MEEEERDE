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
        public bool IsItFilet = false;
        private float distance;
        SpriteRenderer renderer;

        MaterialPropertyBlock propBlock;
        public Color ObjectColor;

        GameObject table;
        Camera camera;
        Collider2D collider;

        bool end = false;

        Vector3 scale;
        Vector3 position;

        private void Start()
        {
            renderer = GetComponentInChildren<SpriteRenderer>();
            RB = GetComponent<Rigidbody2D>();
            collider = GetComponent<Collider2D>();

            propBlock = new MaterialPropertyBlock();

            renderer.GetPropertyBlock(propBlock);
            propBlock.SetColor("_ColorSprite", ObjectColor);
            renderer.SetPropertyBlock(propBlock);

            table = GameObject.Find("TableDemerde");
            camera = FindObjectOfType<Pixel>().GetComponent<Camera>();
        }

        private void Update()
        {
            ShitScale();
            ShitMouv();
            ShitDragAndDrop();
        }

        void ShitScale()
        {
            float viewPos = (MatDeMerde.GetFloat("_Vertex") * 4 - 4);

            scale = transform.localScale;
            scale.x = (test + (transform.localPosition.x * viewPos));
            scale.y = (test + (transform.localPosition.x * viewPos));
            scale.z = 0;
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
            if (dragging && IsItFilet == false)
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                Vector3 rayPoint = ray.GetPoint(distance);
                transform.position = rayPoint;
                position = transform.position;
                position.z = -1;
                transform.position = position;
                transform.localScale = new Vector3(transform.localScale.x * 1.5f, transform.localScale.y * 1.5f, 0);
                collider.enabled = false;
            }
            else
            {
                position = transform.position;
                position.z = 0;
                transform.position = position;
                collider.enabled = true;
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
            if(IsItFilet == false)
                renderer.material.SetColor("_Color" , Color.red );
        }

        void OnMouseExit()
        {
            if (IsItFilet == false)
                renderer.material.SetColor("_Color", new Color(1,1,1,0));
        }

        void OnMouseDown()
        {
            distance = Vector3.Distance(transform.position, new Vector3(camera.transform.position.x, camera.transform.position.y ,0));
            dragging = true;
        }

        void OnMouseUp()
        {
            dragging = false;
        }

    }
}