using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Jogo
{
    class Button
    {
        public enum State { Disabled, Active, Hover, Pressed };

        Vector2 m_pos;
        Vector2 m_size;
        public string m_text;
        SpriteFont m_font;
        Texture2D m_texBg;
        Color m_textColor;

        public Color m_bgColor;

        Vector2 m_stringSize;

        State m_state;

        public Button(Vector2 pos, Vector2 size, string text, SpriteFont font, Texture2D texBg, Color textColor)
        {
            m_pos = pos;
            m_size = size;
            m_text = text;
            m_font = font;
            m_texBg = texBg;
            m_textColor = textColor;
            m_bgColor = new Color(0.9f, 0.9f, 0.9f, 1.0f);

            m_stringSize = m_font.MeasureString(text);

            m_state = State.Active;
        }

        public void SetActive(bool active) { m_state = (active ? State.Active : State.Disabled); }

        public bool Update(GameTime gameTime)
        {
            if (m_state == State.Disabled)
                return false;

            m_state = State.Active;

            Rectangle rect = new Rectangle((int)m_pos.X, (int)m_pos.Y, (int)m_size.X, (int)m_size.Y);

            if (rect.Contains(Mouse.GetState().Position))
            {
                if ((Mouse.GetState().LeftButton == ButtonState.Pressed) &&
                    (Game1.m_prevMouseState.LeftButton == ButtonState.Released))
                    m_state = State.Pressed;
                else
                    m_state = State.Hover;
            }

            if (m_state == State.Pressed)
                return true;

            return false;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Vector2 texSize = new Vector2(m_texBg.Width, m_texBg.Height);

            Texture2D buttonTex = m_texBg;
            Color buttonColor = m_bgColor;

            switch (m_state)
            {

                case State.Hover:
                    buttonColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    break;

                case State.Pressed:
                    buttonColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                    break;

                case State.Disabled:
                    buttonColor = new Color(0.25f, 0.25f, 0.25f, 0.25f);
                    break;
            }

            spriteBatch.Draw(
                buttonTex,
                m_pos,
                null,
                null,
                Vector2.Zero,
                0.0f,
                m_size / texSize,
                buttonColor,
                SpriteEffects.None,
                0.1f
            );

            spriteBatch.DrawString(m_font, m_text,
                m_pos + m_size * 0.5f - m_stringSize * 0.5f,
                    m_textColor, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
        }


    }
}
