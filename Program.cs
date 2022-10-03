using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Globalization;
using System.Text;
using System.IO;

namespace ImageBrailleArtGen
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if(args.Length != 5)
            {
                System.Console.WriteLine("Usage: image-braille-art-gen <width> <height> <color threshold> <input file> <output file>");
                return;
            }

            int w = int.Parse(args[0], CultureInfo.InvariantCulture);
            int h = int.Parse(args[1], CultureInfo.InvariantCulture);
            int th =  int.Parse(args[2], CultureInfo.InvariantCulture);

            using(Image<Rgba32> img = Image.Load<Rgba32>(args[3]))
            {
                // Resize and Dither the input image
                // Dithering greatly increases the final "readability" of the image
                img.Mutate(x => x.Resize(w, h).BinaryDither(KnownDitherings.FloydSteinberg));
                
                // Save the generated output text
                File.WriteAllText(args[4], GetBraille(img, th));
            }
        }

        /// <summary>
        /// Converts a 6 bit value to the corresponding braille character
        /// In the unicode standard the braille characters are neatly organized
        /// </summary>
        private static char Bin2Braille(int x)
        {
            const int offset = 10240;
            return (char)(offset + x);
        }

        /// <summary>
        /// A simple thresholding function
        /// True: Pixel is White
        /// False: Pixel is Black
        /// </summary>
        private static bool Threshold(Rgba32 value, int th)
        {
            return ((int)value.R + value.G + value.B) / 3 >= th;
        }

        public static string GetBraille(Image<Rgba32> i, int threshold)
        {
            StringBuilder sb = new StringBuilder();
            const int bW = 2, bH = 3; // Each braille character can display a 2x3 grid of black and white pixels
            
            // calculate how many 2x3 blocks are there in their respective image-axis
            int nX = (int)MathF.Ceiling(i.Width / (float)bW),
                nY = (int)MathF.Ceiling(i.Height / (float)bH);
            
            // run through the entire image
            for(int y = 0; y < nY; y++)
            {
                for(int x = 0; x < nX; x++)
                {
                    /*
                     * Since the Braille characters are neatly organized in their code point values,
                     * we can take advantage of that and greatly shorten the code.
                     * A Dictionary lookup is not required.
                     */
                    int bitfield = 0;
                     // Scan the image in 2x3 blocks
                    for(int jx = 0; jx < bW; jx++)
                    {
                        for(int jy = 0; jy < bH; jy++)
                        {
                            int xidx = x * bW + jx, yidx = y * bH + jy;
                            if(xidx > 0 && xidx < i.Width && yidx > 0 && yidx < i.Height)
                            {
                                Rgba32 pixel = i[xidx, yidx];
                                if(!Threshold(pixel, threshold))
                                    // Set the bit if the pixel is dark
                                    // jx * bH + jy converts the coordinate inside the 2x3 grid into a linear one
                                    bitfield |= (1 << (jx * bH + jy));
                            }
                        }
                    }
                    // After having traversed the block, we can finally add the corresponding braille character
                    sb.Append(Bin2Braille(bitfield));
                }
                sb.AppendLine(); // After each horizontal image pixel line, we also need a newline in the final text output
            }

            return sb.ToString();
        }
    }
}