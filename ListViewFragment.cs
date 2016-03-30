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

namespace iAppAndroid
{
    class ListViewFragment : Fragment
    {
        private ListView _listView;
        private LayoutInflater _inflater;
        private TaskListViewAdapter _adapter;

        // a list of tasks
        private List<TaskItem> _taskList;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _inflater = inflater;

            // get a hierarchy of the page
            return inflater.Inflate(Resource.Layout.CustomList, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            _listView = view.FindViewById<ListView>(Resource.Id.taskList);

            SetAdapter(0);
        }

        public override void OnDestroyView()
        {
            _listView.ItemClick -= ListViewItemClick;
            _listView = null;

            base.OnDestroyView();
        }

        private void ListViewItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SetAdapter(int number)
        {
            SetListItems(number);

            // set a custom adapter
            _adapter = new TaskListViewAdapter(_taskList, _inflater);
            _listView.Adapter = _adapter;
        }

        private void SetListItems(int number)
        {
            switch (number)
            {
                case 0:
                    _taskList = TaskList.GetAllMyNewTasks(TaskList.MyTables);
                    break;
                case 1:
                    _taskList = TaskList.AllItemsCopy();
                    break;
                default:
                    _taskList = TaskList.GetAllMyNewTasks(TaskList.MyTables);
                    _taskList.RemoveAll(task => !task.Done);
                    break;
            }
        }

        private bool UpdateAdapter(int number)
        {
            SetListItems(number);
            _adapter.NotifyDataSetChanged();
            //_adapter.Dispose();
            //SetAdapter(whichList);
            return true;
        }

        public class TaskListViewAdapter : BaseAdapter<TaskItem>
        {
            private readonly List<TaskItem> _items;
            //private readonly Context _activity;
            private LayoutInflater _inflater;

            public TaskListViewAdapter(List<TaskItem> items, LayoutInflater inflater)
            {
                _items = items;
                //_activity = activity;
                _inflater = inflater;
            }

            public override long GetItemId(int position)
            {
                return position;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                // get the item for the position pointed to
                var item = _items[position];

                // use the simple list adapter
                var view = convertView;
                if (view == null)
                {
                    view = _inflater.Inflate(Resource.Layout.TaskItem, null, false);
                }

                view.FindViewById<TextView>(Resource.Id.Time).Text = item.Time.ToString("t");
                view.FindViewById<TextView>(Resource.Id.Table).Text =
                    item.Sit.Table.ToString() + item.Sit.GetPlaceLetter();

                // get picture
                view.FindViewById<ImageView>(Resource.Id.TaskImage).SetImageResource(item.GetIdByKind());

                return view;
            }

            public override int Count => _items.Count;

            public override TaskItem this[int position] => _items[position];
        }
    }
}