using UnityEngine;

namespace Game.ScurvySurvivor
{
    public class ScriptDeMerdeTableVrai : MonoBehaviour
    {
        [SerializeField] Material matDeMerde;
        GameManagerDeMerde gameMana;

        private void Start()
        {
            gameMana = FindObjectOfType<GameManagerDeMerde>();
            matDeMerde.SetFloat("_Vertex", 1);
        }
        private void Update()
        {
            if(gameMana.waveActivated == true)
                matDeMerde.SetFloat("_Vertex", Mathf.Clamp(matDeMerde.GetFloat("_Vertex") + (Input.GetAxis("Horizontal") * 0.1f * -1), 0.8f , 1.25f));
        }
    }
}