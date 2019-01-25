﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game.Configuration;
using osu.Game.Graphics.UserInterface;

namespace osu.Game.Overlays.Settings.Sections.Gameplay
{
    public class SongSelectSettings : SettingsSubsection
    {
        protected override string Header => "Song Select";

        [BackgroundDependencyLoader]
        private void load(GameConfigManager config)
        {
            Children = new Drawable[]
            {
                new SettingsCheckbox
                {
                    LabelText = "Right mouse drag to absolute scroll",
                    Bindable = config.GetBindable<bool>(GameSetting.SongSelectRightMouseScroll),
                },
                new SettingsCheckbox
                {
                    LabelText = "Show converted beatmaps",
                    Bindable = config.GetBindable<bool>(GameSetting.ShowConvertedBeatmaps),
                },
                new SettingsSlider<double, StarSlider>
                {
                    LabelText = "Display beatmaps from",
                    Bindable = config.GetBindable<double>(GameSetting.DisplayStarsMinimum),
                    KeyboardStep = 0.1f
                },
                new SettingsSlider<double, StarSlider>
                {
                    LabelText = "up to",
                    Bindable = config.GetBindable<double>(GameSetting.DisplayStarsMaximum),
                    KeyboardStep = 0.1f
                },
                new SettingsEnumDropdown<RandomSelectAlgorithm>
                {
                    LabelText = "Random selection algorithm",
                    Bindable = config.GetBindable<RandomSelectAlgorithm>(GameSetting.RandomSelectAlgorithm),
                }
            };
        }

        private class StarSlider : OsuSliderBar<double>
        {
            public override string TooltipText => Current.Value.ToString(@"0.## stars");
        }
    }
}
