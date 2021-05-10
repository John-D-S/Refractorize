using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

namespace Cody
{
    public class MenuHandler : MonoBehaviour
    {
        public Resolution[] resolutions;
        public TMP_Dropdown resolutionDropdown;
        public AudioMixer masterAudio;
        public GameObject PauseMenu;

        private bool paused = false;

        public void ChangeVolume(float volume)
        {
            masterAudio.SetFloat("volume", volume);
        }

        public void Pause()
        {
            Time.timeScale = 0;
            if (PauseMenu)
                PauseMenu.SetActive(true);
            paused = true;
        }

        public void Unpause()
        {
            paused = false;
            if (PauseMenu)
                PauseMenu.SetActive(false);
            Time.timeScale = 1;
        }

        public void ToggleMute(bool isMuted)
        {
            if (isMuted)
            {
                masterAudio.SetFloat("isMutedVolume", -80);

            }

            else
            {
                masterAudio.SetFloat("isMutedVolume", 0);
            }
        }

        public void LoadScene(int scene)
        {
            SceneManager.LoadScene(scene);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        private void Start()
        {
            resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();
            List<string> options = new List<string>();
            int currentResolutionIndex = 0;
            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + "x" + resolutions[i].height;
                options.Add(option);
                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = 1;
                }
            }
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }


        public void SetResolution(int ResolutionIndex)
        {
            Resolution res = resolutions[ResolutionIndex];
            Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        }

        public void SetFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;

        }

        private void Update()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                if (!paused)
                    Pause();
                else
                    Unpause();
            }
        }
    }
}