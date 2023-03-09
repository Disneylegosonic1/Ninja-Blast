using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaBlast
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D ninja;
        private Rectangle nPos;
        private Texture2D background;
        private Rectangle bgPos;
        private KeyboardState oldKB;
        private Texture2D ninjaL;
        private Texture2D ninjaR;
        private Texture2D star;
        private Texture2D fireball;
        private Rectangle sPos;
        private Rectangle fPos;
        private int velocity;
        private int velocity2;
        private int velocity3;
        private Rectangle bg;
        private Texture2D monster;
        private Rectangle mPos;
        Vector2 origin;
        SpriteFont _font;
        Vector2 _textPos;
        string _label;
        int _score;
        private int vertical;
        private Random rng = new Random();
        private int count;
        
   




        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferWidth = 1000;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();
            this.Window.AllowUserResizing = true;
            IsMouseVisible = true;
            nPos = new Rectangle(25, 470, 125, 125);
            bgPos = new Rectangle(0, 0, 1000, 600);
            oldKB = Keyboard.GetState();
            sPos = new Rectangle(1100, 1100, 50, 50);
            fPos = new Rectangle(1100, 1100, 100, 100);
            mPos = new Rectangle(1100, rng.Next(0, 400), 75, 75);
            bg = new Rectangle(0, 0, 1200, 400);
            velocity = 20;
            velocity2 = 2;
            _textPos = new Vector2(20, 20);
            _label = "Score: ";
            _score = 0;
            vertical = 20;
            velocity3 = 1;
            count = 3;

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ninja = Content.Load<Texture2D>("Ninja");
            background = Content.Load<Texture2D>("RedSky");
            ninjaL = Content.Load<Texture2D>("Ninja2");
            ninjaR = Content.Load<Texture2D>("Ninja");
            star = Content.Load<Texture2D>("Shuriken");
            fireball = Content.Load<Texture2D>("Fireball");
            monster = Content.Load<Texture2D>("Monster");
            _font = Content.Load<SpriteFont>("MyFont");
            origin = new Vector2(115, 135);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState kb = Keyboard.GetState();
            if (kb.IsKeyDown(Keys.Left) && !kb.IsKeyDown(Keys.Up) && !kb.IsKeyDown(Keys.Down) && nPos.X > -40)
            {
                ninja = ninjaL;
                nPos.X -= 5;
                /*if (sPos.X > 900 || sPos.X < 0)
                {
                    velocity = -5;
                    velocity2 = -2;
                }
                */
            }
            
            if (kb.IsKeyDown(Keys.Right) && !kb.IsKeyDown(Keys.Up) && !kb.IsKeyDown(Keys.Down) && nPos.X < 760)
            {
                ninja = ninjaR;
                nPos.X+= 2;
                if (sPos.X > 900 || sPos.X < 0)
                {
                    velocity = 20;
                    velocity2 = 2;
                }
            }
            if (kb.IsKeyDown(Keys.Up) && nPos.Y > 0 && !kb.IsKeyDown(Keys.Left) && !kb.IsKeyDown(Keys.Right))
            {
                ninja = ninjaR;
                nPos.Y -= vertical;
            }
            if (kb.IsKeyDown(Keys.Down) && nPos.Y < 450 && !kb.IsKeyDown(Keys.Left) && !kb.IsKeyDown(Keys.Right)) 
            {
                ninja = ninjaR;
                nPos.Y += vertical;
            }

            if (kb.IsKeyDown(Keys.Space) && sPos.X > 800)
            {
                sPos.X = nPos.X + 40;
                sPos.Y = nPos.Y + 50;
                
            }

            sPos.X += velocity;

            if (kb.IsKeyDown(Keys.Enter) && fPos.X > 800 && count > 0)
            {
                fPos.X = nPos.X + 40;
                fPos.Y = nPos.Y + 50;
                count--;
                
            }

            fPos.X += velocity2;

            mPos.X -= velocity3;

            if (sPos.Intersects(mPos)) 
            {
                _score += 100;
                mPos.X = 1100;
                mPos = new Rectangle(1100, rng.Next(0, 400), 75, 75);
                velocity3+= 1;
                //vertical++;
                sPos = new Rectangle(1100, 1100, 50, 50);
            }

            if (fPos.Intersects(mPos))
            {
                _score += 50;
                mPos.X = 1100;
                mPos = new Rectangle(1100, rng.Next(0, 400), 75, 75);
                velocity3++;
                
            }

            if (mPos.Intersects(nPos) || mPos.X < 0)
            {
                Exit();
            }


            // TODO: Add your update logic here

            base.Update(gameTime);
            oldKB = kb;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(background, bgPos, Color.White);
            _spriteBatch.Draw(ninja, nPos, Color.White);
            _spriteBatch.Draw(star, sPos, Color.White);
            _spriteBatch.Draw(fireball, fPos, Color.White);
            _spriteBatch.Draw(monster, mPos, Color.White);
            _spriteBatch.DrawString(_font, _label + _score.ToString(),
              _textPos, Color.White);



            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}