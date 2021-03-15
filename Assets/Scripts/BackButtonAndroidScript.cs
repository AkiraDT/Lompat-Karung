﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonAndroidScript : MonoBehaviour {
	
	void Update () {
		if (Input.GetKeyUp(KeyCode.Escape))		//ketika klik tombol back
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
				activity.Call<bool>("moveTaskToBack", true);		//pindah ke activity sebelumnya
			}
			else
			{
				Application.Quit();		//keluar aplikasi
			}
		}
	}
}
