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

    private void Start()
    {
        panel = GetComponent<RawImage>();
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
        videoPrincipalCorrida.Play();
        panel.texture = videoPrincipalCorridaTexture;
        ilhaOptions.SetActive(false);
        corridaOptions.SetActive(true);
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
        videoPrincipalIlha.Play();
        panel.texture = videoPrincipalIlhaTexture;
        corridaOptions.SetActive(false);
        ilhaOptions.SetActive(true);
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
        videoPrincipalCorrida.Play();
        panel.texture = videoPrincipalCorridaTexture;
        ilhaOptions.SetActive(false);
        corridaOptions.SetActive(true);
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
        panel.texture = videoDriftTexture;
        videoDrift.Play();
        zerinhoCarrao.image.sprite = zerinhoCarraoChoosen;
        DisableButtonsEvent();
    }
    private void OnVoltaOlhosVendadosChoose()
    {
        StopAllVideos();
        ilhaOptions.SetActive(false);
        corridaOptions.SetActive(true);
        panel.texture = videoCorridaVendadoTexture;
        videoCorridaVendado.Play();
        voltaRapidaVendado.image.sprite = voltaRapidaVendadoChoosen;
        DisableButtonsEvent();
    }


    private void OnCurtirIlhaSuaChoose()
    {
        StopAllVideos();
        corridaOptions.SetActive(false);
        ilhaOptions.SetActive(true);
        panel.texture = videoComprarIlhaTexture;
        videoComprarIlha.Play();
        curtirIlhaSua.image.sprite = curtirIlhaSuaChoosen;
        DisableButtonsEvent();
    }

    private void OnGolfinhosChoose()
    {
        StopAllVideos();
        corridaOptions.SetActive(false);
        ilhaOptions.SetActive(true);
        panel.texture = videoNadarGolfinhoTexture;
        videoNadarGolfinho.Play();
        mergulharGolfinhos.image.sprite = mergulharGolfinhosChoosen;
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


    private void SaveLog()
    {
        DataLog dataLog = LogUtil.GetDatalogFromJson();
        dataLog.status = StatusEnum.Jogou.ToString();
        dataLog.additional = "vazio";
        LogUtil.SaveLog(dataLog);
    }

}
