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
    [Activity(Label = "ExecuteTaskActivity")]
    public class ExecuteTaskActivity : Activity
    {
        Button button;
        TaskItem _item;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ExecuteTask);

            TextView task = FindViewById<TextView>(Resource.Id.ToDoTask);
            TextView table = FindViewById<TextView>(Resource.Id.ToDoTable);
            ImageView image = FindViewById<ImageView>(Resource.Id.ToDoImage);
            button= FindViewById<Button>(Resource.Id.ToDoDone);

            int TaskId;
            try
            {
                TaskId = Convert.ToInt16(Intent.GetStringExtra("task"));
            }
            catch (Exception)
            {
                TaskId = -1;
            }

            if (TaskId > -1)
            {
                _item = TaskList.NewTasksList.Find(t => t.TaskId == TaskId);
            }
            else
            {
                _item = TaskList.NewTasksList[0];
            }
            
            image.SetImageResource(_item.GetIdByKind());
            switch(_item.Kind)
            {
                case 0:
                    task.Text = "Come to the visitor!";
                    break;
                case 1:
                    task.Text = "Bring the bill!";
                    break;
                case 2:
                    task.Text = "Bring a fork!";
                    break;
                default:
                    task.Text = "Call a taxi!";
                    break;
            }

            table.Text = _item.Sit.Table.ToString() + TaskList.LetterFromDigit(_item.Sit.Place);

            button.Click += ButtonClick;
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            TaskList.DoneItem(_item);
            button.Click -= ButtonClick;
            
            ShowDialog();
        }

        private void ShowDialog()
        {
            // On "Call" button click, try to dial phone number.
            var callDialog = new AlertDialog.Builder(this);
            callDialog.SetMessage("Done!!");
            callDialog.SetNeutralButton("OK", delegate {
                Thread.Sleep(500);
                Finish();
            });

            // Show the alert dialog to the user and wait for response.
            callDialog.Show();
        }
    }
}