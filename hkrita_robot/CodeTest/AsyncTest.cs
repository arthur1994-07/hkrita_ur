﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace hkrita_robot.CodeTest
{
    public class AsyncTest
    {
        /** Creating and Executing a task **/



        public static void CreateAction()
        {
            Action<object> action = ((object o) =>
            {
                Console.WriteLine("new Action created: Task = {0}, obj= {1}, Thread = {2}",
                    Task.CurrentId, o, Thread.CurrentThread.ManagedThreadId);
                Task<int> task1 = LoopingMethod(1);
                LoopingMethod();
            });
            Task newTask = new Task(action, "new task");
            newTask.Start();
            Console.WriteLine("t1 has been launched. (Main Thread = {0})",
                Thread.CurrentThread.ManagedThreadId);
            newTask.Wait();
        }


        public static void TestAction()
        {
            Action<object> action = ((o) =>
            {
                Console.WriteLine("object in action: " + o);
            });
            Task newTask = new Task(action, "test task");
            newTask.Start();
            newTask.Wait();
        }
        
        /** Task Instantiation **/
        public static void ActionMethod()
        {
            Action<object> action = (object o) =>
            {
                Console.WriteLine("Task = {0}, obj = {1}, Thread = {2}",
                    Task.CurrentId, o, Thread.CurrentThread.ManagedThreadId);
                
            };
            /** create a task but do not start it **/
            Task t1 = new Task(action, "task 1");

            //Task t2 = new Task(action, "task 2");
            Task t2 = Task.Factory.StartNew(action, "task 2");

            /**Block the main thread to demonstrate that t2 is executing **/

            t2.Wait();

            // Launch t1 
            t1.Start();
            Console.WriteLine("t1 has been launched. (Main Thread = {0})",
                Thread.CurrentThread.ManagedThreadId);
            t1.Wait();

            //t2.Start();
            //Console.WriteLine("t2 has been launched. (Main Thread = {0})",
            //    Thread.CurrentThread.ManagedThreadId);
            //t2.Wait();

            /** Construct a started task using Task.Run. **/
            String taskData = "task 3";
            Task t3 = Task.Run(() =>
            {
                Console.WriteLine("Started Task = {0}, obj = {1}, Thread = {2}",
                    Task.CurrentId, taskData, Thread.CurrentThread.ManagedThreadId);
            });
            t3.Wait();

            /** Construct an unstarted task **/
            Task task4 = new Task(action, "task4");
            /** Run it synchronously **/
            task4.RunSynchronously();
            task4.Wait();

            Task finalTask = new Task(action, "final task");
            finalTask.Start();
            Console.WriteLine("final task has been launched. (Main Thread = {0})",
              Thread.CurrentThread.ManagedThreadId);
            finalTask.Wait();

        }

        public static async void emptyMethod()
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("empty method running");
                    Task.Delay(100).Wait();
                }
            });
        }

        public static async Task<int> LoopingMethod(int index)
        {
            int count = 0;
            await Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("loop " + index);
                    count++;
                    Task.Delay(100).Wait();
                }
            });

            return count;
        }
        public static void LoopingMethod()
        {
            int count = 0;
      
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("loop");
                    count++;
                    Task.Delay(100).Wait();
                }
         
            
        }
        public static async Task<string> Method1()
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("Method 1");
                    Task.Delay(100).Wait();
                }
            });
            return "Method 1 complete!";
        }
        public static void Method2()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Method 2");
                Task.Delay(100).Wait();
            }
        }



        public static async Task<object> CallMethods()
        {

            Task<string> task = Method1();
            Method2();

            Console.WriteLine("task result: "+ task.Result);
            return task.Result;
        }
        public void TestCallback()
        {
            Action<int, int> val = (x, y) =>
            {
                Console.WriteLine(x + y);
            };
            val(10, 20);
        }

        public async Task<object> DoSomething()
        {
            Console.WriteLine(1);
            return 1;
        }
    }

}
