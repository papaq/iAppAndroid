using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V4.View;

namespace iAppAndroid
{
    public class SlidingTabsFragment : Fragment
    {
        // Horizontal scrollview
        private SlidingTabScrollView _SlidingTabScrollView;
        // provides a "swiping view"
        private ViewPager _ViewPager;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // get a hierarchy of the page
            return inflater.Inflate(Resource.Layout.fragment_swiper, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            // get tabs line
            _SlidingTabScrollView = view.FindViewById<SlidingTabScrollView>(Resource.Id.sliding_tabs);
            // get a page container
            _ViewPager = view.FindViewById<ViewPager>(Resource.Id.viewpager);
            // set a custom adapter
            _ViewPager.Adapter = new ListInsidePagerAdapter();

            // pass our container instance to =>
            _SlidingTabScrollView.ViewPager = _ViewPager;
        }

        public class ListInsidePagerAdapter : PagerAdapter
        {
            // titles of pages
            List<string> items = new List<string>();

            private TaskListViewAdapter _adapter;
            private ListView _listView;
            private List<TaskItem> _taskList;
            ViewGroup _container;
            int _position;

            // constructor sets values
            public ListInsidePagerAdapter() : base()
            {
                items.Add("My tasks");
                items.Add("All tasks");
                items.Add("Done tasks");
            }

            public override int Count
            {
                get { return items.Count; }
            }

            public override bool IsViewFromObject(View view, Java.Lang.Object obj)
            {
                return view == obj;
            }

            public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
            {
                _container = container;
                _position = position;

                View view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.CustomList, container, false);
                container.AddView(view);
                _listView = view.FindViewById<ListView>(Resource.Id.taskList);
                SetAdapter();
                _listView.ItemClick += ListViewItemClick;

                return view;
            }

            public string GetHeaderTitle(int position)
            {
                return items[position];
            }

            public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object obj)
            {
                _listView.ItemClick -= ListViewItemClick;
                _listView = null;

                container.RemoveView((View)obj);
            }

            private void ListViewItemClick(object sender, AdapterView.ItemClickEventArgs e)
            {
                var intent = new Intent(_container.Context, typeof(ExecuteTaskActivity));
                
                intent.PutExtra("task", "id");
                _container.Context.StartActivity(intent);
            }

            private void SetAdapter()
            {
                SetListItems();

                _adapter = new TaskListViewAdapter(_taskList, _container.Context);
                _listView.Adapter = _adapter;
            }

            private void SetListItems()
            {
                switch (_position)
                {
                    case 0:
                        _taskList = TaskList.GetAllMyNewTasks(TaskList.MyTables);
                        break;
                    case 1:
                        _taskList = TaskList.AllItemsCopy();
                        break;
                    default:
                        _taskList = TaskList.GetAllMyDoneTasks(TaskList.MyTables);
                        break;
                }


            }

            private bool UpdateAdapter()
            {
                SetListItems();
                _adapter.NotifyDataSetChanged();
                //_adapter.Dispose();
                //SetAdapter(whichList);
                return true;
            }
        }
    }
}