﻿using SphereOS.Core;
using SphereOS.Gui.UILib;
using System;
using System.Drawing;

namespace SphereOS.Gui.Apps
{
    internal class Tasks : Process
    {
        internal Tasks() : base("Tasks", ProcessType.Application) { }

        AppWindow window;

        Table table;

        WindowManager wm = ProcessManager.GetProcess<WindowManager>();

        int lastSecond = DateTime.Now.Second;

        private void PopulateTable()
        {
            table.Cells.Clear();
            foreach (Process process in ProcessManager.Processes)
            {
                table.Cells.Add(process.Name);
            }
            table.Render();
        }

        private void EndTaskClicked(int x, int y)
        {
            if (table.SelectedCell != -1 && table.SelectedCell < ProcessManager.Processes.Count)
            {
                ProcessManager.Processes[table.SelectedCell].TryStop();
                ProcessManager.Sweep();
            }
        }

        internal override void Start()
        {
            base.Start();
            window = new AppWindow(this, 256, 256, 384, 256);
            wm.AddWindow(window);
            window.Title = "Tasks";
            window.Closing = TryStop;

            window.Clear(Color.Gray);

            table = new Table(window, 12, 12, window.Width - 24, window.Height - 24 - 20 - 12);
            PopulateTable();
            wm.AddWindow(table);

            Button endTask = new Button(window, window.Width - 100 - 12, window.Height - 20 - 12, 100, 20);
            endTask.Text = "End Task";
            endTask.OnClick = EndTaskClicked;
            wm.AddWindow(endTask);

            wm.Update(window);
        }

        internal override void Run()
        {
            DateTime now = DateTime.Now;
            if (lastSecond != now.Second)
            {
                PopulateTable();
                lastSecond = now.Second;
            }
        }
    }
}
