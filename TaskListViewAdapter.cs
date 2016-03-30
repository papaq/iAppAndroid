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
    internal class TaskListViewAdapter : BaseAdapter<TaskItem>
    {
        private readonly List<TaskItem> _items;
        private readonly Context _context;

        public TaskListViewAdapter(List<TaskItem> items, Context context)
        {
            _items = items;
            _context = context;
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
                view = LayoutInflater.From(_context).Inflate(Resource.Layout.TaskItem, null, false);
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
