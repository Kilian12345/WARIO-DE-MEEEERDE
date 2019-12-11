using UnityEngine;

namespace Game.RubikarioWare
{
    public class ScriptDeMerdeTableVrai : MonoBehaviour
    {
        [SerializeField] Material matDeMerde;

        private void Start()
        {
            matDeMerde.SetFloat("_Vertex", 1);
        }
        private void Update()
        {
            matDeMerde.SetFloat("_Vertex", Mathf.Clamp(matDeMerde.GetFloat("_Vertex") + (Input.GetAxis("Horizontal") * 0.1f * -1), 0.8f , 1.25f));
        }
    }
}