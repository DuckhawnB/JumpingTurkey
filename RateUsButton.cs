using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateUsButton : MonoBehaviour
{

    public void RateUs()
    {
#if UNITY_ANDROID
 Application.OpenURL("market://details?id=com.Kungee.Jumping");
#elif UNITY_IPHONE
        Application.OpenURL("itms-apps://itunes.apple.com/app/com.Kungee.Jumping");
#endif
    }
}
