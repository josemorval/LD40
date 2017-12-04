using UnityEngine;
using System.Collections;

public class SoundObject : MonoBehaviour{

	public AudioClip[] listAudios;
	public float[] signalSpectrum;
	public int windowSignalSize;

	int currentClip;
	

	public void Initialize(AudioClip[] listAudios, int windowSignalSize, bool playOnAwake){

		this.listAudios = listAudios;
		this.windowSignalSize = windowSignalSize;
		this.signalSpectrum = new float[this.windowSignalSize];

		int actualClip = 0;

		gameObject.AddComponent<AudioSource>();
		GetComponent<AudioSource>().clip = listAudios[actualClip];
		GetComponent<AudioSource>().playOnAwake = playOnAwake;
	}

	void ComputeSpectrum(int channel){
		signalSpectrum = GetComponent<AudioSource>().GetSpectrumData(windowSignalSize, channel, FFTWindow.BlackmanHarris);

	}


	//Un random normalito para cambiar entre la lista de audios
	public void RandomChangeClip(){
		
		if(listAudios.Length==1){
			return;
		}
		
		int r = Mathf.FloorToInt(Random.Range(0,listAudios.Length));
		
		if(r==listAudios.Length){
			r=listAudios.Length - 1;
		}
		
		GetComponent<AudioSource>().clip = listAudios[r];
		
	}
	
	public void PlayClip(int i){
		GetComponent<AudioSource>().clip = listAudios[i];
		
	}

	public void ChangeNextClip(){

		if(listAudios.Length==1){
			return;
		}

		currentClip++;
		currentClip %= listAudios.Length;

		GetComponent<AudioSource>().clip = listAudios[currentClip];
	}

	public void Play(){
		GetComponent<AudioSource>().Play();
	}

	public void Stop(){
		GetComponent<AudioSource>().Stop ();
	}

	public void SetVolume(float vol){
		GetComponent<AudioSource>().volume = vol/10f;
	}

	public void SetPitch(float pitch){
		GetComponent<AudioSource>().pitch = pitch;
	}

	public float GetMaxValueSpectrum(){
		if(!GetComponent<AudioSource>().isPlaying){
			return 0.0f;
		}else{
			
			float max = 0.0f;
			
			for(int i=0;i<signalSpectrum.Length;i++){
				max = Mathf.Max(max,signalSpectrum[i]);
			}
			
			return max;
			
		}
		
	}

	public float GetMinValueSpectrum(){
		if(!GetComponent<AudioSource>().isPlaying){
			return 0.0f;
		}else{
			
			float min = 0.0f;
			
			for(int i=0;i<signalSpectrum.Length;i++){
				min = Mathf.Min(min,signalSpectrum[i]);
			}
			
			return min;
			
		}
		
	}

	public float[] GetSpectrum(){

		return signalSpectrum;

	}

	public void SetLoop(bool loop){
		GetComponent<AudioSource>().loop = loop;
	}

	public AudioSource GetAudio(){
		return GetComponent<AudioSource>();
	}
	
	public void StartAt(float s){
		GetComponent<AudioSource>().time=s;
	}







	


}
