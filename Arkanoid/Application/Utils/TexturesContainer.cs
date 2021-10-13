using System.Collections;
using System.Collections.Generic;

namespace Arkanoid.Application.Utils
{
    public class TexturesContainer : Container, ITexturesContainer
    {
        public TexturesContainer()
        {
            _textures = new TexturesEnumerable(_childs);
        }

        private TexturesEnumerable _textures;
        //If necesary, you can create a classes which wrappes LinkedList<Component> to
        //let TexturesContainer classes to manipulate its textures as they needit

        protected sealed override LinkedListNode<Component> _addChildSecretly(Component child)
        {
            return base._addChildSecretly((TextureComponent)child);
        }

        public override void AddChild(Component child)
        {
            base.AddChild((TextureComponent)child);
        }

        public void AddChild(TextureComponent child)
        {
            AddChild((Component)child);
        }

        public void AddChilds(IEnumerable<TextureComponent> childs)
        {
            AddChilds((IEnumerable<Component>)childs);
        }

        public virtual IEnumerable<TextureComponent> Textures => _textures;

        public class TexturesEnumerator : IEnumerator<TextureComponent>
        {
            public TexturesEnumerator(LinkedList<Component> childs)
            {
                _childs = childs;
            }

            private LinkedList<Component> _childs;

            private LinkedListNode<Component> _currentNode;

            public TextureComponent Current => (TextureComponent)_currentNode.Value;

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                _currentNode = _currentNode == null ? _childs.First : _currentNode.Next;
                return _currentNode != null;
            }

            public void Reset()
            {
                _currentNode = _childs.First;
            }

            public void Dispose()
            {
            }
        }

        public class TexturesEnumerable : IEnumerable<TextureComponent>
        {
            public TexturesEnumerable(LinkedList<Component> childs)
            {
                _enumerator = new TexturesEnumerator(childs);
            }

            private TexturesEnumerator _enumerator;

            public IEnumerator<TextureComponent> GetEnumerator()
            {
                return _enumerator;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}
