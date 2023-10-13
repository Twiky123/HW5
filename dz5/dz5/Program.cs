using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace dz5
{
    internal class Program
    {
        struct Grandmother
        {
            public string[] illnesses;
            public string[] medicines;
            public string name;
            public int age;
            
            public void GrandMotherData(string grannyName, int grannyBirthdayYear, string[] grannyIllnesses, string[] grannyMedicines)
            {
                int todayYear = DateTime.Today.Year;

                name = grannyName;
                age = todayYear - grannyBirthdayYear;
                illnesses = grannyIllnesses;
                medicines = grannyMedicines;
            }
        }

      
        struct Hospital
        {
            public int patientsToday;
            public int patientsTotal;
            public string name;
            public string[] illnesses;
        }

        
        static bool GrandmothersToHospitals(Queue<Grandmother> grandmotherQueue, Stack<Hospital> firstHospitalsStack)
        {
            Grandmother granny = grandmotherQueue.Dequeue();
            Stack<Hospital> secondHospitalsStack = new Stack<Hospital>();
            double numberEligibleDiseases = 0;

            if (granny.illnesses.Length == 0)
            {
                for (int i = 0; i < (firstHospitalsStack.Count + secondHospitalsStack.Count); i++)
                {
                    Hospital hospital = firstHospitalsStack.Pop();

                    if (hospital.patientsToday < hospital.patientsTotal)
                    {
                        hospital.patientsToday++;
                        firstHospitalsStack.Push(hospital);

                        foreach (Hospital unsuitableHospitals in secondHospitalsStack)
                        {
                            firstHospitalsStack.Push(unsuitableHospitals);
                        }

                        return true;
                    }

                    secondHospitalsStack.Push(hospital);
                }
            }
            else
            {
                for (int i = 0; i < (firstHospitalsStack.Count + secondHospitalsStack.Count); i++)
                {
                    Hospital hospital = firstHospitalsStack.Pop();

                    foreach (string illness in granny.illnesses)
                    {
                        if (Array.IndexOf(hospital.illnesses, illness.ToLower()) != -1)
                        {
                            numberEligibleDiseases++;
                        }
                    }

                    if ((numberEligibleDiseases / granny.illnesses.Length * 100) > 50)
                    {
                        if (hospital.patientsToday < hospital.patientsTotal)
                        {
                            hospital.patientsToday++;
                            firstHospitalsStack.Push(hospital);

                            foreach (Hospital unsuitableHospitals in secondHospitalsStack)
                            {
                                firstHospitalsStack.Push(unsuitableHospitals);
                            }

                            return true;
                        }
                    }

                    secondHospitalsStack.Push(hospital);
                }
            }

            foreach (Hospital unsuitableHospitals in secondHospitalsStack)
            {
                firstHospitalsStack.Push(unsuitableHospitals);
            }
            return false;
        }

        
        static void HospitalsAndGrandmothersData(Stack<Hospital> hospitalsStack, List<Grandmother> grandmothersList)
        {
            Console.WriteLine("Список больниц");
            Console.WriteLine("{0, 15} {1, 36} {2, 30} {3, 25}\n", "Название", "Список болезней", "Пациентов сейчас", "Пациентов максимум");

            Stack<Hospital> hospitals = new Stack<Hospital>(hospitalsStack);

            for (int i = 0; i < hospitalsStack.Count; i++)
            {
                Hospital hospital = hospitals.Pop();
                string illnesses = "";

                for (int j = 0; j < hospital.illnesses.Length; j++)
                {
                    illnesses += hospital.illnesses[j] + " ";
                }

                Console.WriteLine("{0, 15} {1, 37} {2, 29} {3, 25}", hospital.name, illnesses, hospital.patientsToday, hospital.patientsTotal);
            }

            Console.WriteLine();

            Console.WriteLine("Список бабулек");
            Console.WriteLine("{0, 15} {1, 36} {2, 30} {3, 25}\n", "Имя", "Список болезней", "Список лекарств", "Возраст");

            for (int i = 0; i < grandmothersList.Count; i++)
            {
                Grandmother granny = grandmothersList[i];
                string illnesses = "";
                string medicines = "";

                for (int j = 0; j < granny.illnesses.Length; j++)
                {
                    illnesses += granny.illnesses[j] + " ";
                }

                for (int j = 0; j < granny.medicines.Length; j++)
                {
                    medicines += granny.medicines[j] + " ";
                }

                Console.WriteLine("{0, 15} {1, 37} {2, 30} {3, 24}", granny.name, illnesses, medicines, granny.age);
            }

            Console.WriteLine();
        }

       
        struct Graph
        {
            public Dictionary<char, char[]> graphVerticesEdges;
            public char currentVertex;
            public string lastVertexes;
            public string curentGraphTraversalPath;
            public List<string> graphTraversalPaths;

           
            public void GraphData(Dictionary<char, char[]> graph)
            {
                graphVerticesEdges = graph;
                currentVertex = graph.ElementAt(0).Key;
                lastVertexes = "";
                curentGraphTraversalPath = graph.ElementAt(0).Key.ToString();
                graphTraversalPaths = new List<string>();
            }

           
            public void DepthFirstGraphTraversal()
            {
                if (graphVerticesEdges[currentVertex].Length == 1)
                {
                    graphTraversalPaths.Add(curentGraphTraversalPath);
                    currentVertex = curentGraphTraversalPath[curentGraphTraversalPath.Length - 2];
                }
                else
                {
                    foreach (char nextVertix in graphVerticesEdges[currentVertex])
                    {
                        bool result = true;
                        foreach (char lastVertex in curentGraphTraversalPath)
                        {
                            if (nextVertix == lastVertex)
                            {
                                result = false;
                                break;
                            }
                        }
                        if (result)
                        {
                            curentGraphTraversalPath += nextVertix;
                            currentVertex = nextVertix;
                            DepthFirstGraphTraversal();
                        }
                    }
                }
                curentGraphTraversalPath = curentGraphTraversalPath.Remove(curentGraphTraversalPath.Length - 1);
            }
        }

        static void Main(string[] links)
        {
            bool tasksEnd = false;
            string taskNumber;

            do
            {
                Console.WriteLine("Меню задач");
                Console.WriteLine("Подсказка:\n" +
                                  "Задание №3. Программа получает бабулек и распределяет их по больницам                                   -   цифра 1\n" +
                                  "Задание №4. Программа получает граф, обходит его в глубину и выводит кратчайший путь                    -   цифра 2\n" +
                                  "Закончить выполнение заданий и выйти из программы                                                       -   цифра 0\n");

                Console.Write("Введите номер задания: ");
                taskNumber = Console.ReadLine();

                switch (taskNumber)
                {
                    case "1":
                        

                    case "2":
                        
                       
                    case "3":
                        
                        Console.Clear();
                        Console.WriteLine("Задание 3");

                        Queue<Grandmother> grandmothersQueue = new Queue<Grandmother>();
                        Stack<Hospital> hospitalStack = new Stack<Hospital>();
                        List<Grandmother> grandmothersList = new List<Grandmother>();
                        bool grandmotherHospitalTaskEnd = false;
                        bool distributionResult;

                        Hospital firstHospital = new Hospital();
                        firstHospital.name = "Северная";
                        firstHospital.illnesses = new string[] { "ангина", "простуда", "грипп", "спина" };
                        firstHospital.patientsToday = 0;
                        firstHospital.patientsTotal = 2;
                        hospitalStack.Push(firstHospital);

                        Hospital secondHospital = new Hospital();
                        secondHospital.name = "Южная";
                        secondHospital.illnesses = new string[] { "нога", "простуда", "горло", "голова" };
                        secondHospital.patientsToday = 0;
                        secondHospital.patientsTotal = 5;
                        hospitalStack.Push(secondHospital);

                        Hospital thirdHospital = new Hospital();
                        thirdHospital.name = "Центральная";
                        thirdHospital.illnesses = new string[] { "шея", "голова", "ангина", "грипп" };
                        thirdHospital.patientsToday = 0;
                        thirdHospital.patientsTotal = 3;
                        hospitalStack.Push(thirdHospital);

                        do
                        {
                            HospitalsAndGrandmothersData(hospitalStack, grandmothersList);

                            Grandmother grandmother = new Grandmother();
                            string name;
                            string[] illnesses, medicines;
                            int birthdayYear;
                            bool fillingResult;

                            Console.Write("Введите имя бабули: ");
                            name = Console.ReadLine();
                            Console.Write("Введите год рождения бабули: ");
                            fillingResult = int.TryParse(Console.ReadLine(), out birthdayYear);
                            Console.Write("Введите болезни бабули через пробел: ");
                            illnesses = Console.ReadLine().Split(new string[] { " ", ",", ", " }, StringSplitOptions.RemoveEmptyEntries);
                            Console.Write("Введите лекарства бабули через пробел: ");
                            medicines = Console.ReadLine().Split(new string[] { " ", ",", ", " }, StringSplitOptions.RemoveEmptyEntries);

                            if (fillingResult)
                            {
                                grandmother.GrandMotherData(name, birthdayYear, illnesses, medicines);
                                grandmothersQueue.Enqueue(grandmother);

                                distributionResult = GrandmothersToHospitals(grandmothersQueue, hospitalStack);

                                if (distributionResult)
                                {
                                    grandmothersList.Add(grandmother);

                                    Console.Clear();
                                    HospitalsAndGrandmothersData(hospitalStack, grandmothersList);

                                    Console.WriteLine("Бабулька попаль в больничку");
                                    Console.Write("Чтобы закончить выполнение задания, введите ЗАКОНЧИТЬ. Чтобы продолжить выполнение задания, нажмите Enter: ");

                                    if (Console.ReadLine().ToLower() == "закончить")
                                    {
                                        grandmotherHospitalTaskEnd = true;
                                    }

                                    Console.Clear();
                                }
                                else
                                {
                                    Console.WriteLine("Бабулька не попаль в больничку");
                                    Console.WriteLine("Чтобы закончить выполнение задания, введите ЗАКОНЧИТЬ. Чтобы продолжить выполнение задания, нажмите Enter: ");

                                    if (Console.ReadLine().ToLower() == "закончить")
                                    {
                                        grandmotherHospitalTaskEnd = true;
                                    }

                                    Console.Clear();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Вы ввели некорректные данные");
                                Console.Write("Чтобы продолжить нажмите на любую кнопку ");
                                Console.ReadKey();
                                Console.Clear();
                            }
                        } while (!grandmotherHospitalTaskEnd);
                        break;

                    case "4":
                        
                        Console.Clear();
                        Console.WriteLine("Задание 4");

                        Graph graph = new Graph();
                        string minGraphPath;

                        Dictionary<char, char[]> graphVerticesEdges = new Dictionary<char, char[]>()
                        {
                            {'A', new char[] { 'B', 'C' } },
                            {'B', new char[] { 'A', 'D' } },
                            {'C', new char[] { 'A', 'E', 'F' } },
                            {'D', new char[] { 'B', 'G', 'H' } },
                            {'E', new char[] { 'C', 'I', 'J' } },
                            {'F', new char[] { 'C' } },
                            {'G', new char[] { 'D' } },
                            {'H', new char[] { 'D' } },
                            {'I', new char[] { 'E' } },
                            {'J', new char[] { 'E' } },
                        };

                        graph.GraphData(graphVerticesEdges);
                        graph.DepthFirstGraphTraversal();

                        minGraphPath = graph.graphTraversalPaths[0];

                        for (int i = 0; i < graph.graphTraversalPaths.Count; i++)
                        {
                            if (minGraphPath.Length > graph.graphTraversalPaths[i].Length)
                            {
                                minGraphPath = graph.graphTraversalPaths[i];
                            }
                        }

                        Console.WriteLine($"Минимальный путь в глубину по графу - {minGraphPath}");

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
                        Console.WriteLine("Не существует такого задания");
                        break;
                }
            } while (!tasksEnd);
        }
    }
}
