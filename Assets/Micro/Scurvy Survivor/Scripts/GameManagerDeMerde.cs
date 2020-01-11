using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

namespace Game.ScurvySurvivor
{
    public class GameManagerDeMerde : MicroMonoBehaviour
    {
        GameObject[] fruits;


        Vector2 rightForce = new Vector2(0.5f, 0);
        Vector2 leftForce = new Vector2(-0.5f, 0);
        int randomValue;

        [Space(10)]

        bool waveActivated = false;
        public float seconde = 5;

        [Space(10)]
        [SerializeField] Transform spawnFruit;
        [SerializeField] Transform spawnFilet;
        [Space(5)]
        [SerializeField] GameObject Apple;
        [SerializeField] GameObject Banana;
        [SerializeField] GameObject Lemon;
        [SerializeField] GameObject Orange;
        GameObject[] FruitList = new GameObject[4];
        GameObject Fruit;
        [SerializeField] GameObject Filet;

        [Space(10)]
        public int timeSeconde;
        float time;
        float  t;

        bool end = false;

        [Space(20)]

        // UI
        [SerializeField]  RectTransform arrowMasktTrans;
        [SerializeField]  RawImage arrowMasktImage;
        [SerializeField]  Canvas canvas;

        private void Start()
        {

            FruitList[0] = Apple;
            FruitList[1] = Banana;
            FruitList[2] = Lemon;
            FruitList[3] = Orange;

            randomValue = UnityEngine.Random.Range(1, 3);

            DifficultySpawn();

            fruits = GameObject.FindGameObjectsWithTag("Player");

            StartCoroutine(WaitForWave());

            Macro.StartGame();
        }

        protected override void OnGameStart()
        {
            Macro.DisplayActionVerb("Save The Fruit!", 1);
        }

        protected override void OnActionVerbDisplayEnd()
        {
            Macro.StartTimer(5, true);

        }

        private void Update()
        {
            if (waveActivated == true)
            {
                t += Time.deltaTime;
                time = Mathf.Lerp(0 ,1 , t / timeSeconde);
                Force();

                if(time == 1 && end == false)
                {
                    Macro.Win();
                    Macro.EndGame();
                    end = true;
                }
            }


        }

        void Force()
        {
            for (int i = 0; i < fruits.Length; i++)
            {
                if (randomValue == 1)
                {
                    fruits[i].GetComponent<Rigidbody2D>().AddForce(rightForce);
                }
                else if (randomValue == 2)
                {
                    fruits[i].GetComponent<Rigidbody2D>().AddForce(leftForce);
                }
            }
        }

        IEnumerator WaitForWave()
        {
            float t = 0f;

            while (t < 1)
            {
                t += Time.deltaTime / seconde;

                if (randomValue == 1)
                {
                    arrowMasktTrans.transform.localScale = new Vector3(Mathf.Lerp(arrowMasktTrans.transform.localScale.x, 0, t), arrowMasktTrans.transform.localScale.y, 0);
                }
                else if (randomValue == 2)
                {
                    arrowMasktTrans.transform.localScale = new Vector3(Mathf.Lerp(arrowMasktTrans.transform.localScale.x, 0, t), arrowMasktTrans.transform.localScale.y, 0);

                }
            }

            yield return new WaitForSeconds(seconde);
            canvas.enabled = false;
            waveActivated = true;
        }

        void DifficultySpawn()
        {
            int spawnNumber;

            if(Macro.Difficulty == 1)
            {
                spawnNumber = UnityEngine.Random.Range(1, 5);

                for(int i = 0; i < spawnNumber; i++)
                {
                    Instantiate(Filet, 
                        new Vector3(UnityEngine.Random.Range(-spawnFilet.localScale.x * 0.5f, spawnFilet.localScale.x * 0.5f),
                                    UnityEngine.Random.Range(-spawnFilet.localScale.y * 0.5f, spawnFilet.localScale.y * 0.5f),
                                    0), 
                        Quaternion.identity, 
                        GameObject.Find("FruitZone").transform);
                }
            }
            else if (Macro.Difficulty == 2)
            {
                spawnNumber = UnityEngine.Random.Range(3, 7);
                int randomSideX;
                int randomSideY;

                for (int i = 0; i < spawnNumber; i++)
                {
                    randomSideX = UnityEngine.Random.Range(1, 3);
                    randomSideY = UnityEngine.Random.Range(1, 3);

                    if (randomSideX == 1 && randomSideY == 1)
                    {
                        Instantiate(FruitList[UnityEngine.Random.Range(0, 4)],
                            new Vector3(UnityEngine.Random.Range(-spawnFilet.localScale.x * 0.5f, -spawnFruit.localScale.x * 0.5f),
                                        UnityEngine.Random.Range(-spawnFilet.localScale.y * 0.5f, -spawnFruit.localScale.y * 0.5f),
                                        0),
                            Quaternion.identity,
                            GameObject.Find("FruitZone").transform);
                    }
                    else if (randomSideX == 1 && randomSideY == 2)
                    {
                        Instantiate(FruitList[UnityEngine.Random.Range(0, 4)],
                            new Vector3(UnityEngine.Random.Range(-spawnFilet.localScale.x * 0.5f, -spawnFruit.localScale.x * 0.5f),
                                        UnityEngine.Random.Range(spawnFilet.localScale.y * 0.5f, spawnFruit.localScale.y * 0.5f),
                                        0),
                            Quaternion.identity,
                            GameObject.Find("FruitZone").transform);
                    }
                    else if (randomSideX == 2 && randomSideY == 1)
                    {
                        Instantiate(FruitList[UnityEngine.Random.Range(0, 4)],
                            new Vector3(UnityEngine.Random.Range(spawnFilet.localScale.x * 0.5f, spawnFruit.localScale.x * 0.5f),
                                        UnityEngine.Random.Range(-spawnFilet.localScale.y * 0.5f, -spawnFruit.localScale.y * 0.5f),
                                        0),
                            Quaternion.identity,
                            GameObject.Find("FruitZone").transform);
                    }
                    else if (randomSideX == 2 && randomSideY == 2)
                    {
                        Instantiate(FruitList[UnityEngine.Random.Range(0, 4)],
                            new Vector3(UnityEngine.Random.Range(spawnFilet.localScale.x * 0.5f, spawnFruit.localScale.x * 0.5f),
                                        UnityEngine.Random.Range(spawnFilet.localScale.y * 0.5f, spawnFruit.localScale.y * 0.5f),
                                        0),
                            Quaternion.identity,
                            GameObject.Find("FruitZone").transform);
                    }
                }
            }
            else
            {
                spawnNumber = UnityEngine.Random.Range(3, 7);
                int randomSideX;
                int randomSideY;
                int whatObject;

                
                for (int i = 0; i < spawnNumber; i++)
                {
                        randomSideX = UnityEngine.Random.Range(1, 3);
                        randomSideY = UnityEngine.Random.Range(1, 3);
                        whatObject = UnityEngine.Random.Range(1, 3);

                    if (whatObject == 1)
                    {
                            randomSideX = UnityEngine.Random.Range(1, 3);
                            randomSideY = UnityEngine.Random.Range(1, 3);

                            if (randomSideX == 1 && randomSideY == 1)
                            {
                            Instantiate(FruitList[UnityEngine.Random.Range(0, 4)],
                                    new Vector3(UnityEngine.Random.Range(-spawnFilet.localScale.x * 0.5f, -spawnFruit.localScale.x * 0.5f),
                                                UnityEngine.Random.Range(-spawnFilet.localScale.y * 0.5f, -spawnFruit.localScale.y * 0.5f),
                                                0),
                                    Quaternion.identity,
                                    GameObject.Find("FruitZone").transform);
                            }
                            else if (randomSideX == 1 && randomSideY == 2)
                            {
                            Instantiate(FruitList[UnityEngine.Random.Range(0, 4)],
                                    new Vector3(UnityEngine.Random.Range(-spawnFilet.localScale.x * 0.5f, -spawnFruit.localScale.x * 0.5f),
                                                UnityEngine.Random.Range(spawnFilet.localScale.y * 0.5f, spawnFruit.localScale.y * 0.5f),
                                                0),
                                    Quaternion.identity,
                                    GameObject.Find("FruitZone").transform);
                            }
                            else if (randomSideX == 2 && randomSideY == 1)
                            {
                            Instantiate(FruitList[UnityEngine.Random.Range(0, 4)],
                                    new Vector3(UnityEngine.Random.Range(spawnFilet.localScale.x * 0.5f, spawnFruit.localScale.x * 0.5f),
                                                UnityEngine.Random.Range(-spawnFilet.localScale.y * 0.5f, -spawnFruit.localScale.y * 0.5f),
                                                0),
                                    Quaternion.identity,
                                    GameObject.Find("FruitZone").transform);
                            }
                            else if (randomSideX == 2 && randomSideY == 2)
                            {
                            Instantiate(FruitList[UnityEngine.Random.Range(0, 4)],
                                    new Vector3(UnityEngine.Random.Range(spawnFilet.localScale.x * 0.5f, spawnFruit.localScale.x * 0.5f),
                                                UnityEngine.Random.Range(spawnFilet.localScale.y * 0.5f, spawnFruit.localScale.y * 0.5f),
                                                0),
                                    Quaternion.identity,
                                    GameObject.Find("FruitZone").transform);
                            }
                    }
                    else
                    {
                        spawnNumber = UnityEngine.Random.Range(1, 5);

                        Instantiate(Filet,
                            new Vector3(UnityEngine.Random.Range(-spawnFilet.localScale.x * 0.5f, spawnFilet.localScale.x * 0.5f),
                                        UnityEngine.Random.Range(-spawnFilet.localScale.y * 0.5f, spawnFilet.localScale.y * 0.5f),
                                        0),
                            Quaternion.identity,
                            GameObject.Find("FruitZone").transform);
                        
                    }
                }
                
            }
        }

    }
}