using UnityEngine;

public class BBASS_Emotion : MonoBehaviour
{
    public Material BBASSEmotion;
    public Texture emotion1;
    public Texture emotion2;
    public bool Cheak = false;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && Cheak)
        {
            BBASSEmotion.SetTexture("_bbassTexture", emotion1);
            Cheak = false;
        }

        else if (Input.GetKeyDown(KeyCode.F) && !Cheak)
        {
            BBASSEmotion.SetTexture("_bbassTexture", emotion2);
            Cheak = true;
        }


    }
}
