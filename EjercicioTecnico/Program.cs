using System;
using System.Collections.Generic;
using System.Linq;

namespace EjercicioTecnico
{
    class Program
    {
        static string[] mapa = System.IO.File.ReadAllLines(@"C:\Users\User\source\repos\EjercicioTecnico\EjercicioTecnico\Text\map.txt"); //System.IO.File.ReadAllLines(@"C:\Users\User\source\repos\EjercicioTecnico\EjercicioTecnico\Text\4x4.txt");
        public static int[,] matriz;
        static int bandera = 0;
        static int tamanio = 0;

        static List<int> listaResultante = new List<int>();
        static string listaCalculadaFinal;
        
        static int stepestFinal = 0;

        static void Main(string[] args)
        {
            crearMatriz();

            for (int i = 0; i < tamanio; i++)
            {
                for (int y = 0; y < tamanio; y++)
                {
                    recorrerMatriz(i, y, 1, matriz[i, y]);
                }
            }
            
            string final = string.Join(",", listaResultante);
            List<string> listaFinal = final.Split("-1").OrderByDescending(a => a.Length).ToList();
            List<string> pj = listaFinal[0].Split(",").Where(a => a != "").ToList();
            int tamanioMax = pj.Count;


            foreach (var item in listaFinal)
            {
                List<string> ps = item.Split(",").Where(a => a != "").ToList();
                if (ps.Count == tamanioMax)
                {
                    int numeroInicial = Int32.Parse(ps[0]);
                    int numeroFinal = Int32.Parse(ps[ps.Count - 1]);
                    int stepest = numeroInicial - numeroFinal;

                    if (stepestFinal < stepest)
                    {
                        stepestFinal = stepest;
                        listaCalculadaFinal = string.Join(",", ps);

                    }
                    //stepestFinal = stepestFinal > stepest ? stepestFinal : stepest;
                }
            }           

            Console.WriteLine("Lenght of calculated path: " + tamanioMax);
            Console.WriteLine("Drop of calculated path: "+ stepestFinal);
            Console.WriteLine("Calculated Path: [{0}]", listaCalculadaFinal);

        }


        public static void crearMatriz()
        {
            foreach (string line in mapa)
            {         
                string[] numero = line.Split(" ");
                
                if (numero.Length == 2)
                {
                    tamanio = Int32.Parse(numero[0]);
                    matriz = new int[tamanio, tamanio];
                }
                else
                {
                    for (int i = 0; i < numero.Length; i++)
                    {
                        matriz[bandera, i] = Int32.Parse(numero[i]);
                    }
                    bandera++;
                }

            }
        }


        private static void recorrerMatriz(int posX, int posY, int tamanioA, int numeroInicial)
        {
            int[] RecX = { 0, 1, 0, -1 };
            int[] RecY = { 1, 0, -1, 0 };

            for (var i = 0; i < 4; i++)
            {
                var dentroMatriz = posX + RecX[i] >= 0 && posX + RecX[i] < tamanio && posY + RecY[i] >= 0 && posY + RecY[i] < tamanio;

                if (dentroMatriz && (matriz[posX,posY] > matriz[posX + RecX[i],posY + RecY[i]]))
                {
                    listaResultante.Add(matriz[posX, posY]);
                    recorrerMatriz(posX + RecX[i], posY + RecY[i], tamanioA + 1, numeroInicial);
                }              
            }

            listaResultante.Add(matriz[posX, posY]);
            listaResultante.Add(-1);
        }

    }
}
