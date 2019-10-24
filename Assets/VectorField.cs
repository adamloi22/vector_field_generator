using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorField : MonoBehaviour
{
    // Seems like cones won't work well
    public GameObject arrowPrefab;
    public Material vectorMaterialPrefab;
    public int arrowNum;
    public float fieldSize;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 initVectorDirection = new Vector3(0, 1, 0);
        List<float> xList = new List<float>();
        float maxColour = 0;
        float minColour = 0;
        for (int i = 0; i < arrowNum; i++)
        {
            xList.Add(i * fieldSize / (arrowNum - 1) - fieldSize / 2);
        }
        for (int i = 0; i < arrowNum; i++)
        {
            for (int j = 0; j < arrowNum; j++)
            {
                for (int k = 0; k < arrowNum; k++)
                {
                    Vector3 spawnVector = new Vector3(xList[i]*xList[j]*xList[k], xList[i] * xList[j] * xList[k], xList[i] * xList[j] * xList[k]);
                    float colour = (spawnVector.magnitude);
                    if (i == 1 && j == 1 && k == 1)
                    {
                        minColour = colour;
                        maxColour = colour;
                    }
                    if (colour > maxColour)
                    {
                        maxColour = colour;
                    }
                    if (colour < minColour)
                    {
                        minColour = colour;
                    }
                }
            }
        }
        for (int i = 0; i < arrowNum; i++)
        {
            for (int j = 0; j < arrowNum; j++)
            {
                for (int k = 0; k < arrowNum; k++)
                {
                    Vector3 spawnPosition = new Vector3(xList[i], xList[j], xList[k]);
                    Vector3 spawnVector = new Vector3(xList[i], xList[j], xList[k]);
                    float colour = spawnVector.magnitude;
                    Vector3 spawnVectorDirection = spawnVector.normalized;
                    Vector3 rotateAxis = Vector3.Cross(spawnVectorDirection, initVectorDirection).normalized;
                    float dotProd = Vector3.Dot(spawnVectorDirection, initVectorDirection);
                    float rotateAmount = -180*Mathf.Acos(dotProd / spawnVectorDirection.magnitude)/Mathf.PI;


                    if (colour > 0)
                    {
                        if (rotateAxis == Vector3.zero && dotProd < 0)
                        {
                            GameObject newArrow = (GameObject)Instantiate(arrowPrefab, spawnPosition, Quaternion.Euler(180, 0, 0));
                            Renderer rend = newArrow.GetComponent<Renderer>();
                            rend.material = new Material(vectorMaterialPrefab);
                            rend.material.SetColor("_Color", Color.Lerp(Color.blue, Color.red, (colour - minColour) / (maxColour - minColour)));
                            newArrow.transform.parent = transform;
                        }
                        else
                        {
                            GameObject newArrow = (GameObject)Instantiate(arrowPrefab, spawnPosition, Quaternion.AngleAxis(rotateAmount, rotateAxis));
                            Renderer rend = newArrow.GetComponent<Renderer>();
                            rend.material = new Material(vectorMaterialPrefab);
                            rend.material.SetColor("_Color", Color.Lerp(Color.blue, Color.red, (colour - minColour) / (maxColour - minColour)));
                            newArrow.transform.parent = transform;
                        }
                        print(colour/maxColour);
                    }
                }
            }
        }
    }
}
