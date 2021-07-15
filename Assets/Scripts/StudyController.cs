using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StudyController : MonoBehaviour
{

    private GameObject[] Avatar; // Female1, Female2, Male1, Male2, Mannequin, Cylinder;
    private GameObject[] Spine;

    List<int> procedure;
    private int currentAvatar;
    bool rotate = false; // rotate avatar by 180° to face the participant

    // Start is called before the first frame update
    void Start()
    {

        // Find all Objects needed for the study.
        // 1. Avatars
        Avatar = new GameObject[6];
        Spine = new GameObject[6];
        Avatar[0] = GameObject.Find("Female_Adult_1");
        Avatar[1] = GameObject.Find("Female_Adult_2");
        Avatar[2] = GameObject.Find("Male_Adult_1");
        Avatar[3] = GameObject.Find("Male_Adult_2");
        Avatar[4] = GameObject.Find("Mannequin_1");
        Avatar[5] = GameObject.Find("Cylinder");


        // Samoty Check including disabeling all avatars
        foreach (var item in Avatar)
        {
            if (item == null)
                Debug.LogError("missing reference to avatar");
            else {
                Debug.Log(item.name + " found.");
                item.SetActive(false);
            }
                
        }

        // Create Study procedure (random presentation of Avatars)
        // each Avatar is presented twice in a random order. 
        procedure = new List<int>() {0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 };
        procedure.Shuffle();

        //Set First Avatar 
        currentAvatar = 0;
        LoadNextAvatar();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ArtworkRating(int rating)
    {
        Debug.Log("Participant provided feedback to artwork: " + rating);
        LoadNextAvatar();
    }

    private void LoadNextAvatar()
    {
        //ToDo: check if there is a next avatar. 

        //Rotate the avatar back for potential reuse
        if (!rotate)
        {
            Avatar[currentAvatar].transform.Rotate(Vector3.up, 180);
        }

        //Disable Current Avatar. 
        Avatar[currentAvatar].SetActive(false);
        currentAvatar = procedure.First();
        procedure.RemoveAt(0);
        Avatar[currentAvatar].SetActive(true);


        rotate = !rotate;


    }
}



static class Extension
{
   public static void Shuffle<T>(this IList<T> list)
    {
        System.Random rnd = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rnd.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}