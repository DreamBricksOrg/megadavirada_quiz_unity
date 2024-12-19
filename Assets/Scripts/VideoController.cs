using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    private RawImage panel;

    [Header("Texturas de Vídeo")]
    public Texture videoPrincipalCorridaTexture;
    public Texture videoCorridaVendadoTexture;
    public Texture videoDriftTexture;
    public Texture videoPrincipalIlhaTexture;
    public Texture videoComprarIlhaTexture;
    public Texture videoNadarGolfinhoTexture;

    [Header("Vídeo Players")]
    public VideoPlayer videoPrincipalCorrida;
    public VideoPlayer videoCorridaVendado;
    public VideoPlayer videoDrift;
    public VideoPlayer videoPrincipalIlha;
    public VideoPlayer videoComprarIlha;
    public VideoPlayer videoNadarGolfinho;

    [Header("Opções de Interface")]
    public GameObject ilhaOptions;
    public Button curtirIlhaSua;
    public Sprite curtirIlhaSuaChoosen;
    public Button mergulharGolfinhos;
    public Sprite mergulharGolfinhosChoosen;
    public GameObject corridaOptions;
    public Button zerinhoCarrao;
    public Sprite zerinhoCarraoChoosen;
    public Button voltaRapidaVendado;
    public Sprite voltaRapidaVendadoChoosen;


    public List<VideoPlayer> videos;
    public List<Button> buttons;

    public string lastPlayed;
    public GameObject barLoadAnimator;

    private void Start()
    {
        panel = GetComponent<RawImage>();
        barLoadAnimator.gameObject.SetActive(false);
        lastPlayed = "";
        ConfigureVideoEvents();
        InitialState();
        ConfigureButtonsEvents();
    }

    private void ConfigureButtonsEvents()
    {
        curtirIlhaSua.onClick.AddListener(() => OnCurtirIlhaSuaChoose());
        mergulharGolfinhos.onClick.AddListener(() => OnGolfinhosChoose());
        zerinhoCarrao.onClick.AddListener(() => OnZerinhosCarraoChoose());
        voltaRapidaVendado.onClick.AddListener(() => OnVoltaOlhosVendadosChoose());
    }

    private void ConfigureVideoEvents()
    {
        RegisterVideoEndEvent(videoPrincipalCorrida, OnVideoPrincipalCorridaEnd);
        RegisterVideoEndEvent(videoCorridaVendado, OnVideooCorridaVendadoEnd);
        RegisterVideoEndEvent(videoDrift, OnVideoDriftEnd);
        RegisterVideoEndEvent(videoPrincipalIlha, OnVideoPrincipalIlhaEnd);
        RegisterVideoEndEvent(videoComprarIlha, OnVideoComprarIlhaEnd);
        RegisterVideoEndEvent(videoNadarGolfinho, OnVideoNadarGolfinhoEnd);
    }

    private void InitialState()
    {
        PlayVideoPrincipalCorrida();
    }

    private void RegisterVideoEndEvent(VideoPlayer videoPlayer, VideoPlayer.EventHandler handler)
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += handler;
        }
        else
        {
            Debug.LogError($"VideoPlayer não atribuído para {handler.Method.Name}.");
        }
    }

    private void OnVideoPrincipalCorridaEnd(VideoPlayer vp)
    {
        barLoadAnimator.gameObject.SetActive(true);
    }

    private void OnVideooCorridaVendadoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void OnVideoDriftEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene("SampleScene");

    }

    private void OnVideoPrincipalIlhaEnd(VideoPlayer vp)
    {
        barLoadAnimator.gameObject.SetActive(true);
    }

    private void OnVideoComprarIlhaEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void OnVideoNadarGolfinhoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene("SampleScene");
    }


    private void OnZerinhosCarraoChoose()
    {
        StopAllVideos();
        ilhaOptions.SetActive(false);
        corridaOptions.SetActive(true);
        videoDrift.targetTexture.Release();
        videoDrift.Play();
        panel.texture = videoDriftTexture;
        zerinhoCarrao.image.sprite = zerinhoCarraoChoosen;
        barLoadAnimator.gameObject.SetActive(false);
        DisableButtonsEvent();
    }

    private void OnVoltaOlhosVendadosChoose()
    {
        StopAllVideos();
        ilhaOptions.SetActive(false);
        corridaOptions.SetActive(true);
        videoCorridaVendado.targetTexture.Release();
        videoCorridaVendado.Play();
        panel.texture = videoCorridaVendadoTexture;
        voltaRapidaVendado.image.sprite = voltaRapidaVendadoChoosen;
        barLoadAnimator.gameObject.SetActive(false);
        DisableButtonsEvent();
    }


    private void OnCurtirIlhaSuaChoose()
    {
        StopAllVideos();
        corridaOptions.SetActive(false);
        ilhaOptions.SetActive(true);
        videoComprarIlha.targetTexture.Release();
        videoComprarIlha.Play();
        panel.texture = videoComprarIlhaTexture;
        curtirIlhaSua.image.sprite = curtirIlhaSuaChoosen;
        barLoadAnimator.gameObject.SetActive(false);
        DisableButtonsEvent();
    }

    private void OnGolfinhosChoose()
    {
        StopAllVideos();
        corridaOptions.SetActive(false);
        ilhaOptions.SetActive(true);
        videoNadarGolfinho.targetTexture.Release();
        videoNadarGolfinho.Play();
        panel.texture = videoNadarGolfinhoTexture;
        mergulharGolfinhos.image.sprite = mergulharGolfinhosChoosen;
        barLoadAnimator.gameObject.SetActive(false);
        DisableButtonsEvent();
    }

    private void StopAllVideos()
    {
        foreach (var video in videos)
        {
            video.Stop();
        }
    }

    private void DisableButtonsEvent()
    {
        foreach (var button in buttons)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    public void PlayVideoPrincipalIlha()
    {
        videoPrincipalIlha.targetTexture.Release();
        videoPrincipalIlha.enabled = false;
        videoPrincipalIlha.enabled = true;
        videoPrincipalIlha.Play();
        panel.texture = videoPrincipalIlhaTexture;
        corridaOptions.SetActive(false);
        ilhaOptions.SetActive(true);
        lastPlayed = "VideoPrincipalIlha";
    }

    public void PlayVideoPrincipalCorrida()
    {
        videoPrincipalCorrida.targetTexture.Release();
        videoPrincipalCorrida.enabled = false;
        videoPrincipalCorrida.enabled = true;
        videoPrincipalCorrida.Play();
        panel.texture = videoPrincipalCorridaTexture;
        ilhaOptions.SetActive(false);
        corridaOptions.SetActive(true);
        lastPlayed = "VideoPrincipalCorrida";
    }


    private void SaveLog()
    {
        DataLog dataLog = LogUtil.GetDatalogFromJson();
        dataLog.status = StatusEnum.Jogou.ToString();
        dataLog.additional = "vazio";
        LogUtil.SaveLog(dataLog);
    }

}
