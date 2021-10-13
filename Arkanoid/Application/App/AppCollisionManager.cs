using Arkanoid.Application.App.Components.Textures.Blocks;
using Arkanoid.Application.Utils;
using System;
using System.Collections.Generic;

namespace Arkanoid.Application.App
{
    public class AppCollisionManager
    {
        public AppCollisionManager(ScenarioTemplate scenario)
        {
            Scenario = scenario;
            PaddleSubs = new LinkedList<CollideableComponent>();
            OwnContainerSubs = new LinkedList<CollideableComponent>();
            BlockSubs = new LinkedList<CollideableComponent>();
            MovingComponentsSubs = new LinkedList<CollideableComponent>();
            Subscriptions = new LinkedList<CollideableComponent>[4];
            Subscriptions[(int)Subscription.Paddle] = PaddleSubs;
            Subscriptions[(int)Subscription.Container] = OwnContainerSubs;
            Subscriptions[(int)Subscription.Block] = BlockSubs;
            Subscriptions[(int)Subscription.MovingComps] = MovingComponentsSubs;
        }

        private LinkedList<CollideableComponent>[] Subscriptions;

        public ScenarioTemplate Scenario { get; private set; }

        private LinkedList<CollideableComponent> PaddleSubs { get; }

        private LinkedList<CollideableComponent> OwnContainerSubs { get; }

        private LinkedList<CollideableComponent> BlockSubs { get; }

        private LinkedList<CollideableComponent> MovingComponentsSubs { get; }

        private Side? CompsCollised(TextureComponent comp1, TextureComponent comp2, Action<Side> action)
        {
            if (CollisionOps.AreIntersected(comp1, comp2))
            {
                action(CollisionOps.GetCollisionSide(comp1, comp2));
            }
            return null;
        }

        private Side GetOppsiteSide(Side side)
        {
            switch (side)
            {
                case Side.Right:
                    return Side.Left;
                case Side.Left:
                    return Side.Right;
                case Side.Top:
                    return Side.Bottom;
                default:
                    return Side.Top;
            }
        }

        private void CheckPaddleHit()
        {
            foreach (CollideableComponent component in PaddleSubs)
            {
                CompsCollised(component,
                    Scenario.Paddle,
                    side => component.OnPaddleHit(Scenario.Paddle, side));
            }
        }

        private void CheckContainerHit()
        {
            int side;
            foreach (CollideableComponent component in OwnContainerSubs)
            {
                side = CollisionOps.ContainerHit(component);
                if (side != -1)
                {
                    component.OnContainerHit((Side)side);
                }
            }
        }

        private void CheckBlockHit()
        {
            //Strange behaviour without foreach
            BlockSubs.ForAll(component => CheckBlocksHit(component.Value));
            /*foreach (CollideableComponent component in BlockSubs)
            {
                CheckBlocksHit(component);
            }*/
        }

        private void CheckBlocksHit(CollideableComponent component)
        {
            foreach (Block block in Scenario.Blocks.Textures)
            {
                CompsCollised(component,
                    block,
                    side =>
                    {
                        component.OnBlockHit(block, side);
                        block.Hit();
                    });
            }
        }

        private void CheckMovingComponents()
        {
            LinkedListNode<CollideableComponent> current = MovingComponentsSubs.First;
            LinkedListNode<CollideableComponent> next;
            do
            {
                while ((next = current.Next) != null)
                {
                    CompsCollised(current.Value,
                        next.Value,
                        side =>
                        {
                            current.Value.OnMovingComponentHit(next.Value, side);
                            next.Value.OnMovingComponentHit(current.Value, GetOppsiteSide(side));
                        });
                }
                current = current.Next;
            } while (current.Next != null);
        }

        public void CheckScenarioCollisions()
        {
            if (PaddleSubs.Count > 0)
            {
                CheckPaddleHit();
            }
            if (OwnContainerSubs.Count > 0)
            {
                CheckContainerHit();
            }
            if (BlockSubs.Count > 0)
            {
                CheckBlockHit();
            }
            if (MovingComponentsSubs.Count > 1)
            {
                CheckMovingComponents();
            }
        }

        public void Clear()
        {
            Scenario = null;
            foreach (Subscription subs in Enum.GetValues(typeof(Subscription)))
            {
                KickFromSubsbription(Subscriptions[(int)subs], subs);
            }
        }

        public void Subscribe(CollideableComponent component, Subscription subscription)
        {
            component.CollisionKeys[(int)subscription] = Subscriptions[(int)subscription].AddLast(component);
        }

        public void Unsuscribe(CollideableComponent component, Subscription subscription)
        {
            Subscriptions[(int)subscription].Remove(component.CollisionKeys[(int)subscription]);
            component.CollisionKeys[(int)subscription] = null;
        }

        private void KickFromSubsbription(LinkedList<CollideableComponent> subscriptors, Subscription subs)
        {
            subscriptors.ForAll(subscriptor => Unsuscribe(subscriptor.Value, subs));
        }
    }
}