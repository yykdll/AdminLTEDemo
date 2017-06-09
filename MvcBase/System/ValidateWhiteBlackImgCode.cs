using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


namespace System
{
    public static class ValidateWhiteBlackImgCode
    {
        private static Random random_0;
        private static char[] char_0;

        static ValidateWhiteBlackImgCode()
        {
            LicenseManager.Validate(typeof(ValidateWhiteBlackImgCode));
            ValidateWhiteBlackImgCode.random_0 = new Random();
            ValidateWhiteBlackImgCode.char_0 = new char[29]
      {
        'A',
        'B',
        'C',
        'D',
        'E',
        'F',
        'G',
        'H',
        'J',
        'K',
        'M',
        'N',
        'P',
        'Q',
        'R',
        'S',
        'T',
        'U',
        'V',
        'W',
        'X',
        'Y',
        '3',
        '4',
        '5',
        '6',
        '7',
        '8',
        '9'
      };
        }

        public static string RandemCode()
        {
            return ValidateWhiteBlackImgCode.RandemCode(4);
        }

        public static string RandemCode(int length)
        {
            string empty = string.Empty;
            for (int index = 0; index < length; ++index)
                empty += (Char)(object)ValidateWhiteBlackImgCode.char_0[ValidateWhiteBlackImgCode.random_0.Next(0, ValidateWhiteBlackImgCode.char_0.Length)];
            return empty;
        }

        public static byte[] Img(string code, int width, int height)
        {
            byte[] numArray = (byte[])null;
            using (Bitmap bitmap = new Bitmap(width, height))
            {
                using (Graphics graphics_0 = Graphics.FromImage((Image)bitmap))
                {
                    graphics_0.Clear(Color.White);
                    bool bool_0 = ValidateWhiteBlackImgCode.random_0.Next(0, 10) % 2 == 0;
                    Image image_1 = ValidateWhiteBlackImgCode.smethod_0(code, width, height, bool_0);
                    Image image_0 = ValidateWhiteBlackImgCode.smethod_0(code, width, height, !bool_0);
                    switch (ValidateWhiteBlackImgCode.random_0.Next(0, 2))
                    {
                        case 0:
                            graphics_0.smethod_1(width, height, image_0, image_1);
                            break;
                        case 1:
                            graphics_0.akCnwcnidw(width, height, image_0, image_1);
                            break;
                    }
                    graphics_0.Flush();
                }
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    bitmap.Save((Stream)memoryStream, ImageFormat.Png);
                    numArray = memoryStream.ToArray();
                    memoryStream.Close();
                }
            }
            return numArray;
        }

        private static Image smethod_0(string string_0, int int_0, int int_1, bool bool_0)
        {
            Bitmap bitmap = new Bitmap(int_0, int_1);
            using (Graphics graphics = Graphics.FromImage((Image)bitmap))
            {
                graphics.Clear(bool_0 ? Color.Black : Color.White);
                float emSize = 1f / 1000f;
                SizeF sizeF = SizeF.Empty;
                RectangleF layoutRectangle = RectangleF.Empty;
                Font font1 = new Font("微软雅黑", emSize, FontStyle.Bold);
                Font font2;
                for (; sizeF.IsEmpty || (double)sizeF.Width < (double)int_0; sizeF = graphics.MeasureString(string_0, font2))
                {
                    emSize += 1f / 1000f;
                    font2 = new Font("微软雅黑", emSize, FontStyle.Bold);
                }
                Font font3 = new Font("微软雅黑", emSize - 1f / 1000f, FontStyle.Bold);
                sizeF = graphics.MeasureString(string_0, font3);
                layoutRectangle = new RectangleF((float)(((double)int_0 - (double)sizeF.Width) / 2.0), (float)(2.0 + ((double)int_1 - (double)sizeF.Height) / 2.0), sizeF.Width, sizeF.Height);
                graphics.DrawString(string_0, font3, bool_0 ? Brushes.White : Brushes.Black, layoutRectangle);
                graphics.Flush();
            }
            return (Image)bitmap;
        }

        private static void smethod_1(this Graphics graphics_0, int int_0, int int_1, Image image_0, Image image_1)
        {
            int num1 = 5;
            int num2 = 3;
            SizeF sizeF = new SizeF((float)int_0 / 4f, (float)int_1 / 2f);
            PointF pointF = new PointF((float)(ValidateWhiteBlackImgCode.random_0.Next(0, (int)sizeF.Width) * -1), (float)(ValidateWhiteBlackImgCode.random_0.Next(0, (int)sizeF.Height) * -1));
            List<RectangleF> rectangleFList = new List<RectangleF>();
            for (int index1 = 0; index1 < num2; ++index1)
            {
                for (int index2 = 0; index2 < num1; ++index2)
                    rectangleFList.Add(new RectangleF(pointF.X + (float)index2 * sizeF.Width, pointF.Y + sizeF.Height * (float)index1, sizeF.Width, sizeF.Height));
            }
            for (int index = 0; index < rectangleFList.Count; ++index)
            {
                using (TextureBrush textureBrush = new TextureBrush(index % 2 == 0 ? image_0 : image_1))
                {
                    graphics_0.FillRectangle((Brush)textureBrush, rectangleFList[index]);
                    graphics_0.Flush();
                }
            }
        }

        private static void akCnwcnidw(this Graphics graphics_0, int int_0, int int_1, Image image_0, Image image_1)
        {
            graphics_0.DrawImage(image_0, 0, 0);
            PointF pointF = new PointF((float)ValidateWhiteBlackImgCode.random_0.Next(0, int_0), (float)ValidateWhiteBlackImgCode.random_0.Next(0, int_1));
            float num1 = (float)(int_0 / 7);
            float num2 = (double)int_0 / 2.0 <= (double)pointF.X ? pointF.X : (float)int_0 - pointF.X;
            List<KeyValuePair<PointF, float>> keyValuePairList = new List<KeyValuePair<PointF, float>>();
            for (int index = 0; (double)num2 - (double)index * (double)num1 > 0.0; ++index)
            {
                float num3 = num2 - (float)index * num1;
                keyValuePairList.Add(new KeyValuePair<PointF, float>(new PointF(pointF.X - num3, pointF.Y - num3), num3 * 2f));
            }
            for (int index = 0; index < keyValuePairList.Count; ++index)
            {
                if ((double)keyValuePairList[index].Value > (double)num1)
                {
                    using (Brush brush = (Brush)new TextureBrush(index % 2 == 0 ? image_1 : image_0))
                        graphics_0.FillEllipse(brush, keyValuePairList[index].Key.X, keyValuePairList[index].Key.Y, keyValuePairList[index].Value, keyValuePairList[index].Value);
                }
            }
        }
    }
}
