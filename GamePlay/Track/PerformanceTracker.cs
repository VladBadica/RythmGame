using System;
using System.Collections.Generic;
using System.Linq;

namespace RythmGame.GamePlay.Track
{
    public class PerformanceTracker
    {

        private List<int> accuracyList;
        public string Accuracy
        {
            get
            {
                float x = 0;
                accuracyList.ForEach(acc => x += acc);
                if(accuracyList.Count == 0)
                {
                    return "0.00";
                }

                return (x / accuracyList.Count).ToString("0.00");
            }
        }

        private int perfectCombo;

        public PerformanceTracker()
        {
            accuracyList = new List<int>();
        }

        public void AddHitAccuracy(int trackBallWidth, int trackBallCenterX, int stepCenterX)
        {
            int trackBallHalfWidth = trackBallWidth / 2;
            // Add 1 to the end because trackball is 32 px and center X pixel is always rounded, so we are 1 pixel lenient of missClick - Can't compute exact center of Trackball
            int value = (trackBallHalfWidth + 1 - Math.Abs(trackBallCenterX - stepCenterX)) * 100 / trackBallHalfWidth;

            if (value > 75)
            {
                accuracyList.Add(100);
                perfectCombo++;
            }
            else if (value > 50)
            {
                accuracyList.Add(66);
                perfectCombo = 0;
            }
            else
            {
                accuracyList.Add(33);
                perfectCombo = 0;
            }
        }

        public int GetLastHitScore()
        {
            switch (accuracyList.Last())
            {
                case 100: return 300 * perfectCombo;
                case 66: return 100;
                case 30:
                default: return 50;
            }
        }

        public string GetLastHitInfoLabel()
        {
            switch (accuracyList.Last())
            {
                case 100: return $"Perfect x{perfectCombo}";
                case 66: return "Good";
                case 30:
                default: return "OK";
            }
        }

        public void Reset()
        {
            accuracyList.Clear();
            perfectCombo = 0;
        }
    }
}
