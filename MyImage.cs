using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace PSI_TD2
{
    class MyImage
    {
        private string type;
        private int taille;
        private int tailleOffset;
        private int largeur;
        private int hauteur;
        private int bitparcouleur;
        private byte[,] image;
        private byte[] source;

        public string Type
        {
            get { return type; }
        }
        public int Taille
        {
            get { return taille; }
        }
        public int TailleOffset
        {
            get { return tailleOffset; }
        }
        public int Largeur
        {
            get { return largeur; }
        }
        public int Hauteur
        {
            get { return hauteur; }
        }
        public int Bitparcouleur
        {
            get { return bitparcouleur; }
        }
        public byte[,] Image
        {
            get { return image; }
        }


        public MyImage(string myfile)
        {
            byte[] tab = File.ReadAllBytes(myfile);
            source = tab;
            byte[] tt = new byte[2];
            byte[] t = new byte[4];
            for (int i=0;i<2;i++)
            {
                tt[i] = tab[i];
            }
            type = ASCIIEncoding.ASCII.GetString(tt);

            for (int i = 2; i < 6; i++)
            {
                t[i - 2] = tab[i];
                Console.WriteLine(tab[i]);
            }
            taille = Convertir_Endian_To_Int(t);

            for (int i = 10; i < 14; i++)
            {
                t[i - 10] = tab[i];
            }
            tailleOffset = Convertir_Endian_To_Int(t);

            for (int i = 18; i < 22; i++)
            {
                t[i - 18] = tab[i];
            }
            largeur = Convertir_Endian_To_Int(t);
            for (int i = 22; i < 26; i++)
            {
                t[i - 22] = tab[i];
            }
            hauteur = Convertir_Endian_To_Int(t);

            
            for (int i = 28; i < 30; i++)
            {
                tt[i - 28] = tab[i];
                //Console.WriteLine(tab[i]);
            }
            bitparcouleur = Convertir_Endian_To_Int(tt);

            image = new byte[hauteur, largeur*3];
            int p = 0;
            for (int i=tailleOffset;i<tab.Length;i=i+(largeur*3))
            {
                int q = 0;
                for(int j=i;j<i+(largeur*3);j++)
                {
                    image[p, q] = tab[j];
                    q++;
                }
                p++;
    
            }
            

        }

        public void From_Image_To_File(string file)
        {
            byte[] retour = new byte[taille];
            byte[] t2 = new byte[2];
            byte[] t4 = new byte[4];
            byte[] img = ConvertirImageEnTByte();

            t2=ASCIIEncoding.ASCII.GetBytes(type);
            for( int i= 0;i<2;i++)
            {
                retour[i] = t2[i];
            }

            t4 = Convertir_Int_To_Endian(taille);
            for (int i = 2; i < 6; i++)
            {
                retour[i] = t4[i-2];
            }

            t4 = Convertir_Int_To_Endian(tailleOffset);
            for (int i = 10; i < 13; i++)
            {
                retour[i] = t4[i-10];
            }

            retour[14] = 40;

            t4 = Convertir_Int_To_Endian(largeur);
            for (int i = 18; i < 22; i++)
            {
                retour[i] = t4[i-18];
            }

            t4 = Convertir_Int_To_Endian(hauteur);
            for (int i = 22; i < 26; i++)
            {
                retour[i] = t4[i - 22];
            }

            retour[26] = 1;

            t2 = Convertir_Int_To_Endian(bitparcouleur);
            for(int i=28;i<30;i++)
            {
                retour[i] = t2[i - 28];
            }

            
            for(int i=54;i<retour.Length;i++)
            {
                retour[i] = img[i - 54];
            }

            File.WriteAllBytes(file, retour);
        }
        byte[] ConvertirImageEnTByte()
        {
            byte[] retour = new byte[image.Length];
            int i = 0;
            foreach(byte b in image)
            {
                retour[i] = b;
                i++;
            }
            return retour;
        }
        public int Convertir_Endian_To_Int(byte[] tab)
        {
            double n = -1;
            if(tab!=null&&tab.Length!=0)
            {
                n = 0;
                for (int i = 0; i < tab.Length; i++)
                {
                    n = n + tab[i] * Math.Pow(256d, i);
                    Console.WriteLine(n);
                }
            }
            return Convert.ToInt32(n);
            
        }
        public void InverserCouleur()
        {
            for(int i=0;i<image.GetLength(0);i++)
            {
                for(int j=0;j<image.GetLength(1);j++)
                {
                    image[i, j] = Convert.ToByte(255 - image[i, j]);
                }
            }
        }
        public byte[] Convertir_Int_To_Endian(int val)
        {

            if (val > -1)
            {
                byte[] tab = BitConverter.GetBytes(val);
                return tab;
            }
            else return null;
            
            
        }
    }
}
