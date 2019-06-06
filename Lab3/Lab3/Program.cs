using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Lab_3
{
    class Program
    {
        static double K1 = 0.0005, K2 = 0.002;
        static int n;
        static int[] b1, c1;
        static int[,] A, A1, A2, B2;
        static double[] b, y1, y2;
        static double[,] C2;

        static double[,] Y3;

        static double[,] Y3Double, Y3Triple;
        static double[] first, second, x;

        static void Main(string[] args)
        {
            Console.WriteLine("Введiть n: ");
            n = Convert.ToInt32(Console.ReadLine());

            b1 = new int[n];
            c1 = new int[n];

            A = new int[n, n];
            A1 = new int[n, n];
            A2 = new int[n, n];
            B2 = new int[n, n];
            C2 = new double[n, n];
            Y3Double = new double[n, n];
            Y3Triple = new double[n, n];
            Y3 = new double[n, n];

            b = new double[n];
            y1 = new double[n];
            y2 = new double[n];
            first = new double[n];
            second = new double[n];
            x = new double[n];

            Thread t_set_b1_c1 = new Thread(set_b1_c1);
            Thread t_setA = new Thread(set_A);
            Thread t_setA1_A2_B2 = new Thread(set_A1_A2_B2);
            Thread t_set_C2 = new Thread(set_C2);
            Thread t_set_b = new Thread(set_b);

            Thread t_get_y1 = new Thread(get_y1);
            Thread t_get_y2 = new Thread(get_y2);
            Thread t_get_Y3 = new Thread(get_Y3);

            Thread t_get_Y3D = new Thread(get_Y3Double);
            Thread t_get_Y3T = new Thread(get_Y3Triple);
            Thread t_first = new Thread(get_First);
            Thread t_second = new Thread(get_Second);
            Thread t_result = new Thread(get_Result);

            t_set_b1_c1.Start();
            t_setA.Start();
            t_setA1_A2_B2.Start();
            t_set_C2.Start();
            t_set_b.Start();
            t_get_y1.Start();
            t_get_y2.Start();

            t_get_Y3.Start();
            t_get_Y3D.Start();
            t_get_Y3T.Start();
            t_get_Y3.Join();
            t_get_Y3D.Join();
            t_get_Y3T.Join();

            t_first.Start();
            t_first.Join();
            t_second.Start();
            t_second.Join();
            t_result.Start();

            showArray(b1, "b1");
            showArray(c1, "c1");
            showArray(A, "A");
            showArray(A1, "A1");
            showArray(A2, "A2");
            showArray(B2, "B2");
            showArray(C2, "C2");
            showArray(b, "b");
            Console.WriteLine("Results");
            showArray(y1, "y1");
            showArray(y2, "y2");
            showArray(Y3, "Y3");
            showArray(Y3Double, "Y3Double");
            showArray(Y3Triple, "Y3Triple");

            showArray(first, "first");
            showArray(second, "second");

            Console.WriteLine("x");
            for (int i = 0; i < n; ++i)
            {
                int tr = i + 1;
                Console.WriteLine("Обчислено в потоцi - " + tr + ": " + Math.Round(x[i], 2));
            }
            Console.ReadKey();
        }

        static void set_b()
        {
            for (int i = 0; i < n; ++i)
            {
                if (i % 2 == 0)
                    b[i] = 3/(Math.Pow(i,2)+3);
                else
                    b[i] = 3/i;
                Thread.Sleep(0);

            }
        }

        static void set_C2()
        {
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    C2[i, j] = i+j;
                    Thread.Sleep(0);
                }
                Thread.Sleep(0);
            }
        }

        static void set_A()
        {
            Random r = new Random();
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    A[i, j] = r.Next(1, n + 1);
                    Thread.Sleep(0);
                }
                Thread.Sleep(0);
            }
        }

        static void set_A1_A2_B2()
        {
            Random r = new Random();
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    A1[i, j] = r.Next(1, n + 1);
                    A2[i, j] = r.Next(1, n + 1);
                    B2[i, j] = r.Next(1, n + 1);
                    Thread.Sleep(0);
                }
                Thread.Sleep(0);
            }
        }

        static void set_b1_c1()
        {
            Random r = new Random();
            for (int i = 0; i < n; ++i)
            {
                b1[i] = r.Next(1, n + 1);
                c1[i] = r.Next(1, n + 1);
                Thread.Sleep(0);
            }
        }

        static void get_Y3()
        {
            double[,] R = new double[n, n];
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    R[i, j] = (B2[i, j] - C2[i, j]);
                    Thread.Sleep(0);
                }
                Thread.Sleep(0);
            }
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; j++)
                {
                    Y3[i, j] = 0;
                    for (int k = 0; k < n; ++k)
                    {
                        Y3[i, j] += A2[i, k] * R[k, j];
                        Thread.Sleep(0);
                    }
                    Thread.Sleep(0);
                }
                Thread.Sleep(0);
            }
        }

        static void get_y1()
        {
            double s = 0.0;
            for (int i = 0; i < n; ++i)
            {
                s = 0.0;
                for (int j = 0; j < n; ++j)
                {
                    s += A[i, j] * b[j];
                    Thread.Sleep(0);
                }
                y1[i] = s;
                Thread.Sleep(0);
            }
        }

        static void get_y2()
        {
            double[] d = new double[n];
            for (int i = 0; i < n; ++i)
            {
                d[i] = 3 * b1[i] + c1[i];
                Thread.Sleep(0);
            }

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    y2[i] += (double)A1[i, j] * d[j];
                    Thread.Sleep(0);
                }
                Thread.Sleep(0);
            }
        }

        static void get_Y3Double()
        {
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    Y3Double[i, j] = 0;
                    for (int k = 0; k < n; ++k)
                    {
                        Y3Double[i, j] += Y3[i, k] * Y3[k, j];
                    }
                    Thread.Sleep(0);
                }
                Thread.Sleep(0);
            }
            Thread.Sleep(0);
        }

        static void get_Y3Triple()
        {
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    Y3Double[i, j] = 0;
                    for (int k = 0; k < n; ++k)
                    {
                        Y3Triple[i, j] += Y3[i, k] * Y3[k, j]*Y3[i,j];
                    }
                    Thread.Sleep(0);
                }
                Thread.Sleep(0);
            }
            Thread.Sleep(0);
        }


        static void get_Second()
        {
            double[] y3 = new double[n];
            for (int i = 0; i < n; ++i)
            {
                y3[i] = y1[i] + y2[i];
                Thread.Sleep(0);
            }
            double s = 0.0;

            for (int i = 0; i < n; ++i)
            {
                s = 0.0;
                for (int j = 0; j < n; ++j)
                {
                    s += y2[j] * y1[j] + K2 * Y3Double[i, j] * y1[j] * y1[j];
                    Thread.Sleep(0);
                }
                second[i] = s;
                Thread.Sleep(0);
            }
        }

        static void get_First()
        {

            double s = 0.0;

            for (int i = 0; i < n; ++i)
            {
                s = 0.0;
                for (int j = 0; j < n; ++j)
                {
                    s += K1 * Y3[i, j] * y2[j] * y2[j] + Y3Triple[i, j] - Y3[i, j];
                    Thread.Sleep(0);
                }
                first[i] = s;
                Thread.Sleep(0);
            }
        }

        static void get_Result()
        {
            for (int i = 0; i < n; ++i)
            {
                x[i] = first[i] * second[i];
                Thread.Sleep(0);
            }
        }

        static void showArray(double[,] Input, string s)
        {
            Console.WriteLine(s);
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    Console.Write(Math.Round(Input[i, j], 2) + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void showArray(int[,] Input, string s)
        {
            Console.WriteLine(s);
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    Console.Write(Input[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void showArray(double[] Input, string s)
        {
            Console.WriteLine(s);
            for (int i = 0; i < n; ++i)
                Console.Write(Math.Round(Input[i], 2) + " ");
            Console.WriteLine();
        }

        static void showArray(int[] Input, string s)
        {
            Console.WriteLine(s);
            for (int i = 0; i < n; ++i)
                Console.Write(Input[i] + " ");
            Console.WriteLine();
        }

    }
}
