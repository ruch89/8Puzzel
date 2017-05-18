using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace _8Puzzel
{
    class Box
    {
        Texture2D texture;
        Rectangle location;
        public Color tint;
        public Color fontColor;
        public bool touched;
        public int number;


        Vector2 motion;
        Vector2 position;
        //float speed = 5;


        public Rectangle Location
        {
            get { return location; }
        }

        public Box(Texture2D texture, Rectangle location, Color tint)
        {
            this.texture = texture;
            this.location = location;
            this.tint = tint;
            this.touched = false;
            this.fontColor = Color.White;
        }

        public void setFontColor(Color color)
        {
            this.fontColor = color;
        }


        public void SetInStartPosition(Rectangle brickLocation)
        {
            //motion = new Vector2(1, -1);
        }

        public void Update()
        {
            //position += motion*speed;
            location.X = (int)position.X;
            location.Y = (int)position.Y;

        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            //String text = this.number.ToString()+"\n "+this.startLadder.ToString()+"\n "+ this.startSnake.ToString();
            if (!this.touched)
            {
                String text = this.number.ToString();
                spriteBatch.Draw(texture, location, tint);
                spriteBatch.DrawString(spriteFont, text, new Vector2(location.X + 7, location.Y + 7), this.fontColor);

            }
        }
    }
}
