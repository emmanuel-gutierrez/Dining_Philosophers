using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dining_Philosophers
{
    class Program
    {
        static int n=5;
        static Semaphore mutex = new Semaphore(1, 1);
        static int[] state = new int[n];
        static int THINKING = 0;
        static int HUNGRY = 1;
        static int EATING = 2;
        static Semaphore[] s = new Semaphore[n];

        static void Main(string[] args)
        {
            for (int x = 0; x < n; x++)
			{
			 s[x] = new Semaphore(1, n);
			}

            Thread p1 = new Thread(philosopher1);
            Thread p2 = new Thread(philosopher2);
            Thread p3 = new Thread(philosopher3);
            Thread p4 = new Thread(philosopher4);
            Thread p5 = new Thread(philosopher5);

            p1.Start();
            p2.Start();
            p3.Start();
            p4.Start();
            p5.Start();

        }

        static void philosopher1()
        {
            while (true)
            {
                state[0] = THINKING;
                Console.WriteLine("Philosopher #1 is thinking");
                take_forks(0);
                Thread.Sleep(250);
                put_forks(0);
            }
        }

        static void philosopher2()
        {
            while (true)
            {
                state[1] = THINKING;
                Console.WriteLine("Philosopher #2 is thinking");
                take_forks(1);
                Thread.Sleep(250);
                put_forks(1);
            }
        }

        static void philosopher3()
        {
            while (true)
            {
                state[2] = THINKING;
                Console.WriteLine("Philosopher #3 is thinking");
                take_forks(2);
                Thread.Sleep(250);
                put_forks(2);
            }
        }

        static void philosopher4()
        {
            while (true)
            {
                state[3] = THINKING;
                Console.WriteLine("Philosopher #4 is thinking");
                take_forks(3);
                Thread.Sleep(250);
                put_forks(3);
            }
        }

        static void philosopher5()
        {
            while (true)
            {
                state[4] = THINKING;
                Console.WriteLine("Philosopher #5 is thinking");
                take_forks(4);
                Thread.Sleep(250);
                put_forks(4);
            }
        }

        static void take_forks(int i)
        {
            mutex.WaitOne();
            state[i] = HUNGRY;
            Console.WriteLine("Philosopher #{0} is hungry", i);
            test(i);
            mutex.Release();
            s[i].WaitOne();
        }

        static void put_forks(int i)
        {
            mutex.WaitOne();
            state[i] = THINKING;
            Console.WriteLine("Philosopher #{0} is putting the forks back.", i);
            test(((i + n - 1) % n));
            test(((i + 1) % n));
            mutex.Release();
        }

        static void test(int i)
        {
            if (state[i] == HUNGRY && state[((i + n - 1) % n)] != EATING && state[((i + 1) % n)] != EATING)
            {
                state[i] = EATING;
                Console.WriteLine("Philosopher #{0} is eating", i);
                s[i].Release();
                Console.ReadLine();
            }
        }
    }
}
