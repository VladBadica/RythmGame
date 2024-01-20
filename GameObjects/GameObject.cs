using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RythmGame.GameObjects
{
    public abstract class GameObject
    {

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void Initialize();

        public abstract void LoadContent();

        public abstract void Update(GameTime gameTime);
    }
}
