using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace PSI_TD2
{
    class Program
    {
        static void Main(string[] args)
        {
            MyImage test = new MyImage("Test.bmp");
            Process.Start("Test.bmp");
            Console.WriteLine(test.Taille + " " + test.TailleOffset + " " + test.Largeur + " " + test.Hauteur + " " + test.Bitparcouleur);
            //afficheMatriceByte(test.Image);
            test.InverserCouleur();
            test.From_Image_To_File("retour.bmp");
            
            Process.Start("retour.bmp");
            
             
            
            //Console.WriteLine(p);
            Console.ReadLine();
        }
        static void AfficheTabByte(byte[] t)
        {
            foreach(byte b in t)
            {
                Console.Write(b + " ");
            }
        }
        static void afficheMatriceByte(byte[,] t)
        {
            for (int i=0;i<t.GetLength(0);i++)
            {
                for(int j=0;j<t.GetLength(1);j++)
                {
                    Console.Write(t[i, j] + " ");
                    
                }
                Console.WriteLine();
            }
        }
        static int Convertir_Endian_To_Int(byte[] tab)
        {
            double n = -1;
            if (tab != null && tab.Length != 0)
            {
                n = 0;
                for (int i = 0; i < tab.Length; i++)
                {
                    n = n + tab[i] * Math.Pow(256d, i);
                    
                }
            }
            return Convert.ToInt32(n);

        }
    }
}
