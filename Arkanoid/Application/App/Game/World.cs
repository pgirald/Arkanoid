using Arkanoid.Application.App.Game.Scenarios.ScenarioStates;
using Arkanoid.Application.App.Graphics;
using Arkanoid.Application.App.Graphics.Textures.Blocks;
using Arkanoid.Application.Utils.Components;
using Arkanoid.Application.Utils.Game;
using Arkanoid.Application.Utils.Textures;
using System;
using System.Collections.Generic;

namespace Arkanoid.Application.App.Game
{
    public class World : Container, IDrawable
    {
        private List<ArkanoidScenario> scenarios;

        private ArkanoidScenario current;

        private int scenarioIndex = 1;

        public IEnumerable<TextureComponent> Textures => current.Textures;

        public World(float width, float height)
        {
            scenarios = new List<ArkanoidScenario>();
            Width = width;
            Height = height;
        }

        private void OnNextConfirmed(object sender, EventArgs args)
        {
            ((NextStage)sender).NextStageConfirmed -= OnNextConfirmed;
            current = scenarios[scenarioIndex++];
        }

        public void Initialize()
        {
            ArkanoidScenario first = new ArkanoidScenario(CreateSimpleBlocks(this),Width,Height);
            ArkanoidScenario second = new ArkanoidScenario(CreateSimpleBlocks(this), Width, Height);
            ArkanoidScenario final = new ArkanoidScenario(CreateSimpleBlocks(this), Width, Height);
            first.InitialState = new GameStart(first);
            ((NextStage)first.FinalState).NextStageConfirmed += OnNextConfirmed;
            ((NextStage)second.FinalState).NextStageConfirmed += OnNextConfirmed;
            final.FinalState = new GameFinished(final);
            scenarios.Add(first);
            scenarios.Add(second);
            scenarios.Add(final);
            current = first;
            foreach (ArkanoidScenario scenario in scenarios)
            {
                scenario.Initialize();
            }
        }

        public void RunCurrent(GameInfo info)
        {
            current.Run(info);
        }

        private BlockSet CreateSimpleBlocks(Container parent)
        {
            BlockSet blocks = new BlockSet();
            for (int i = 1; i > 0; i--)
            {
                for (int j = 1; j > 0; j--)
                {
                    blocks.AddBlock<Block>();
                }
                blocks.next();
            }
            blocks.Align(parent, Alignment.TopRight);
            return blocks;
        }
    }
}
