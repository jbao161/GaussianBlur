using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

// 2020_01_12_100611523 TODO:
// Implement faster box blur algorithm
// add a "to grayscale" button and method
// 
namespace GaussianBlur
{

    public partial class MainWindow : Form
    {

        private bool debugflag = true;
        private void debug(string message)
        {
            if (debugflag) Console.WriteLine(message);
        }

        static int guassian_kernel_size = 5;
        static double gaussian_kernel_sigma = 1.0;

        /* Section 1 : Find a control at mouse location
         * this is to find the picture being right clicked on when we get the copy paste context menu
         * code stolen from https://stackoverflow.com/a/16543294
        */
        public static Control FindControlAtPoint(Control container, Point pos)
        {
            Control child;
            foreach (Control c in container.Controls)
            {
                if (c.Visible && c.Bounds.Contains(pos))
                {
                    child = FindControlAtPoint(c, new Point(pos.X - c.Left, pos.Y - c.Top));
                    if (child == null) return c;
                    else return child;
                }
            }
            return null;
        }
        public static Control FindControlAtCursor(Form form)
        {
            Point pos = Cursor.Position;
            if (form.Bounds.Contains(pos))
                return FindControlAtPoint(form, form.PointToClient(pos));
            return null;
        } // end Section 1

        public MainWindow()
        {
            InitializeComponent();
        }
        private void MainWindow_Load(object sender, EventArgs e)
        {
            pictureBox_rightpanel.AllowDrop = true;
            pictureBox_leftpanel.AllowDrop = true;
        }
        private void pictureBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }
        /*
         * Drag and drop 
         * picture files from Windows explorer onto the panel to load the image
         */
        private void pictureBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            try
            {
                // https://stackoverflow.com/questions/6576341/open-image-from-file-then-release-lock
                using (var bmpTemp = new Bitmap(s[0]))
                {
                    ((System.Windows.Forms.PictureBox)sender).Image = new Bitmap(bmpTemp);
                }
            }
            catch (OutOfMemoryException ex)
            {
                Console.WriteLine("File is not a valid image: {0}. \n {1}", s[0], ex);
                return;
            }
            resize_imagepanels();
        }
        /* automatically resize the image
        * concept: calculate the height and width of each image when they are stitched together
        * (scale the taller image to the height of the shorter image, and keep aspect ratio)
        * now you know the proportional width the left image takes up in the total stitched width
        * multiply this proportion by the width of the viewing window and you get the width the left picture
        * should take up. Put a divider there and the two images in 'Zoom' display will be of same height
        */
        private void resize_imagepanels()
        {
            if (!(pictureBox_leftpanel.Image is null || pictureBox_rightpanel.Image is null))
            {
                if (this.splitContainer_bothimages.Orientation == Orientation.Vertical)
                {
                    int min_height = Math.Min(pictureBox_leftpanel.Image.Height, pictureBox_rightpanel.Image.Height);
                    int stitched_leftimg_width = (int)(pictureBox_leftpanel.Image.Width * min_height / (double)pictureBox_leftpanel.Image.Height);
                    int stitched_rightimg_width = (int)(pictureBox_rightpanel.Image.Width * min_height / (double)pictureBox_rightpanel.Image.Height);
                    int result_width = stitched_leftimg_width + stitched_rightimg_width;
                    this.splitContainer_bothimages.SplitterDistance = panel_bothimages.Width * stitched_leftimg_width / result_width;
                }
                if (this.splitContainer_bothimages.Orientation == Orientation.Horizontal)
                {
                    int min_width = Math.Min(pictureBox_leftpanel.Image.Width, pictureBox_rightpanel.Image.Width);
                    int stitched_leftimg_height = (int)(min_width * pictureBox_leftpanel.Image.Height / (double)pictureBox_leftpanel.Image.Width);
                    int stitched_rightimg_height = (int)(min_width * pictureBox_rightpanel.Image.Height / (double)pictureBox_rightpanel.Image.Width);
                    int result_height = stitched_leftimg_height + stitched_rightimg_height;
                    this.splitContainer_bothimages.SplitterDistance = panel_bothimages.Height * stitched_leftimg_height / result_height;
                }
            }
        }
        /*  Create a blurred image
         */
        private Bitmap blur_image()
        {
            if (pictureBox_leftpanel.Image is null)
            {
                debug("error. don't have both images loaded");
                return null;
            }
            else
            {
                int kernel_size = 5;
                double sigma = 1.0;
                Bitmap blurred_img = new Bitmap(pictureBox_leftpanel.Image);
                double[,] kernel = gaussian_kernel(kernel_size, sigma);

                return blurred_img;
            }
        }
        /*  Create a gaussian kernel
         */
        private double[,] gaussian_kernel(int size, double sigma)
        {

            if (size % 2 == 0) return null; // size must be an odd integer
            double s = 2.0 * sigma * sigma, sum = 0;
            int halfwidth = size / 2;

            // generate the kernel matrix
            double[,] kernel = new double[size, size];
            for (int x = -halfwidth; x <= halfwidth; x++)
            {
                for (int y = -halfwidth; y <= halfwidth; y++)
                {
                    kernel[x + halfwidth, y + halfwidth] = (Math.Exp(-(x * x + y * y) / s)) / (Math.PI * s);
                    sum += kernel[x + halfwidth, y + halfwidth];
                }
            }

            // normalize the kernel values
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    kernel[i, j] /= sum;
                }
            }
            // printarray(kernel);
            return kernel;
        }
        private double[,] gaussian_kernel()
        {
            return gaussian_kernel(guassian_kernel_size, gaussian_kernel_sigma);
        }
        /*  Create a boxblur kernel
        */
        private double[,] boxblur_kernel(int size)
        {

            if (size % 2 == 0) return null; // size must be an odd integer
            double sum = 0;
            int halfwidth = size / 2;

            // generate the kernel matrix
            double[,] kernel = new double[size, size];
            for (int x = -halfwidth; x <= halfwidth; x++)
            {
                for (int y = -halfwidth; y <= halfwidth; y++)
                {
                    kernel[x + halfwidth, y + halfwidth] = 1;
                    sum += kernel[x + halfwidth, y + halfwidth];
                }
            }

            // normalize the kernel values
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    kernel[i, j] /= sum;
                }
            }
            // printarray(kernel);
            return kernel;
        }
        private double[,] boxblur_kernel()
        {
            return boxblur_kernel(guassian_kernel_size);
        }
        private int[,,] ReadImg(Bitmap original)
        {
            int imgwidth = original.Width;
            int imgheight = original.Height;
            int i, j;
            int pixelColors = 3;
            int[,,] GreyImage = new int[pixelColors, imgwidth, imgheight];  //[Row,Column]

            BitmapData bitmapData1 = original.LockBits(new Rectangle(0, 0, original.Width, original.Height),
                                     ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapData1.Scan0;

                for (i = 0; i < bitmapData1.Height; i++)
                {
                    for (j = 0; j < bitmapData1.Width; j++)
                    {
                        for (int color = 0; color < pixelColors; color++)
                        {
                            GreyImage[color, j, i] = (int)(imagePointer1[color]);
                        }
                        //4 bytes per pixel
                        imagePointer1 += 4;
                    }//end for j
                     //4 bytes per pixel
                    imagePointer1 += bitmapData1.Stride - (bitmapData1.Width * 4);
                }//end for i

            }//end unsafe
            original.UnlockBits(bitmapData1);

            return GreyImage;
        }
        /* Box Blur
         * Fast approximation of a gaussian blur by a repetition of average color around each point
         */
        
        private void printarray(double[,] array)
        {
            int i, j;
           for (i = 0; i < array.GetLength(0); i++)
            {
                for (j = 0; j < array.GetLength(1); j++)
                {
                    System.Diagnostics.Debug.Print(array[i, j].ToString()+" | ");

                }   //end for j
                System.Diagnostics.Debug.WriteLine("");
            }//End for i
        }
        public Bitmap DisplayImage(int[,,] GreyImage)
        {
            int i, j;
            int W, H;
            int pixelColors = GreyImage.GetLength(0);
            W = GreyImage.GetLength(1);
            H = GreyImage.GetLength(2);
            Bitmap image = new Bitmap(W, H);
            BitmapData bitmapData1 = image.LockBits(new Rectangle(0, 0, W, H),
                                     ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            unsafe
            {

                byte* imagePointer1 = (byte*)bitmapData1.Scan0;

                for (i = 0; i < bitmapData1.Height; i++)
                {
                    for (j = 0; j < bitmapData1.Width; j++)
                    {
                        // write the logic implementation here
                        for (int color = 0; color < pixelColors; color++)
                        {
                            imagePointer1[color] = (byte)GreyImage[color, j, i];
                        }
                        imagePointer1[3] = (byte)255;
                        //4 bytes per pixel
                        imagePointer1 += 4;
                    }   //end for j
                    //4 bytes per pixel
                    imagePointer1 += (bitmapData1.Stride - (bitmapData1.Width * 4));
                }//End for i
            }//end unsafe
            image.UnlockBits(bitmapData1);
            return image;// col;
        }      // Display Grey Image
        /*  Convolute an image with a filter
        */
        private Bitmap Convolve(Bitmap original, double[,] filter)
        {
            int[,,] imgarray = ReadImg(original);
            int[,,] outarray = new int[imgarray.GetLength(0), imgarray.GetLength(1), imgarray.GetLength(2)];
            int size = filter.GetLength(0);
            int filter_radius = size / 2; //rounded down
            int pixelColors = 3;
            int i, j;
            double sum = 0;
            for (i = filter_radius; i < imgarray.GetLength(1) - filter_radius - 1; i++)
            {
                for (j = filter_radius; j < imgarray.GetLength(2) - filter_radius - 1; j++)
                {
                    // write the logic implementation here
                    for (int color = 0; color < pixelColors; color++)
                    {
                        sum = 0;
                        for (int xx = -filter_radius; xx <= filter_radius; xx++)
                        {
                            for (int yy = -filter_radius; yy <= filter_radius; yy++)
                            {
                                sum += imgarray[color, i + xx, j + yy] * filter[xx + filter_radius, yy + filter_radius];
                            }
                        }
                        outarray[color, i, j] =  (int)sum;
                    }

                }   //end for j
            }//End for i

            Bitmap output = DisplayImage(outarray);
            return output;

        }
        public static Bitmap MakeGrayscale2(Bitmap original)
        {
            unsafe
            {
                //create an empty bitmap the same size as original
                Bitmap newBitmap = new Bitmap(original.Width, original.Height);

                //lock the original bitmap in memory
                BitmapData originalData = original.LockBits(
                   new Rectangle(0, 0, original.Width, original.Height),
                   ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

                //lock the new bitmap in memory
                BitmapData newData = newBitmap.LockBits(
                   new Rectangle(0, 0, original.Width, original.Height),
                   ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

                //set the number of bytes per pixel
                int pixelSize = 3;

                for (int y = 0; y < original.Height; y++)
                {
                    //get the data from the original image
                    byte* oRow = (byte*)originalData.Scan0 + (y * originalData.Stride);

                    //get the data from the new image
                    byte* nRow = (byte*)newData.Scan0 + (y * newData.Stride);

                    for (int x = 0; x < original.Width; x++)
                    {
                        //create the grayscale version
                        byte grayScale =
                           (byte)((oRow[x * pixelSize] * .11) + //B
                           (oRow[x * pixelSize + 1] * .59) +  //G
                           (oRow[x * pixelSize + 2] * .3)); //R

                        //set the new image's pixel to the grayscale version
                        nRow[x * pixelSize] = grayScale; //B
                        nRow[x * pixelSize + 1] = grayScale; //G
                        nRow[x * pixelSize + 2] = grayScale; //R
                    }
                }

                //unlock the bitmaps
                newBitmap.UnlockBits(newData);
                original.UnlockBits(originalData);

                return newBitmap;
            }
        }
        /* Make a grayscale image
         */
        public static Bitmap MakeGrayscale3(Bitmap original)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
               new float[][]
               {
         new float[] {.3f, .3f, .3f, 0, 0},
         new float[] {.59f, .59f, .59f, 0, 0},
         new float[] {.11f, .11f, .11f, 0, 0},
         new float[] {0, 0, 0, 1, 0},
         new float[] {0, 0, 0, 0, 1}
               });

            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();
            return newBitmap;
        }
        /*  Create a new image by stitching the two panel images together
         */
        private Bitmap stitch_images()
        {
            if ((pictureBox_leftpanel.Image is null) || (pictureBox_rightpanel.Image is null))
            {
                debug("error. don't have both images loaded");
                return null;
            }
            else
            {

                if (this.splitContainer_bothimages.Orientation == Orientation.Vertical)
                {
                    // scale the taller image to the height of the shorter image, and keep aspect ratio
                    int min_height = Math.Min(pictureBox_leftpanel.Image.Height, pictureBox_rightpanel.Image.Height);
                    int stitched_leftimg_width = (int)(min_height * pictureBox_leftpanel.Image.Width / (double)pictureBox_leftpanel.Image.Height);
                    int stitched_rightimg_width = (int)(min_height * pictureBox_rightpanel.Image.Width / (double)pictureBox_rightpanel.Image.Height);
                    int result_width = stitched_leftimg_width + stitched_rightimg_width;

                    Bitmap stitched_leftimg = new Bitmap(pictureBox_leftpanel.Image,
                        stitched_leftimg_width, min_height);
                    Bitmap stitched_rightimg = new Bitmap(pictureBox_rightpanel.Image,
                        stitched_rightimg_width, min_height);

                    Bitmap stitchedimage = new Bitmap(result_width, min_height);
                    using (Graphics g = Graphics.FromImage(stitchedimage))
                    {
                        g.DrawImage(stitched_leftimg, 0, 0);
                        g.DrawImage(stitched_rightimg, stitched_leftimg.Width, 0);
                    }
                    return stitchedimage;

                }

                if (this.splitContainer_bothimages.Orientation == Orientation.Horizontal) // left image becomes the one on top
                {
                    // scale the wider image down to width of the thinner image, and keep aspect ratio
                    int min_width = Math.Min(pictureBox_leftpanel.Image.Width, pictureBox_rightpanel.Image.Width);
                    int stitched_leftimg_height = (int)(min_width * pictureBox_leftpanel.Image.Height / (double)pictureBox_leftpanel.Image.Width);
                    int stitched_rightimg_height = (int)(min_width * pictureBox_rightpanel.Image.Height / (double)pictureBox_rightpanel.Image.Width);
                    int result_height = stitched_leftimg_height + stitched_rightimg_height;

                    Bitmap stitched_leftimg = new Bitmap(pictureBox_leftpanel.Image,
                        min_width, stitched_leftimg_height);
                    Bitmap stitched_rightimg = new Bitmap(pictureBox_rightpanel.Image,
                        min_width, stitched_rightimg_height);


                    Bitmap stitchedimage = new Bitmap(min_width, result_height);
                    using (Graphics g = Graphics.FromImage(stitchedimage))
                    {
                        g.DrawImage(stitched_leftimg, 0, 0);
                        g.DrawImage(stitched_rightimg, 0, stitched_leftimg_height);
                    }
                    return stitchedimage;
                }

                return new Bitmap(pictureBox_leftpanel.Image);

            }
        }
        /*  Open the stitched image in a viewing window
         */
        private void button_preview_Click(object sender, EventArgs e)
        {
            Bitmap stitchedimage = stitch_images();
            if (!(stitchedimage is null)) try
                {
                    using (Form form = new Form())
                    {

                        form.StartPosition = FormStartPosition.CenterScreen;
                        form.ClientSize = stitchedimage.Size;
                        PictureBox pb = new PictureBox();
                        pb.Dock = DockStyle.Fill;
                        pb.Image = stitchedimage;
                        pb.SizeMode = PictureBoxSizeMode.Zoom;
                        form.Controls.Add(pb);
                        form.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("{0}", ex);
                }
        }
        /*  Save the stiched image to a new file
         *  feature 1: automatically generate a filename with a sortable and copypaste friendly timestamp
         */
        private void button_save_Click(object sender, EventArgs e)
        {
            Bitmap stitchedimage = stitch_images();
            if (!(stitchedimage is null)) try
                {
                    // Displays a SaveFileDialog so the user can save the Image   
                    saveFileDialog1.Filter = "Jpeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|Png Image|*.png";
                    saveFileDialog1.Title = "Save an Image File";
                    saveFileDialog1.FileName = DateTime.Now.ToString("yyyy_MM_dd_HHmmssfff") + " blurred";                     // feature 1: timestamp
                    saveFileDialog1.RestoreDirectory = true;
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        // Saves the Image via a FileStream created by the OpenFile method.  
                        System.IO.FileStream fs =
                           (System.IO.FileStream)saveFileDialog1.OpenFile();
                        // Saves the Image in the appropriate ImageFormat based upon the  
                        // File type selected in the dialog box.  
                        // NOTE that the FilterIndex property is one-based.
                        switch (saveFileDialog1.FilterIndex)
                        {
                            case 1:
                                stitchedimage.Save(fs,
                                   System.Drawing.Imaging.ImageFormat.Jpeg);
                                break;

                            case 2:
                                stitchedimage.Save(fs,
                                   System.Drawing.Imaging.ImageFormat.Bmp);
                                break;

                            case 3:
                                stitchedimage.Save(fs,
                                   System.Drawing.Imaging.ImageFormat.Gif);
                                break;
                            case 4:
                                stitchedimage.Save(fs,
                                   System.Drawing.Imaging.ImageFormat.Png);
                                break;
                        }

                        fs.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("{0}", ex);
                }
        }
        /*  Section 2: Button controls to clear the picture panels
         */
        private void button_releaseright_Click(object sender, EventArgs e)
        {
            pictureBox_rightpanel.Image = null;
        }

        private void button_releaseleft_Click(object sender, EventArgs e)
        {
            pictureBox_leftpanel.Image = null;
        }
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Try to cast the sender to a ToolStripItem
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    // Get the control that is displaying this context menu
                    ((PictureBox)owner.SourceControl).Image = null;
                }
            }
        }// end section 2

        /* Section 3: Context menu for copy and paste
         */
        // open a copy paste menu at right click mouse location
        private void control_MouseClick_copypastemenu(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point mPointWhenClicked = new Point(e.X, e.Y);
                contextMenu_image.Show((Control)sender, e.X, e.Y);
            }
        }
        // copy image to clipboard from panel 
        private void contextMenu_image_item_copy_Click(object sender, EventArgs e)
        {
            PictureBox thispicturebox = FindControlAtCursor(this) as PictureBox;
            if (!(thispicturebox.Image is null))
            {
                DataObject dobj = new DataObject();
                dobj.SetData(DataFormats.Bitmap, true, thispicturebox.Image);
                Clipboard.SetDataObject(dobj, true);
            }
        }
        // paste image to panel from file or clipboard 
        private void contextMenu_image_item_paste_Click(object sender, EventArgs e)
        {
            PictureBox thispicturebox = FindControlAtCursor(this) as PictureBox;
            if (Clipboard.ContainsImage())
            {
                object o = Clipboard.GetImage();
                if (o != null)
                {
                    thispicturebox.Image = (Image)o;
                }
            }
            if (Clipboard.GetDataObject().GetDataPresent("FileDrop"))
            {
                string[] s = (string[])Clipboard.GetDataObject().GetData(DataFormats.FileDrop, false);
                try
                {
                    using (var bmpTemp = new Bitmap(s[0]))
                    {
                        thispicturebox.Image = new Bitmap(bmpTemp);
                    }
                }
                catch (OutOfMemoryException ex)
                {
                    Console.WriteLine("File is not a valid image: {0}. \n {1}", s[0], ex);
                    return;
                }
            }
            resize_imagepanels();
        } // end section 3

        /*  Section 4: Keyboard arrows change image to next file in folder
        */
        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{
        //    if (keyData == (Keys.Right))
        //    {
        //        return true;
        //    }
        //    return base.ProcessCmdKey(ref msg, keyData);
        //}
        /*  Section 5: Toggle top/bottom or side/side stitching
        */
        private void button_verticalhorizontal_Click(object sender, EventArgs e)
        {
            splitContainer_bothimages.Orientation = (splitContainer_bothimages.Orientation == Orientation.Vertical ? Orientation.Horizontal : Orientation.Vertical);
            button_verticalhorizontal.Text = (splitContainer_bothimages.Orientation == Orientation.Vertical ? "Stack images vertically" : "Put images side by side");
            resize_imagepanels();
        }

        private void button_swapimages_Click(object sender, EventArgs e)
        {
            Bitmap image_tempswap = (pictureBox_leftpanel.Image == null ? null : new Bitmap(pictureBox_leftpanel.Image));
            pictureBox_leftpanel.Image = (pictureBox_rightpanel.Image == null ? null : new Bitmap(pictureBox_rightpanel.Image));
            pictureBox_rightpanel.Image = image_tempswap;
            resize_imagepanels();
        }

        private void button_blur_Click(object sender, EventArgs e)
        {
            Bitmap left_img = (pictureBox_leftpanel.Image == null ? null : new Bitmap(pictureBox_leftpanel.Image));
            double[,] gaussian = gaussian_kernel(33, 10.0);
            Bitmap gray_img = Convolve(left_img, gaussian);

            pictureBox_rightpanel.Image = gray_img;
            resize_imagepanels();
        }

        private void button_boxblur_Click(object sender, EventArgs e)
        {
            Bitmap left_img = (pictureBox_leftpanel.Image == null ? null : new Bitmap(pictureBox_leftpanel.Image));
            double[,] boxfilter = boxblur_kernel(30);
            int boxblur_iter = 1;
            GSBlur fastblurobj = new GSBlur(left_img);
            Bitmap gray_img = fastblurobj.Process(10); ;
            pictureBox_rightpanel.Image = gray_img;
            resize_imagepanels();
        }
    } // end MainWindow : Form
}


