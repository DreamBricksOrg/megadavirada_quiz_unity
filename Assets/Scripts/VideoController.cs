using System.Collections;
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
    public Button mergulharGolfinhos;
    public GameObject corridaOptions;
    public Button zerinhoCarrao;
    public Button voltaRapidaVendado;

    [Header("Cores")]
    public Sprite mergulharGolfinhosChoosen;
    public Sprite curtirIlhaSuaChoosen;
    public Sprite zerinhoCarraoChoosen;
    public Sprite voltaRapidaVendadoChoosen;
    public Sprite mergulharGolfinhosOriginal;
    public Sprite curtirIlhaSuaOriginal;
    public Sprite zerinhoCarraoOriginal;
    public Sprite voltaRapidaVendadoOriginal;


    public List<VideoPlayer> videos;
    public List<Button> buttons;

    public string lastPlayed;
    public GameObject barLoadAnimator;

    public int videoPrincipalIlhaPlayCount = 0;

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
        StartCoroutine(AnimateButtonSize(zerinhoCarrao));
        StartCoroutine(AnimateButtonSize(voltaRapidaVendado));
        barLoadAnimator.gameObject.SetActive(true);
    }

    private void OnVideooCorridaVendadoEnd(VideoPlayer vp)
    {
        PlayVideoPrincipalIlha();
        voltaRapidaVendado.image.sprite = voltaRapidaVendadoOriginal;
        ConfigureButtonsEvents();
    }

    private void OnVideoDriftEnd(VideoPlayer vp)
    {
        PlayVideoPrincipalIlha();
        zerinhoCarrao.image.sprite = zerinhoCarraoOriginal;
        ConfigureButtonsEvents();
    }

    private void OnVideoPrincipalIlhaEnd(VideoPlayer vp)
    {
        StartCoroutine(AnimateButtonSize(curtirIlhaSua));
        StartCoroutine(AnimateButtonSize(mergulharGolfinhos));
        barLoadAnimator.gameObject.SetActive(true);
    }

    private void OnVideoComprarIlhaEnd(VideoPlayer vp)
    {
        PlayVideoPrincipalCorrida();
        curtirIlhaSua.image.sprite = curtirIlhaSuaOriginal;
        ConfigureButtonsEvents();
    }

    private void OnVideoNadarGolfinhoEnd(VideoPlayer vp)
    {
        PlayVideoPrincipalCorrida();
        mergulharGolfinhos.image.sprite = mergulharGolfinhosOriginal;
        ConfigureButtonsEvents();
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
        SaveLog("Dar zerinhos com o carrão");
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
        SaveLog("Uma volta rápida com os olhos vendados");
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
        SaveLog("Curtir uma ilha toda sua");
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
        SaveLog("Mergulhar com golfinhos");
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
        videoPrincipalIlhaPlayCount++;
        CheckVideoPrincipalIlhaPlayCount();
        videoPrincipalCorrida.targetTexture.Release();
        videoPrincipalCorrida.enabled = false;
        videoPrincipalCorrida.enabled = true;
        videoPrincipalCorrida.Play();
        panel.texture = videoPrincipalCorridaTexture;
        ilhaOptions.SetActive(false);
        corridaOptions.SetActive(true);
        lastPlayed = "VideoPrincipalCorrida";
    }

    private void CheckVideoPrincipalIlhaPlayCount()
    {
        if (videoPrincipalIlhaPlayCount >= 8)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void SaveLog(string additional)
    {
        DataLog dataLog = LogUtil.GetDatalogFromJson();
        dataLog.status = StatusEnum.Jogou.ToString();
        dataLog.additional = additional;
        LogUtil.SaveLog(dataLog);
    }

    private IEnumerator AnimateButtonSize(Button button)
    {
        float duration = 0.5f; // Duração da animação (segundos)
        RectTransform buttonTransform = button.GetComponent<RectTransform>();

        Vector2 originalSize = buttonTransform.sizeDelta; // Tamanho original do botão
        Vector2 targetSize = originalSize * 1.2f; // Tamanho aumentado (120% do original)

        // Executar a animação 3 vezes
        for (int i = 0; i < 3; i++)
        {
            // Crescendo o botão
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                float normalizedTime = t / duration;
                Vector2 newSize = Vector2.Lerp(originalSize, targetSize, normalizedTime);
                buttonTransform.sizeDelta = newSize;
                yield return null;
            }

            // Garante que o tamanho final seja o tamanho alvo
            buttonTransform.sizeDelta = targetSize;

            // Retornando ao tamanho original
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                float normalizedTime = t / duration;
                Vector2 newSize = Vector2.Lerp(targetSize, originalSize, normalizedTime);
                buttonTransform.sizeDelta = newSize;
                yield return null;
            }

            // Garante que o tamanho final seja o tamanho original
            buttonTransform.sizeDelta = originalSize;
        }
    }

    public void AnimateSpecificButton(Button button)
    {
        StartCoroutine(AnimateButtonSize(button));
    }
}
