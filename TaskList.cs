using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading;

namespace iAppAndroid
{
    internal class TaskList
    {
        public static List<TaskItem> NewTasksList = new List<TaskItem>();
        public static List<TaskItem> DoneTasksList = new List<TaskItem>();

        public static readonly IEnumerable<int> MyTables = new List<int>() { 0, 1, 5, 8 };

        private static readonly object ThreadLock = new object();

        static TaskList()
        {

        }

        public static int CountNewTasks()
        {
            int counter;
            var myNewTasks = GetAllMyNewTasks(TaskList.MyTables);
            lock (ThreadLock)
                counter = myNewTasks.Count;
            return counter;
        }

        public static void AddItem(TaskItem item)
        {
            lock (ThreadLock)
                NewTasksList.Add(item);
        }

        public static void DoneItem(TaskItem item)
        {
            lock (ThreadLock)
            {
                DoneTasksList.Add(item);
                NewTasksList.Remove(item);
            }
        }

        public static List<TaskItem> AllItemsCopy()
        {
            List<TaskItem> copyOfList = new List<TaskItem>();
            lock (ThreadLock)
            {
                foreach (var item in NewTasksList)
                {
                    copyOfList.Add(item);
                }
            }
            return copyOfList;
        }

        public static List<TaskItem> GetAllMyNewTasks(IEnumerable<int> tables)
        {
            lock (ThreadLock)
            {
                return NewTasksList.FindAll(task => tables.Contains(task.Sit.Table));
            }
        }

        public static List<TaskItem> GetAllMyDoneTasks(IEnumerable<int> tables)
        {
            lock (ThreadLock)
            {
                return DoneTasksList.FindAll(task => tables.Contains(task.Sit.Table));
            }
        }

        public static string LetterFromDigit(int digit)
        {
            return Char.ConvertFromUtf32((int)'A' + digit);
        }

        public static List<TaskItem> Reverse(List<TaskItem> list)
        {
            list.Reverse();
            return list;
        }

        public void FillList()
        {
            var rnd = new Random();
            var i = 0;
            do
            {
                AddItem(new TaskItem()
                {
                    Sit = new Coordinate()
                    {
                        Place = rnd.Next(4),
                        Table = rnd.Next(15)
                    },
                    Time = DateTime.Now,
                    TaskId = i++,
                    Kind = rnd.Next(4),
                    Done = false
                });
                Thread.Sleep(2000);
            } while (true);
        }
    }
}