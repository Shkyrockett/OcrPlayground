// <copyright file="ViewerForm.cs" company="Shkyrockett" >
//     Copyright © 2022 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks>
// </remarks>

using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using Windows.Globalization;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;

namespace OCRPlayground
{
    /// <summary>
    /// The ViewerForm class.
    /// </summary>
    public partial class ViewerForm
        : Form
    {
        private readonly Language language = new("en");

        /// <summary>
        /// The metadata
        /// </summary>
        private Metadata metadata = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewerForm"/> class.
        /// </summary>
        public ViewerForm() => InitializeComponent();

        /// <summary>
        /// Pictures the box1_ paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (metadata is Metadata data)
            {
                var graphics = e.Graphics;
                Pen boxPen = Pens.Lime;
                Brush textBrush = Brushes.Black;
                using var glowPen = new Pen(Color.White, 3);
                var size = pictureBox1?.Image?.Size ?? default;

                foreach (var entry in data.TextEnteries)
                {
                    if (entry.Rectangle is RectangleF frame)
                    {
                        var bounds = new RectangleF(frame.X * size.Width, frame.Y * size.Height, frame.Width * size.Width, frame.Height * size.Height);
                        graphics.DrawRectangle(boxPen, Rectangle.Round(bounds));
                        DrawHaloText(e, entry.TextEntry, base.Font, textBrush, glowPen, bounds.TopLeft());
                    }
                }
            }
        }

        /// <summary>
        /// Browses the button_ click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void BrowseButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "All Images|*.bmp;*.dib;*.rle;*.jpg;*.jpeg;*.jpe;*.jfif;*.gif;*.tif;*.tiff;*.png|bmp files: (*.bmp; *.dib; *.rle)|*.bmp;*.dib;*.rle|jpeg files: (*.jpg; *.jpeg; *.jpe; *.jfif)|*.jpg;*.jpeg;*.jpe;*.jfif|gif files: (*.gif)|*.gif|tiff files: (*.tif; *.tiff)|*.tif;*.tiff|png files: (*.png)|*.png|all files|*.*";
            openFileDialog1.FileName = filePathTextBox.Text;
            switch (openFileDialog1.ShowDialog())
            {
                case DialogResult.Yes:
                case DialogResult.OK:
                    filePathTextBox.Text = openFileDialog1.FileName;
                    break;
                case DialogResult.None:
                case DialogResult.Cancel:
                case DialogResult.Abort:
                case DialogResult.Retry:
                case DialogResult.Ignore:
                case DialogResult.No:
                case DialogResult.TryAgain:
                case DialogResult.Continue:
                default:
                    break;
            }
        }

        /// <summary>
        /// files the path text box_ text changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private async void FilePathTextBox_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(filePathTextBox.Text))
            {
                await OpenImage(filePathTextBox.Text);
            }
        }

        /// <summary>
        /// Opens the image.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>A Task.</returns>
        private async Task OpenImage(string filename)
        {
            metadata = new Metadata();
            FileStream stream = File.OpenRead(filename);
            var pos = stream.Position;
            pictureBox1.Image = new Bitmap(stream);
            var size = pictureBox1?.Image?.Size ?? default;
            stream.Position = pos;
            OcrResult ocrResult = await OcrDocument(stream, language);
            var builder = new StringBuilder();
            foreach (var line in ocrResult.Lines)
            {
                RectangleF? rect = null;
                foreach (var word in line.Words)
                {
                    rect = rect is null ? word.BoundingRect.ToRectangleF() : RectangleF.Union(rect.Value, word.BoundingRect.ToRectangleF());
                }

                // Scale down to fraction of image to match what is done for People tags.
                rect = rect is null ? rect : new RectangleF(rect.Value.X / size.Width, rect.Value.Y / size.Height, rect.Value.Width / size.Width, rect.Value.Height / size.Height);
                metadata.Add(new Text() { TextEntry = line.Text, Rectangle = rect });
                builder.AppendLine(line.Text);
            }

            textBox2.Text = builder.ToString();
            pictureBox1?.Invalidate();
        }

        /// <summary>
        /// OCRs the document.
        /// </summary>
        /// <param name="stream">The file stream.</param>
        /// <returns>A Task.</returns>
        private static async Task<OcrResult> OcrDocument(FileStream stream, Language language)
        {
            var decoder = await BitmapDecoder.CreateAsync(stream.AsRandomAccessStream());
            var bitmap = await decoder.GetSoftwareBitmapAsync();
            var engine = OcrEngine.TryCreateFromLanguage(language);
            var ocrResult = await engine.RecognizeAsync(bitmap).AsTask();
            return ocrResult;
        }

        /// <summary>
        /// Draws the halo text. https://web.archive.org/web/20140811104050/http://bobpowell.net/halo.aspx
        /// </summary>
        /// <param name="e">The <see cref="PaintEventArgs" /> instance containing the event data.</param>
        /// <param name="text">The text.</param>
        /// <param name="font">The font.</param>
        /// <param name="brush">The brush.</param>
        /// <param name="pen">The pen.</param>
        /// <param name="origin">The location.</param>
        private static void DrawHaloText(PaintEventArgs e, string text, Font font, Brush brush, Pen pen, PointF origin)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var path = new GraphicsPath();
            path.AddString(text, font.FontFamily, (int)font.Style, g.ConvertGraphicsUnits(font.Size, font.Unit, GraphicsUnit.Pixel), origin, StringFormat.GenericDefault);
            var matrix = new Matrix();
            matrix.Translate(-1, -1);
            g.Transform = matrix;
            g.DrawPath(pen, path);
            g.FillPath(brush, path);
            g.ResetTransform();
            //g.DrawString(text, Font, brush, origin);
            path.Dispose();
        }
    }
}