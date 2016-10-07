﻿//Copyright (c) 2007-2016 ppy Pty Ltd <contact@ppy.sh>.
//Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Framework.GameModes;
using osu.Framework.Graphics.Transformations;
using osu.Game.Graphics.Background;
using OpenTK;

namespace osu.Game.GameModes
{
    public abstract class BackgroundMode : GameMode, IEquatable<BackgroundMode>
    {
        public virtual bool Equals(BackgroundMode other)
        {
            return other?.GetType() == GetType();
        }

        const float transition_length = 500;
        const float x_movement_amount = 50;

        public override void Load()
        {
            base.Load();

            Content.Scale *= 1 + (x_movement_amount / Size.X) * 2;
        }

        protected override void OnEntering(GameMode last)
        {
            Content.FadeOut();
            Content.MoveToX(x_movement_amount);

            Content.FadeIn(transition_length, EasingTypes.InOutQuart);
            Content.MoveToX(0, transition_length, EasingTypes.InOutQuart);

            base.OnEntering(last);
        }

        protected override void OnSuspending(GameMode next)
        {
            Content.MoveToX(-x_movement_amount, transition_length, EasingTypes.InOutQuart);
            base.OnSuspending(next);
        }

        protected override void OnExiting(GameMode next)
        {
            Content.FadeOut(transition_length, EasingTypes.OutExpo);
            Content.MoveToX(x_movement_amount, transition_length, EasingTypes.OutExpo);

            base.OnExiting(next);
        }

        protected override void OnResuming(GameMode last)
        {
            Content.MoveToX(0, transition_length, EasingTypes.OutExpo);
            base.OnResuming(last);
        }
    }

    public class BackgroundModeDefault : BackgroundMode
    {
        public override void Load()
        {
            base.Load();

            Add(new Background());
        }
    }

    public class BackgroundModeCustom : BackgroundMode
    {
        private readonly string textureName;

        public BackgroundModeCustom(string textureName)
        {
            this.textureName = textureName;
        }

        public override void Load()
        {
            base.Load();
            Add(new Background(textureName));
        }

        public override bool Equals(BackgroundMode other)
        {
            return base.Equals(other) && textureName == ((BackgroundModeCustom)other).textureName;
        }
    }
}
