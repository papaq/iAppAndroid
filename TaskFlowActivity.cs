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
using Android.Support.V4.Widget;

namespace iAppAndroid
{
    [Activity(Label = "TaskFlowActivity")]
    public class TaskFlowActivity : Activity
    {
        //private TaskListViewAdapter _adapter;
        //private ListView _listView;
        //private List<TaskItem> _taskList;
        //private string _whichList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.TaskFlow);
            
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            SlidingTabsFragment fragment = new SlidingTabsFragment();
            transaction.Replace(Resource.Id.task_flow_fragment, fragment);
            transaction.Commit();
                        
        }
        
    }
}