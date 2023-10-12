using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tymakovlab6
{
    internal class Program
    {
        static int[,] Martix(int[,] Firstmat, int[,] Secondmat)
        {
            int[,] ResMartix = new int[2, 2];

            for (int i = 0; i < Firstmat.GetLength(0); i++)
            {
                for (int j = 0; j < Secondmat.GetLength(0); j++)
                {
                    ResMartix[i, j] = Firstmat[i, 0] * Secondmat[0, j] + Firstmat[i, 1] * Secondmat[1, j];
                }
            }

            return ResMartix;
        }

        static LinkedList<int> Martix(LinkedList<int> firstMatrixList, LinkedList<int> secondMatrixList)
        {
            LinkedList<int> Resmatrixlist = new LinkedList<int>();

            Resmatrixlist.AddLast(firstMatrixList.First.Value * secondMatrixList.First.Value + firstMatrixList.First.Next.Value * secondMatrixList.Last.Previous.Value);
            Resmatrixlist.AddLast(firstMatrixList.First.Value * secondMatrixList.First.Next.Value + firstMatrixList.First.Next.Value * secondMatrixList.Last.Value);
            Resmatrixlist.AddLast(firstMatrixList.Last.Previous.Value * secondMatrixList.First.Value + firstMatrixList.Last.Value * secondMatrixList.Last.Previous.Value);
            Resmatrixlist.AddLast(firstMatrixList.Last.Previous.Value * secondMatrixList.First.Next.Value + firstMatrixList.Last.Value * secondMatrixList.Last.Value);

            return Resmatrixlist;
        }


        static void DisplayingMatrices(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(String.Format("{0, 4}", matrix[i, j]));
                }

                Console.WriteLine();
            }
        }

        static void DisplayingMatrices(LinkedList<int> matrix)
        {
            int count = 0;

            foreach (int number in matrix)
            {
                Console.Write(String.Format("{0, 4}", number));
                count++;

                if (count % 2 == 0)
                {
                    Console.WriteLine();
                }
            }
        }

        static double[] Mounthtemp(int[,] temperature)
        {
            double[] mounthAverageTempArray = new double[temperature.GetLength(0)];

            for (int i = 0; i < temperature.GetLength(0); i++)
            {
                int tempSumm = 0;

                for (int j = 0; j < temperature.GetLength(1); j++)
                {
                    tempSumm += temperature[i, j];
                }

                mounthAverageTempArray[i] = (double)tempSumm / temperature.GetLength(1);
            }

            return mounthAverageTempArray;
        }

       
        static Dictionary<string, double> Mounthtemp(Dictionary<string, int[]> temperature)
        {
            Dictionary<string, double> mounthAverageTempDictionary = new Dictionary<string, double>();

            foreach (KeyValuePair<string, int[]> mounth in temperature)
            {
                int tempSumm = 0;

                for (int i = 0; i < mounth.Value.Length; i++)
                {
                    tempSumm += mounth.Value[i];
                }

                mounthAverageTempDictionary.Add(mounth.Key, (double)tempSumm / mounth.Value.Length);
            }

            return mounthAverageTempDictionary;
        }

        
        static void Displaymounthtemp(double[] mounthAverageTempArray)
        {
            for (int i = 0; i < mounthAverageTempArray.Length; i++)
            {
                DateTime mounth = new DateTime(1000, 1, 1).AddMonths(i);
                string mounthName = mounth.ToString("MMMM");

                Console.WriteLine($"Средняя температура в {mounthName} равна {mounthAverageTempArray[i]:F} градусам");
            }
        }

        
        static void DisplaysMounthAverageTemperature(Dictionary<string, double> mounthAverageTempDictionary)
        {
            foreach (KeyValuePair<string, double> mounth in mounthAverageTempDictionary)
            {
                Console.WriteLine($"Средняя темепература в {mounth.Key} равна {mounth.Value:F} градусам");
            }
        }

        static void Main(string[] links)
        {
            bool tasksEnd = false;
            string taskNumber;

            do
            {
                Console.WriteLine("Лаба Тумакова 6");
                Console.WriteLine("Подсказка:\n" +
                                  "Упражнение 6.2. Программа умножает две матрицы второго порядка                                 -   цифра 1\n" +
                                  "Упражнение 6.3. Программа вычисляет среднюю температуру за год                                 -   цифра 2\n" +
                                  "Домашнее задание 6.2. Программа из Упражнения 6.2, но используются коллекции LinkedList        -   цифра 3\n" +
                                  "Домашнее задание 6.3. Программа из Упражнения 6.3, но используется класс Dictionary            -   цифра 4\n" +
                                  "Закончить выполнение заданий и выйти из программы                                              -   цифра 0\n");

                Console.Write("Введите номер задания: ");
                taskNumber = Console.ReadLine();

                switch (taskNumber)
                {
                    case "1":
                        

                    case "2":
                        // Упражнение 6.2. Программа умножает две матрицы второго порядка.
                        Console.Clear();
                        Console.WriteLine("Упражнение 2");

                        Random randomNumber = new Random();
                        int[,] Firstmat = new int[2, 2];
                        int[,] Secondmat = new int[2, 2];
                        int[,] ResMartix;

                        for (int i = 0; i < Firstmat.GetLength(0); i++)
                        {
                            for (int j = 0; j < Firstmat.GetLength(1); j++)
                            {
                                Firstmat[i, j] = randomNumber.Next(20);
                                Secondmat[i, j] = randomNumber.Next(20);
                            }
                        }

                        ResMartix = Martix(Firstmat, Secondmat);

                        DisplayingMatrices(Firstmat);
                        Console.WriteLine("{0, 6}", "*");
                        DisplayingMatrices(Secondmat);
                        Console.WriteLine("{0, 6}", "=");
                        DisplayingMatrices(ResMartix);

                        Console.Write("\nЧтобы закончить выполнение упражнения, нажмите на любую кнопку ");
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case "3":
                        Console.Clear();
                        Console.WriteLine("Упражнение 3");

                        Random randomTemperature = new Random();
                        int[,] temperature = new int[12, 30];
                        double[] Mounthtemparr = new double[12];
                        double Yeartemp = 0;


                        for (int i = 0; i < temperature.GetLength(0); i++)
                        {
                            for (int j = 0; j < temperature.GetLength(1); j++)
                            {
                                temperature[i, j] = randomTemperature.Next(-35, 40);
                            }
                        }

                        Mounthtemparr = Mounthtemp(temperature);
                        Displaymounthtemp(Mounthtemparr);

                        for (int i = 0; i < Mounthtemparr.Length; i++)
                        {
                            Yeartemp += Mounthtemparr[i];
                        }

                        Yeartemp /= Mounthtemparr.Length;

                        Console.WriteLine($"Средняя температура за год равна {Yeartemp:F} градусам");
                        Console.Write("Чтобы закончить выполнение упражнения, нажмите на любую кнопку ");
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case "4":


                    case "5":
                        Console.Clear();
                        Console.WriteLine("Домашнее задание 2");

                        Random randomNum = new Random();
                        LinkedList<int> Firstmatlist = new LinkedList<int>();
                        LinkedList<int> Secondmatlist = new LinkedList<int>();
                        LinkedList<int> resultMatrixList = new LinkedList<int>();

                        for (int i = 0; i <= 3; i++)
                        {
                            Firstmatlist.AddLast(randomNum.Next(20));
                            Secondmatlist.AddLast(randomNum.Next(20));
                        }

                        resultMatrixList = Martix(Firstmatlist, Secondmatlist);

                        DisplayingMatrices(Firstmatlist);
                        Console.WriteLine("{0, 6}", "*");
                        DisplayingMatrices(Secondmatlist);
                        Console.WriteLine("{0, 6}", "=");
                        DisplayingMatrices(resultMatrixList);

                        Console.Write("\nЧтобы закончить выполнение упражнения, нажмите на любую кнопку ");
                        Console.ReadKey();
                        Console.Clear();

                        break;
                    case "6":               
                        Console.Clear();
                        Console.WriteLine("Домашнее задание 3");

                        Dictionary<string, int[]> yearTemperature = new Dictionary<string, int[]>();
                        Dictionary<string, double> mounthAverageTempList = new Dictionary<string, double>();
                        randomTemperature = new Random();
                        Yeartemp = 0;

                        for (int i = 0; i < 12; i++)
                        {
                            int[] mounthTemp = new int[30];

                            for (int j = 0; j < 30; j++)
                            {
                                mounthTemp[j] = randomTemperature.Next(-35, 40);
                            }

                            DateTime mounth = new DateTime(1000, 1, 1).AddMonths(i);
                            string mounthName = mounth.ToString("MMMM");
                            yearTemperature.Add(mounthName, mounthTemp);
                        }

                        mounthAverageTempList = Mounthtemp(yearTemperature);
                        DisplaysMounthAverageTemperature(mounthAverageTempList);

                        foreach (KeyValuePair<string, double> mounth in mounthAverageTempList)
                        {
                            Yeartemp += mounth.Value;
                        }

                        Yeartemp /= yearTemperature.Count;

                        Console.WriteLine($"Средняя температура за год равна {Yeartemp:F} градусам");
                        Console.Write("Чтобы закончить выполнение упражнения, нажмите на любую кнопку ");
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case "0":
                        Console.Clear();
                        Console.WriteLine("Завершение работы");
                        tasksEnd = true;
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Вы ввели номер задания которого нет");
                        break;
                }
            } while (!tasksEnd);
        }
    }
}
