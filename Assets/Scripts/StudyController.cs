using holoutils;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StudyController : MonoBehaviour
{

    private GameObject[] Avatar; // Female1, Female2, Male1, Male2, Mannequin, Cylinder;
    private GameObject[] Spine;
    private GameObject[] Artwork;
    private AvatarVoiceFeedback avatarVoice;

    private CSVLogger logger;

    List<int> procedure;
    private int currentAvatar;
    bool rotate = true; // rotate (2nd) avatar by 180° to face the participant

    private GameObject MainCamera, ARCamera;

    private MixedRealityKeyboard keyboard;
    private String participantId = "-1";
    private bool logging = true;

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

        avatarVoice = GameObject.Find("AvatarVoice").GetComponent<AvatarVoiceFeedback>();



        // Samoty Check including disabeling all avatars and finding spine

        for (int i = 0; i < 6; i++)
        {
            if (Avatar[i] == null)
                Debug.LogError("missing reference to avatar");

            if (Avatar[i] != null)
            {
                Debug.Log(Avatar[i].name + " found.");
                Debug.Log("Find the Spine for " + Avatar[i].name);
                Spine[i] = Avatar[i].transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine").gameObject;
                if (Spine[i] == null) Debug.LogError("Could not find the Spine for " + Avatar[i].name);
                Avatar[i].SetActive(false);
            }

        }

        // Init Artwork
        Artwork = new GameObject[4];
        Artwork[0] = GameObject.Find("Artwork_BackRight");
        Artwork[1] = GameObject.Find("Artwork_BackLeft");
        Artwork[2] = GameObject.Find("Artwork_FrontRight");
        Artwork[3] = GameObject.Find("Artwork_FrontLeft");

        // Link Logger
        logger = GetComponent<CSVLogger>();

        // Find Camera
        //MainCamera = GameObject.Find("Main Camera");
        ARCamera = GameObject.Find("ARCamera");

        // Create Study procedure (random presentation of Avatars)
        // each Avatar is presented twice in a random order. 
        procedure = new List<int>() {0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 };
        procedure.Shuffle();

        //Set First Avatar 
        currentAvatar = procedure.First();
        procedure.RemoveAt(0);
        Avatar[currentAvatar].SetActive(true);

        LoadNewArtwork();

    }

    // Update is called once per frame
    void Update()
    {
        LogData();  
    }

    private void LogData()
    {
        if (logging)
        {
            List<string> loggingData = new List<string>();

            // Timestamp
            loggingData.Add(DateTime.Now.ToString("yyyyMMdd_HHmmss.fff"));

            // ParticipantID
            loggingData.Add(participantId);

            // CurrentAvatar
            loggingData.Add(currentAvatar.ToString());

            // Participant Position
            //loggingData.Add(ARCamera.transform.position.ToString());
            //loggingData.Add(MainCamera.transform.position.ToString());





            //logger.AddRow(loggingData);
        }
    }

    public void GreetingAvatar()
    {
        avatarVoice.PlayMaleLeft();
        var i = UnityEngine.Random.value;

        if (currentAvatar < 2) // Female Avatar
        {
            if (i < .5)
                avatarVoice.PlayFemaleLeft();
            else
                avatarVoice.PlayFemaleRight();
        }

        if (currentAvatar < 4 && currentAvatar > 1) // Male
            {
                if (i < .5)
                avatarVoice.PlayMaleLeft();
            else
                avatarVoice.PlayMaleRight();
        }

        if (currentAvatar > 3) // Neutral Avatar
        {
            if (i < .5)
                avatarVoice.PlayNeutralLeft();
            else
                avatarVoice.PlayNeutralRight();
        }



    }

    public void ArtworkRating(int rating)
    {
        Debug.Log("Participant provided feedback to artwork: " + rating);
        LoadNextAvatar();
        LoadNewArtwork();

    }

    private void LoadNewArtwork()
    {
        int i=0, k=0;
        System.Random rnd = new System.Random();
        

        while (i == k) // make sure i and k differ
        {
            i = rnd.Next(0, 11);
            k = rnd.Next(0, 11);
        }

        if (!rotate)
        { // Load Backside 
            Artwork[0].GetComponent<ChangeTexture>().LoadTexture(i);
            Artwork[1].GetComponent<ChangeTexture>().LoadTexture(k);
        }
        else
        { // Load Frontside
            Artwork[2].GetComponent<ChangeTexture>().LoadTexture(i);
            Artwork[3].GetComponent<ChangeTexture>().LoadTexture(k);
        }
    }

    public void StartStudy()
    {
        // Get Participant ID
        keyboard = GetComponent<MixedRealityKeyboard>();
        keyboard.ShowKeyboard("",false);
    }

    public void ParticipantIdInput()
    {
        participantId = keyboard.Text;
        Debug.Log("Entered participant ID: " + participantId);
        keyboard.HideKeyboard();
        logger.StartNewCSV();
        logging = true;

        Microsoft.MixedReality.Toolkit.Audio.TextToSpeech tts = GetComponent<Microsoft.MixedReality.Toolkit.Audio.TextToSpeech>();
        var msg = string.Format("Please hand over the HoloLens, the participant id is " + participantId, tts.Voice.ToString());
        tts.StartSpeaking(msg);

        // Hot fix for missing artwork.
        LoadNewArtwork();

    }

    private void LoadNextAvatar()
    {
        //ToDo: check if there is a next avatar. 
        // show info to return HL
        if (procedure.Count == 0)
        {

            logging = false;
            logger.EndCSV();

            Microsoft.MixedReality.Toolkit.Audio.TextToSpeech tts = GetComponent<Microsoft.MixedReality.Toolkit.Audio.TextToSpeech>();
            var msg = string.Format("Thank you. We are done. Please return the Hololens to the experimenter.", tts.Voice.ToString());
            tts.StartSpeaking(msg);

            return;
        }


        //Rotate the avatar back for potential reuse
        if (!rotate)
        {
            Debug.Log("Rotate last Avatar back.");
            Avatar[currentAvatar].transform.Rotate(Vector3.up, -180);
        }

        //Disable Current Avatar. 
        Avatar[currentAvatar].SetActive(false);
        currentAvatar = procedure.First();
        procedure.RemoveAt(0);
        Avatar[currentAvatar].SetActive(true);
        
        if (rotate)
        {
            Debug.Log("Rotate Avatar.");
            Avatar[currentAvatar].transform.Rotate(Vector3.up, 180);
        }
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