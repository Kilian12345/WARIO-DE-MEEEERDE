using UnityEngine;
using System;
using System.Collections;

namespace Game.RubikarioWare
{
    public class GameManagerDeMerde : MicroMonoBehaviour
    {
        GameObject[] fruits;

        [SerializeField] Material matDeMerde;

        Vector2 rightForce = new Vector2(0.5f, 0);
        Vector2 leftForce = new Vector2(-0.5f, 0);
        int randomValue;

        [Space(10)]

        bool waveActivated = false;
        public float seconde = 5;
        [SerializeField] GameObject Arrow;

        [Space(10)]
        [SerializeField] Transform spawnFruit;
        [SerializeField] Transform spawnFilet;
        [Space(5)]
        [SerializeField] GameObject Fruit;
        [SerializeField] GameObject Filet;

        private void Start()
        {
            fruits = GameObject.FindGameObjectsWithTag("Player");
            randomValue = UnityEngine.Random.Range(1, 3);
            StartCoroutine(WaitForWave());

            DifficultySpawn();


            Macro.StartGame();
        }

        protected override void OnGameStart()
        {

        }

        private void Update()
        {
            if (waveActivated == true)
            {
                Force();
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
            if (randomValue == 1)
            {
                Arrow.GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (randomValue == 2)
            {
                Arrow.GetComponent<SpriteRenderer>().flipX = true;

            }

            yield return new WaitForSeconds(seconde);
            Arrow.SetActive(false);
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
                        GameObject.Find("Fruit___").transform.parent);
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
                        Instantiate(Fruit,
                            new Vector3(UnityEngine.Random.Range(-spawnFilet.localScale.x * 0.5f, -spawnFruit.localScale.x * 0.5f),
                                        UnityEngine.Random.Range(-spawnFilet.localScale.y * 0.5f, -spawnFruit.localScale.y * 0.5f),
                                        0),
                            Quaternion.identity,
                            GameObject.Find("Fruit___").transform.parent);
                    }
                    else if (randomSideX == 1 && randomSideY == 2)
                    {
                        Instantiate(Fruit,
                            new Vector3(UnityEngine.Random.Range(-spawnFilet.localScale.x * 0.5f, -spawnFruit.localScale.x * 0.5f),
                                        UnityEngine.Random.Range(spawnFilet.localScale.y * 0.5f, spawnFruit.localScale.y * 0.5f),
                                        0),
                            Quaternion.identity,
                            GameObject.Find("Fruit___").transform.parent);
                    }
                    else if (randomSideX == 2 && randomSideY == 1)
                    {
                        Instantiate(Fruit,
                            new Vector3(UnityEngine.Random.Range(spawnFilet.localScale.x * 0.5f, spawnFruit.localScale.x * 0.5f),
                                        UnityEngine.Random.Range(-spawnFilet.localScale.y * 0.5f, -spawnFruit.localScale.y * 0.5f),
                                        0),
                            Quaternion.identity,
                            GameObject.Find("Fruit___").transform.parent);
                    }
                    else if (randomSideX == 2 && randomSideY == 2)
                    {
                        Instantiate(Fruit,
                            new Vector3(UnityEngine.Random.Range(spawnFilet.localScale.x * 0.5f, spawnFruit.localScale.x * 0.5f),
                                        UnityEngine.Random.Range(spawnFilet.localScale.y * 0.5f, spawnFruit.localScale.y * 0.5f),
                                        0),
                            Quaternion.identity,
                            GameObject.Find("Fruit___").transform.parent);
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
                                Instantiate(Fruit,
                                    new Vector3(UnityEngine.Random.Range(-spawnFilet.localScale.x * 0.5f, -spawnFruit.localScale.x * 0.5f),
                                                UnityEngine.Random.Range(-spawnFilet.localScale.y * 0.5f, -spawnFruit.localScale.y * 0.5f),
                                                0),
                                    Quaternion.identity,
                                    GameObject.Find("Fruit___").transform.parent);
                            }
                            else if (randomSideX == 1 && randomSideY == 2)
                            {
                                Instantiate(Fruit,
                                    new Vector3(UnityEngine.Random.Range(-spawnFilet.localScale.x * 0.5f, -spawnFruit.localScale.x * 0.5f),
                                                UnityEngine.Random.Range(spawnFilet.localScale.y * 0.5f, spawnFruit.localScale.y * 0.5f),
                                                0),
                                    Quaternion.identity,
                                    GameObject.Find("Fruit___").transform.parent);
                            }
                            else if (randomSideX == 2 && randomSideY == 1)
                            {
                                Instantiate(Fruit,
                                    new Vector3(UnityEngine.Random.Range(spawnFilet.localScale.x * 0.5f, spawnFruit.localScale.x * 0.5f),
                                                UnityEngine.Random.Range(-spawnFilet.localScale.y * 0.5f, -spawnFruit.localScale.y * 0.5f),
                                                0),
                                    Quaternion.identity,
                                    GameObject.Find("Fruit___").transform.parent);
                            }
                            else if (randomSideX == 2 && randomSideY == 2)
                            {
                                Instantiate(Fruit,
                                    new Vector3(UnityEngine.Random.Range(spawnFilet.localScale.x * 0.5f, spawnFruit.localScale.x * 0.5f),
                                                UnityEngine.Random.Range(spawnFilet.localScale.y * 0.5f, spawnFruit.localScale.y * 0.5f),
                                                0),
                                    Quaternion.identity,
                                    GameObject.Find("Fruit___").transform.parent);
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
                            GameObject.Find("Fruit___").transform.parent);
                        
                    }
                }
                
            }
        }

    }
}