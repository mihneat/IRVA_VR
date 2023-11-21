using L6_GoogleCardboard.Scripts.Helpers;
using TMPro;
using UnityEngine;

namespace L6_GoogleCardboard.Scripts
{
    /// <summary>
    /// Used to keep track of game score & update world space UI canvas.
    /// </summary>
    public class ScoreController : Singleton<ScoreController>
    {
       [SerializeField] private TextMeshProUGUI scoreText;

       private int _score = 0;

       public void IncrementScore()
       {
           _score++;
           
           // TODO 5.1 : Set the score text.
       }

       public void DecrementScore()
       {
           // TODO 5.2 : Implement score modification & set the score text.
       }
    }
}