﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cb0t
{
    class UserListBoxItem
    {
        public User Owner { get; private set; }

        public UserListBoxItem(User owner)
        {
            this.Owner = owner;
        }

        public void Draw(DrawItemEventArgs e, bool selected, bool tracked, bool is_black)
        {
            Color item_bg = is_black ? Color.Black : Color.White;

            if (tracked)
                item_bg = is_black ? Color.DimGray : Color.Gainsboro;
            if (selected)
                item_bg = is_black ? Color.DarkBlue : Color.LightSteelBlue;

            using (SolidBrush brush = new SolidBrush(item_bg))
                e.Graphics.FillRectangle(brush, e.Bounds);

            if (this.Owner.AvatarBytes.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream(this.Owner.AvatarBytes))
                using (Bitmap bmp = new Bitmap(ms))
                    e.Graphics.DrawImage(bmp, new Point(e.Bounds.X + 1, e.Bounds.Y + 2));
            }
            else
            {
                using (MemoryStream ms = new MemoryStream(Avatar.DefaultAvatar))
                using (Bitmap bmp = new Bitmap(ms))
                    e.Graphics.DrawImage(bmp, new Point(e.Bounds.X + 1, e.Bounds.Y + 2));
            }

            int img_x = 58;

            Color name_color = is_black ? Color.White : Color.Black;

            if (this.Owner.Level == 3)
                name_color = Color.Red;
            else if (this.Owner.Level == 2)
                name_color = is_black ? Color.Lime : Color.Green;
            else if (this.Owner.Level == 1)
                name_color = is_black ? Color.Aqua : Color.Blue;

            if (this.Owner.HasFiles)
            {
                using (MemoryStream ms = new MemoryStream(Avatar.BrowseIcon))
                using (Bitmap bmp = new Bitmap(ms))
                    e.Graphics.DrawImage(bmp, new Rectangle(e.Bounds.X + img_x, e.Bounds.Y + 7, 14, 14));

                img_x += 15;
            }

            if (this.Owner.IsAway)
            {
                using (MemoryStream ms = new MemoryStream(Avatar.AwayIcon))
                using (Bitmap bmp = new Bitmap(ms))
                    e.Graphics.DrawImage(bmp, new Rectangle(e.Bounds.X + img_x, e.Bounds.Y + 7, 14, 14));

                img_x += 15;
            }

            if (this.Owner.SupportsVC)
            {
                using (MemoryStream ms = new MemoryStream(is_black ? Avatar.VoiceIconBlack : Avatar.VoiceIcon))
                using (Bitmap bmp = new Bitmap(ms))
                    e.Graphics.DrawImage(bmp, new Rectangle(e.Bounds.X + img_x, e.Bounds.Y + 7, 14, 14));

                img_x += 15;
            }

            using (Font name_font = new Font(e.Font, FontStyle.Bold))
            using (SolidBrush brush = new SolidBrush(name_color))
            {
                char[] n_letters = this.Owner.Name.ToCharArray();
                int n_x_pos = e.Bounds.X + img_x;
                int n_y_pos = e.Bounds.Y + 7;
                int n_max_width = (n_x_pos + e.Bounds.Width);

                for (int i = 0; i < n_letters.Length; i++)
                {
                    switch (n_letters[i])
                    {
                        case ' ':
                            n_x_pos += 4;
                            break;

                        default:
                            try
                            {
                                int w = (int)Math.Round((double)e.Graphics.MeasureString(n_letters[i].ToString(), name_font, 100, StringFormat.GenericTypographic).Width);

                                if ((n_x_pos + w) < n_max_width)
                                    e.Graphics.DrawString(n_letters[i].ToString(), name_font, brush, new PointF(n_x_pos, n_y_pos));

                                n_x_pos += w;
                            }
                            catch { break; }
                            break;
                    }

                    if (n_x_pos >= n_max_width)
                        break;
                }
            }

            try
            {
                using (SolidBrush brush = new SolidBrush(is_black ? Color.WhiteSmoke : Color.Gray))
                    e.Graphics.DrawString(this.Owner.ToASLString(), e.Font, brush, new PointF(e.Bounds.X + 58, e.Bounds.Y + 24));
            }
            catch { }

            String text = this.Owner.PersonalMessage;
            bool is_song = false;

            if (text.StartsWith("\x0007"))
            {
                text = Helpers.StripColors(text);

                if (is_black)
                    text = "\x0007\x000311" + text;
                else
                    text = "\x0007\x000312" + text;

                is_song = true;
            }

            if (!String.IsNullOrEmpty(text))
            {
                char[] letters = text.ToCharArray();
                bool bold = false, italic = false, underline = false;
                int x = e.Bounds.X + 58, y = e.Bounds.Y + 38;
                int max_width = (e.Bounds.X + e.Bounds.Width);
                Color fore_color = is_black ? Color.WhiteSmoke : Color.Gray;
                int color_finder;

                if (is_song)
                {
                    using (MemoryStream ms = new MemoryStream(is_black ? Avatar.MusicIconBlack : Avatar.MusicIcon))
                    using (Bitmap bmp = new Bitmap(ms))
                        e.Graphics.DrawImage(bmp, new RectangleF(x, y, 14, 14));

                    x += 15;
                }

                for (int i = 0; i < letters.Length; i++)
                {
                    switch (letters[i])
                    {
                        case '\x0006':
                            bold = !bold;
                            break;

                        case '\x0007':
                            underline = !underline;
                            break;

                        case '\x0009':
                            italic = !italic;
                            break;

                        case '\x0003':
                            if (letters.Length >= (i + 3))
                                if (int.TryParse(text.Substring(i + 1, 2), out color_finder))
                                {
                                    fore_color = this.GetColorFromCode(color_finder);
                                    i += 2;
                                }
                                else goto default;
                            else goto default;
                            break;

                        case '\x0005':
                            if (letters.Length >= (i + 3))
                                if (int.TryParse(text.Substring(i + 1, 2), out color_finder))
                                    i += 2;
                                else goto default;
                            else goto default;
                            break;

                        case ' ':
                            x += underline ? 2 : (bold ? 4 : 3);
                            break;

                        case '(':
                        case ':':
                        case ';':
                            Emotic em = Emoticons.FindEmoticon(text.Substring(i).ToUpper());

                            if (em != null && !is_song)
                            {
                                if ((x + 15) < max_width)
                                    e.Graphics.DrawImage(Emoticons.emotic[em.Index], new RectangleF(x + 3, y, 14, 14));

                                x += 15;
                                i += (em.Shortcut.Length - 1);
                                break;
                            }
                            else goto default;

                        default:
                            using (Font f = new Font(e.Font, this.CreateFont(bold, italic, underline)))
                            using (SolidBrush brush = new SolidBrush(fore_color))
                            {
                                try
                                {
                                    int w = (int)Math.Round((double)e.Graphics.MeasureString(letters[i].ToString(), f, 100, StringFormat.GenericTypographic).Width);

                                    if ((x + w) < max_width)
                                        e.Graphics.DrawString(letters[i].ToString(), f, brush, new PointF(x, y));

                                    x += w;
                                }
                                catch { break; }
                            }
                            break;
                    }

                    if (x >= max_width)
                        break;
                }
            }
        }

        private FontStyle CreateFont(bool bold, bool italic, bool underline)
        {
            FontStyle f = bold ? FontStyle.Bold : FontStyle.Regular;

            if (italic)
                f |= FontStyle.Italic;

            if (underline)
                f |= FontStyle.Underline;

            return f;
        }

        private Color GetColorFromCode(int code)
        {
            switch (code)
            {
                case 1: return Color.Black;
                case 2: return Color.Navy;
                case 3: return Color.Green;
                case 4: return Color.Red;
                case 5: return Color.Maroon;
                case 6: return Color.Purple;
                case 7: return Color.Orange;
                case 8: return Color.Yellow;
                case 9: return Color.Lime;
                case 10: return Color.Teal;
                case 11: return Color.Aqua;
                case 12: return Color.Blue;
                case 13: return Color.Fuchsia;
                case 14: return Color.Gray;
                case 15: return Color.Silver;
            }

            return Color.White;
        }
    }
}
