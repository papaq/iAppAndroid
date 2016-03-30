using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading;

namespace iAppAndroid
{
    [Activity(Label = "TableApp", MainLauncher = true, Icon = "@drawable/iconTable")]
    public class MainActivity : Activity
    {
        private Thread _flowThread;
        private Thread _numberCounterThread;
        private TextView numberOfTasks;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var buttonMyTasks = FindViewById<Button>(Resource.Id.myTasks);
            //var buttonAllTasks = FindViewById<Button>(Resource.Id.allTasks);
            //var buttonDoneTasks = FindViewById<Button>(Resource.Id.archive);
            var buttonMap = FindViewById<Button>(Resource.Id.tablesMap);
            numberOfTasks = FindViewById<TextView>(Resource.Id.numberOfTasks);

            buttonMyTasks.TextSize *= Resources.DisplayMetrics.Density/5;
           // buttonAllTasks.TextSize *= Resources.DisplayMetrics.Density / 5;
            buttonMap.TextSize *= Resources.DisplayMetrics.Density / 5;
            //buttonDoneTasks.TextSize *= Resources.DisplayMetrics.Density / 5;

            AddHandler(buttonMyTasks, "my");
            //AddHandler(buttonAllTasks, "all");
            //AddHandler(buttonDoneTasks, "done");

            // create thread for making new tasks
            var taskList = new TaskList();
            _flowThread = new Thread(taskList.FillList);
            _flowThread.Start();


        }

        protected override void OnStart()
        {
            base.OnStart();

            // create thread for counting new tasks
            _numberCounterThread = new Thread(UpdateNumberOfTasks);
            _numberCounterThread.Start();
        }

        private void UpdateNumberOfTasks()
        {
            do
            {
                RunOnUiThread(() => numberOfTasks.Text = TaskList.CountNewTasks().ToString());
                Thread.Sleep(1000);
            } while (true);
        }

        private void CreateListActivity(string whichActivity)
        {
            var intent = new Intent(this, typeof(TaskFlowActivity));
            intent.PutExtra("list", whichActivity);
            StartActivity(intent);
        }

        private void AddHandler(Button button, string whichActivity)
        {
            button.Click += (sender, e) =>
            {
                CreateListActivity(whichActivity);
            };
        }

        protected override void OnStop()
        {
            base.OnStop();

            _numberCounterThread.Abort();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            // abort flowThread
            _flowThread.Abort();
        }
    }
}

