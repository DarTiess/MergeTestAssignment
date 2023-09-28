using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Level
{
    [CreateAssetMenu(fileName = "LevelLoader", menuName = "LevelLoader", order = 51)]
    public class LevelLoader : ScriptableObject, ILevelLoader
    {
        public List<string> NameScene;

        public int NumLevel
        {
            get { return PlayerPrefs.GetInt("NumLevel"); }
            set { PlayerPrefs.SetInt("NumLevel", value); }
        }
        public int NumScene
        {                    
            get { return PlayerPrefs.GetInt("NumScene"); }
            set { PlayerPrefs.SetInt("NumScene", value); }
        }
        /// <summary>
        /// On Start game Load scene from saved
        /// </summary>

        public void StartGame()
        {
            if (NumLevel == 0) NumLevel = 1;
            if (NumScene == 0) NumScene = 1;
        
            LoadScene();    
        }
        /// <summary>
        /// Load next Level
        /// </summary>
        public void LoadNextLevel()
        {
            NumLevel += 1;
            NumScene += 1;
        
            LoadScene();           
        }
        /// <summary>
        /// Load Scene
        /// </summary>
        public void LoadScene()
        {
            int numLoadedScene = NumScene;
            if (numLoadedScene <= NameScene.Count){numLoadedScene -= 1;}
            if (numLoadedScene > NameScene.Count){numLoadedScene = (numLoadedScene - 1) % NameScene.Count;}
            Debug.Log("Load Scene Number " + numLoadedScene + "Level Number " + NumLevel);

            SceneManager.LoadScene(NameScene[numLoadedScene]);
        }
        /// <summary>
        /// Restart active scene
        /// </summary>
        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
