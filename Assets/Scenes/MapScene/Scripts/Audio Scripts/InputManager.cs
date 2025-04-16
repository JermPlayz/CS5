using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.Instance.ChangeMusic1(AudioManager.SoundType.Music_Battle_Rain);
            AudioManager.Instance.ChangeMusic2(AudioManager.SoundType.Music_Battle_Thunder);
            AudioManager.Instance.MuteMusic2();
        }
        else if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            AudioManager.Instance.MuteMusic1();
            AudioManager.Instance.UnmuteMusic2();
        }
        else if(Input.GetKeyDown(KeyCode.Tab))
        {
            AudioManager.Instance.MuteMusic2();
            AudioManager.Instance.UnmuteMusic1();
        }
    }
}
