﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace RythmGame.UiComponents
{
    public class LabelListOverflow
    {
        private List<Label> labels;
        private List<Label> markedForRemoval;
        private Vector2 position;
        private double visibilityTime;
        public Color LabelsColor = Color.Black;

        public LabelListOverflow(Vector2 position, double visibilityTime = 2000)
        {
            labels = new List<Label>();
            markedForRemoval = new List<Label>();
            this.position = position;
            this.visibilityTime = visibilityTime;
        }

        public void AddLabel(string text)
        {
            Label l1 = new Label(text, position, visibilityTime) { LabelColor = LabelsColor };
            l1.TimedLabelHidden += RemoveLabel;

            labels.ForEach(label => {
                label.Position.Y = label.Position.Y - 20;
                label.Alpha = (byte)(label.Alpha * 0.8);
                });
            labels.Add(l1);
        }

        public void Draw()
        {
            labels.ForEach(label => label.Draw());
        }

        public void Reset()
        {
            labels.Clear();
            markedForRemoval.Clear();
        }

        private void RemoveLabel(object sender, EventArgs e)
        {
            markedForRemoval.Add((Label)sender);
        }

        public void Update()
        {
            if(markedForRemoval.Count > 0)
            {
                markedForRemoval.ForEach(removeLable => labels.Remove(removeLable));
                markedForRemoval.Clear();
            }

            labels.ForEach(label => label.Update());

        }
    }
}
