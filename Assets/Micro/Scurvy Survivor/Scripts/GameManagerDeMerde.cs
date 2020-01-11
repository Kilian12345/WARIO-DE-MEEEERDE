using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

namespace Game.ScurvySurvivor
{
    public class GameManagerDeMerde : MicroMonoBehaviour
    {
        GameObject[] fruits;


        public Vector2 rightForce = new Vector2(0.5f, 0);
        public Vector2 leftForce = new Vector2(-0.5f, 0);
        int randomValue;

        [Space(10)]

        [HideInInspector] public bool waveActivated = false;
        public float seconde;

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

        [HideInInspector] public bool endMana = false;

        [Space(20)]
        [Header ("UI")]

        // UI
        [SerializeField]  RectTransform arrowMasktTrans;
        [SerializeField]  RectTransform arrowParent;
        [SerializeField]  Image arrowMasktImage;
        [SerializeField]  Canvas canvas;
        public Text textWin;
        public Text textLose;


        [Space(20)]
        [Header("Camera Shake")]

        // How long the object should shake for.
        public float shakeDuration = 0f;
        public float shakeAmount = 0.7f;
        public float decreaseFactor = 1.0f;
        Vector3 originalPos;
        Transform camTransform;

        [Space(20)]
        [Header("Sound")]

        public AudioClip PickUpSound;
        public AudioClip DropSound;
        [HideInInspector] public AudioSource audioSource;

        bool ghetto = false;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();

            camTransform = FindObjectOfType<Pixel>().GetComponent<Camera>().transform;
            originalPos = camTransform.localPosition;

            FruitList[0] = Apple;
            FruitList[1] = Banana;
            FruitList[2] = Lemon;
            FruitList[3] = Orange;

            randomValue = UnityEngine.Random.Range(1, 3);

            DifficultySpawn();

            fruits = GameObject.FindGameObjectsWithTag("Player");

            canvas.enabled = false;

            Macro.StartGame();
        }

        void CameraShake()
        {
            if (shakeDuration > 0)
            {
                camTransform.localPosition = originalPos + UnityEngine.Random.insideUnitSphere * shakeAmount;

                shakeDuration -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                shakeDuration = 0f;
                camTransform.localPosition = originalPos;
            }
        }

        protected override void OnGameStart()
        {
            Macro.DisplayActionVerb("Save The Fruit!", 1);
        }

        protected override void OnActionVerbDisplayEnd()
        {
            Macro.StartTimer(5, true);
            canvas.enabled = true;
            StartCoroutine(WaitForWave());

        }

        private void Update()
        {
            if (waveActivated == true)
            {
                t += Time.deltaTime;
                time = Mathf.Lerp(0 ,1 , t / timeSeconde);
                Force();

                CameraShake();

                if (time == 1 && endMana == false && ghetto == false)
                {
                    StartCoroutine(End());
                    ghetto = true;
                }
            }


        }

        IEnumerator End()
        {
            textWin.enabled = true;
            endMana = true;
            Macro.Win();
            yield return new WaitForSeconds(1);
            Macro.EndGame();
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
            var currentScale = arrowMasktTrans.transform.localScale;

            while (t < 1)
            {
                t += Time.deltaTime / seconde;

                arrowMasktTrans.transform.localScale = new Vector3(Mathf.Lerp(currentScale.x, 0, t), currentScale.y, 0);


                if (t < 0.5f)
                {
                    arrowMasktImage.color = new Color(Mathf.Lerp(0, 1, (t * 2f)), 1, 0);
                }
                else if (t >= 0.5f)
                {
                    arrowMasktImage.color = new Color(255, Mathf.Lerp(1, 0, t * 2f - 1), 0);
                }


                if (randomValue == 1)
                {
                    arrowParent.transform.localScale = new Vector3(-1,1,1);
                }
                else if (randomValue == 2)
                {
                    arrowParent.transform.localScale = new Vector3(1, 1, 1);

                }

                yield return arrowMasktTrans.transform.localScale;
                yield return arrowMasktImage.color;
            }

            //yield return new WaitForSeconds(seconde);
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
                int spawnNumberThree = UnityEngine.Random.Range(5, 7); // De base (3,7)
                int randomSideX;
                int randomSideY;
                int whatObject;

                
                for (int i = 0; i < spawnNumberThree; i++)
                {
                        randomSideX = UnityEngine.Random.Range(1, 3);
                        randomSideY = UnityEngine.Random.Range(1, 3);
                        whatObject = UnityEngine.Random.Range(1, 4);

                    if (whatObject <= 2)
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
                        int spawnNumberThreeDos = UnityEngine.Random.Range(1, 3);

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