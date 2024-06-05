using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SetAppearance : MonoBehaviour
{
    [SerializeField][Range(0, 2)] int skinColourIndex = 0;
    [SerializeField][Range(-1,2)] int beardIndex = -1;
    [SerializeField][Range(0, 2)] int beardColourIndex = 0;
    [SerializeField][Range(0, 11)] int suitIndex = 0;
    [SerializeField][Range(-1, 4)] int hairIndex = -1;
    [SerializeField][Range(0, 5)] int hairColourIndex = 0;
    [SerializeField] SkinnedMeshRenderer skinMesh;
    [SerializeField] SkinnedMeshRenderer headMesh;
    [SerializeField] Material[] skinColourList;
    [SerializeField] GameObject suitRoot;
    [SerializeField] GameObject hairRoot;
    [SerializeField] GameObject beardRoot;
    [SerializeField] bool randomize = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            //randomize = true;
            if (randomize) {
                Randomize();
                randomize = false;
            }

            UpdateAppearance();
        }
    }

    public void UpdateAppearance()
    {
        SetSkinColour(skinColourIndex);
        SetSuit(suitIndex);
        SetHair(hairIndex, hairColourIndex);
        SetBeard(beardIndex, beardColourIndex);
    }

    void SetSkinColour(int index)
    {
        skinMesh.material = skinColourList[index];
        headMesh.material = skinColourList[index];
    }

    void SetSuit(int index)
    {
        for (int i = 0; i < suitRoot.transform.childCount; i++)
        {
            suitRoot.transform.GetChild(i).gameObject.SetActive(i == index);
        }
        
        
    }

    void SetHair(int index, int colour)
    {
        for (int i = 0; i < hairRoot.transform.childCount; i++) {
            
            hairRoot.transform.GetChild(i).gameObject.SetActive(i == index);

            if (i == index) {
                SkinnedMeshRenderer renderer = hairRoot.transform.GetChild(i).GetComponent<SkinnedMeshRenderer>();
                MaterialList materialList = hairRoot.transform.GetChild(i).GetComponent<MaterialList>();

                renderer.material = materialList.materials[colour];
            }
        }
    }

    void SetBeard(int index, int colour)
    {
        for (int i = 0; i < beardRoot.transform.childCount; i++) {

            beardRoot.transform.GetChild(i).gameObject.SetActive(i == index);

            if (i == index) {
                SkinnedMeshRenderer renderer = beardRoot.transform.GetChild(i).GetComponent<SkinnedMeshRenderer>();
                MaterialList materialList = beardRoot.GetComponent<MaterialList>();

                renderer.material = materialList.materials[colour];
            }
        }
    }

    public void Randomize()
    {
        skinColourIndex = Random.Range(0, 3);
        suitIndex = Random.Range(0, 12);
        beardIndex = Random.Range(-1, 5);
        beardColourIndex = Random.Range(0, 3);
        hairIndex = Random.Range(0, 4);
        hairColourIndex = Random.Range(0, 6);
    }
}
