using UnityEngine;
using System.Collections.Generic;

//[ExecuteInEditMode]
using System.Collections;


public class SoundManagerScript : MonoBehaviour {

	[System.Serializable]
	public struct AudioClipArray{
		public string name;
		public AudioClip[] listAudios;
	}
		
	public AudioClipArray[] audios;


	public void Start () {


		//El manager de cada audio en el soundObject, aunque esten referenciados en el padre;
		for(int i=0;i<audios.Length;i++){
			//Inicializacion de los soundObject
			GameObject g = new GameObject();
			g.transform.parent = transform;
			g.transform.name = audios[i].name;
			g.AddComponent<SoundObject>().Initialize(audios[i].listAudios,128,false);
		}

		//Propiedades particulares de los audios


	}

	void Awake(){
		DontDestroyOnLoad(gameObject);
	}

	public void PlaySound(string soundName, bool loop){

		for(int i=0; i<transform.childCount;i++){
			if(soundName == transform.GetChild(i).name){
				print(soundName);
				SoundObject so = transform.GetChild(i).GetComponent<SoundObject>();

				if(so.GetAudio().isPlaying){
					return;
				}
				so.SetLoop(loop);
				so.Play();
				return;

			}
		}

		return;

	}

	public void PlaySoundPitchVariation(string soundName, bool loop){

		for(int i=0; i<transform.childCount;i++){
			if(soundName == transform.GetChild(i).name){

				SoundObject so = transform.GetChild(i).GetComponent<SoundObject>();
				/*
				if(so.GetAudio().isPlaying){
					return;
				}
				*/
				so.SetLoop(loop);
				float scale = Mathf.Pow(2f,1f/12.0f);
				int n = Random.Range(1,73);
				so.SetPitch(Mathf.Pow(1.05946f,n));
				so.Play();
				return;

			}
		}

		return;

	}

	public void PlaySoundPitchVariation(string soundName, float pitch, bool loop){

		for(int i=0; i<transform.childCount;i++){
			if(soundName == transform.GetChild(i).name){

				SoundObject so = transform.GetChild(i).GetComponent<SoundObject>();
				/*
				if(so.GetAudio().isPlaying){
					return;
				}
				*/
				so.SetLoop(loop);
				float scale = Mathf.Pow(2f,1f/12.0f);
				float n = pitch;
				so.SetPitch(Mathf.Pow(1.05946f,n));
				so.Play();
				return;

			}
		}

		return;

	}

	public void StopSound(string soundName){
		for (int i=0; i<transform.childCount; i++) {
			if (soundName == transform.GetChild (i).name) {
				SoundObject so = transform.GetChild (i).GetComponent<SoundObject> ();

				if (so.GetAudio ().isPlaying) {
						so.Stop();
				}
			}
		}
	}

	public void StopAllSounds(){
		for (int i=0; i<transform.childCount; i++) {
			SoundObject so = transform.GetChild (i).GetComponent<SoundObject> ();
			
			if (so.GetAudio ().isPlaying) {
				so.Stop();
			}
		}
	}

	public void PlayRandomSound(string soundName, bool loop){
		
		for(int i=0; i<transform.childCount;i++){
			if(soundName == transform.GetChild(i).name){
				transform.GetChild(i).GetComponent<SoundObject>().RandomChangeClip();
				SoundObject so = transform.GetChild(i).GetComponent<SoundObject>();
				
				/*if(so.GetAudio().isPlaying){
					return;
				}*/


				so.SetLoop(loop);
				so.Play();
				//SI ES LA MUSICA DE INTRO LA BAJO. QUE ESTA ALTA
				if (soundName == "Intro") so.GetAudio().volume = 0.75f;
				return;
				
			}
		}
		
		return;
		
	}

	public SoundObject GetSound(string soundName){

		for(int i=0; i<transform.childCount;i++){
			if(soundName==transform.GetChild(i).name){
				return transform.GetChild(i).GetComponent<SoundObject>();
			}
		}

		return null;

	}


	public void VolumeControl(string soundName, float vol){
		//string soundName="Calendula";
		float v= 1;
		for(int i=0; i<transform.childCount;i++){
			if(soundName == transform.GetChild(i).name){
				//transform.GetChild(i).GetComponent<SoundObject>().RandomChangeClip();
				SoundObject so = transform.GetChild(i).GetComponent<SoundObject>();
				so.SetVolume(vol);
			}}
		}

	public void SFXVolumeControl(){
		float v= 1;
		for(int i=0; i<transform.childCount;i++){
			if(transform.GetChild(i).name != "Calendula"){
				transform.GetChild(i).GetComponent<SoundObject>().RandomChangeClip();
				SoundObject so = transform.GetChild(i).GetComponent<SoundObject>();
				so.SetVolume(v);
			}}
	}

	public void PitchControl(){
		string soundName="Calendula";
		float v= 1;
		for(int i=0; i<transform.childCount;i++){
			if(soundName == transform.GetChild(i).name){				
				//transform.GetChild(i).GetComponent<SoundObject>().RandomChangeClip();
				SoundObject so = transform.GetChild(i).GetComponent<SoundObject>();
				so.SetPitch(v);
			}}
	}


	public void PlayButtonSound(string button){
		if (button.Equals("PopUpButtonOk") || button.Equals("ExitButton") || button.Equals("Stay")|| button.Equals("Leave")) {
			switch (button) {
				case "PlayButton":
				case "ConfigButton":
				case "OnlineButton":
				case "CreditsButton": 
				case "VideoConfig":
				case "AudioConfig":
				case "LanguageConfig":
				case "ControlConfig":
				case "NewGameButton":
				case "LoadGameButton":
					PlayRandomSound ("MenuClick", false);
					break;
				case "BackwardButton":
					PlayRandomSound ("BackClick", false);
					break;
				default:
					PlayRandomSound ("ConfigClick", false);
					break;
			}
		}

	}

	public void PlayButtonHoover(string button){
		switch (button) {
		case "PlayButton": case "ConfigButton": case "OnlineButton": case "CreditsButton": 
		case "VideoConfig": case "AudioConfig": case "LanguageConfig": case "ControlConfig":
		case "NewGameButton": case "LoadGameButton":
			PlayRandomSound ("GeneralHoover", false);
			break;
		default:
			break;
		}
		
	}

	public void PopUpSound(){
		PlayRandomSound ("PopUpOpen", false);
	}

	public void PasswordSound(string function){
		switch (function) {
		case "open":
			PlayRandomSound ("PasswordOpen", false);
			break;
		case "error":
			PlayRandomSound ("PasswordError", false);
			break;
		}
	}

	public void MenuEye(string function){
		switch (function) {
		case "hoover":
			PlayRandomSound ("MenuEyeHoover", false);
			break;
		case "click":
			PlayRandomSound ("MenuEyeClick", false);
			break;
		}
	}

	public void ChatSounds(string function){
		switch (function) {
		case "sent":
			PlayRandomSound ("ChatMessageSent", false);
			break;
		case "received":
			PlayRandomSound ("ChatMessageSent", false);
			break;
		}
	}

	public void CenterScreenSound(string function){
		switch (function) {
		case "move":
			PlayRandomSound ("MoveCenterScreen", false);
			break;
		case "fix":
			PlayRandomSound ("FixCenterScreen", false);
			break;
		}
	}

	public void Liquid(string function){
		switch (function) {
		case "play":
			PlayRandomSound ("LiquidSaturation", true);
			break;
		case "stop":
			StopSound ("LiquidSaturation");
			break;
		}
	}

	public void ModifyLiquidSaturation(){
		PlayRandomSound ("ModifyLiquid", false);
	}

	public void DoubleMouse(){
		PlayRandomSound ("DoubleMouse", false);
	}

	public void Linterna(string function){
		switch (function) {
		case "play":
			PlayRandomSound ("Linterna", true);
			break;
		case "stop":
			StopSound ("Linterna");
			break;
		}
	}

	public void EyeFoundSound(){
		PlayRandomSound ("EyeFound", false);
	}

	public void WhiteNoise(string function){
		switch (function) {
		case "play":
			PlayRandomSound ("Noise", true);
			break;
		case "stop":
			StopSound ("Noise");
			break;
		}
	}

	public void ModifyFPS(){
		PlayRandomSound ("FPS", false);
	}

	public void LanguageSound(){
		PlayRandomSound ("Language", false);
	}

	public void ShortNoise(){
		PlayRandomSound ("ShortNoise", false);
	}

	public void Credits(string function){
		switch (function) {
		case "ok":
			PlayRandomSound ("CreditsOk", false);
			break;
		case "error":
			PlayRandomSound ("CreditsError", false);
			break;
		}
	}
	
	public void PlayMainSound(string soundName, bool loop){
		
		for(int i=0; i<transform.childCount;i++){
			if(soundName == transform.GetChild(i).name){			
///////////////				transform.GetChild(i).GetComponent<SoundObject>().PlayClip(GameObject.FindGameObjectWithTag("gmgr").GetComponent<GMGRScript>().nextLevel-1);
				SoundObject so = transform.GetChild(i).GetComponent<SoundObject>();
				
				/*if(so.GetAudio().isPlaying){
					return;
				}*/
				
				
				so.SetLoop(loop);
				so.Play();
				return;
				
			}
		}
		
		return;
		
	}
	
	public void MainSong(){
		//PlayRandomSound ("Calendula", true);
		PlayMainSound ("Calendula", true);
	}
	
	public void Intro(){
		PlayRandomSound ("Intro", false);
	}
	
	public void InvertedCalendula(){
		PlaySound("Calendula", false);
		//float v= -2f;
		for(int i=0; i<transform.childCount;i++){
			if("Calendula" == transform.GetChild(i).name){				
				MainSong();
				//transform.GetChild(i).GetComponent<SoundObject>().RandomChangeClip();
				SoundObject so = transform.GetChild(i).GetComponent<SoundObject>();
				so.Play	();
				so.SetLoop(true);
				//so.StartAt(30.0f);
				
				//so.SetPitch(v);
			}}
		
	}
	
	public void StopInvert(){
			for (int i=0; i<transform.childCount; i++) {
				if ("Calendula" == transform.GetChild (i).name) {
					SoundObject so = transform.GetChild (i).GetComponent<SoundObject> ();
					so.SetPitch(1.0f);
					if (so.GetAudio ().isPlaying) {
						so.Stop();
					}
				}
			}
	}
	
	public void Blooming(){
		PlayRandomSound ("Blooming", false);
	}
	
	public void GlitchEndScene(){
		PlaySound("Glitch", false);
	}

	
	public void BeepContinuo(){
		PlaySound("Beep", true);
	}

	public void OnlineMessage(){
		PlayRandomSound("OnlineMessage", false);
	}

	public void OnlineDisconnect(){
		PlaySound("OnlineDisconnect", false);
	}

	public void Resolution(){
		PlayRandomSound("Resolution", false);
	}
	
	public void LerpPan(SoundObject aud, float init, float fin, float multi){		
		StartCoroutine(CorLerpPan(aud, init, fin, multi));	
	}
	
	public void LerpPitch(SoundObject aud, float init, float fin, float multi){		
		StartCoroutine(CorLerpPitch(aud, init, fin, multi));	
	}
	
	public void LerpVolume(SoundObject aud, float init, float fin, float multi){		
		StartCoroutine(CorLerpVol(aud, init, fin, multi));	
	}
	
	IEnumerator CorLerpPan(SoundObject aud, float init, float fin, float multi){				
		if (init < fin){		
			while(init<fin){
				init+=Time.deltaTime*multi;
				aud.GetComponent<AudioSource>().panStereo = init;
				yield return null;
			}
		}else{			
			while(init>fin){
				init-=Time.deltaTime*multi;
				aud.GetComponent<AudioSource>().panStereo = init;
				yield return null;
			}
		}
	}
	
	IEnumerator CorLerpVol(SoundObject aud, float init, float fin, float multi){				
		if (init < fin){		
			while(init<fin){
				init+=Time.deltaTime*multi;
				aud.GetComponent<AudioSource>().volume = init;
				yield return null;
			}
		}else{			
			while(init>fin){
				init-=Time.deltaTime*multi;
				aud.GetComponent<AudioSource>().volume = init;
				yield return null;
			}
		}
	}
	
	IEnumerator CorLerpPitch(SoundObject aud, float init, float fin, float multi){		
		if (init < fin){			
			while(init<fin){
				init+=Time.deltaTime*multi;
				aud.GetComponent<AudioSource>().pitch = init;
				yield return null;
			}
		}else{		
			while(init>fin){
				init-=Time.deltaTime*multi;
				aud.GetComponent<AudioSource>().pitch = init;
				yield return null;
			}
		}
	}

	public void WaypointRotation(){
		PlayRandomSound ("WaypointRotation", false);
	}

	public void SplitSound(){
		PlayRandomSound ("Split", false);
	}

	public void LowNoise(){
		PlayRandomSound ("LowNoise", false);
	}

	public void Glitch3D(){
		PlayRandomSound ("Glitch3D", false);
	}
	
	





}
