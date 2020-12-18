namespace AutoCareV2._0.Class
{
    using System;

    // if you would like to use ArrayList insted
    using System.Collections.Generic;   // here we use Generic Type List<string>
    using System.Drawing;
    using System.Windows.Forms;

    // the component is derived from a TextBox
    // and is therfore called TextBox, but ment is this class (AutoCompleteTextbox)
    public sealed class AutoCompleteTextbox : TextBox
    {
        #region Fields

        // the ListBox used for suggestions
        private readonly ListBox _listBox;

        // string to remember a former input
        private string _oldText;

        // a Panel for displaying
        private readonly Panel _panel;

        #endregion Fields

        #region Constructors

        // the constructor
        public AutoCompleteTextbox()
        {
            // assigning some default values
            // minimum characters to be typed before suggestions are displayed
            MinTypedCharacters = 2;
            // not cases sensitive
            CaseSensitive = false;
            // the list for our suggestions database
            // the sample dictionary en-EN.dic is stored here when form1 is loaded (see form1.Load event)
            AutoCompleteList = new List<string>();

            // the listbox used for suggestions
            _listBox = new ListBox { Name = "SuggestionListBox", Font = Font, Visible = true, Height = 250};

            // the panel to hold the listbox later on
            _panel = new Panel();
            _panel.Visible = false;
            _panel.Font = Font;
            // to be able to fit to changing sizes of the parent form
            _panel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            // initialize with minimum size to avoid overlaping or flickering problems
            _panel.ClientSize = new Size(1, 1);
            _panel.Name = "SuggestionPanel";
            _panel.Padding = new Padding(0, 0, 0, 0);
            _panel.Margin = new Padding(0, 0, 0, 0);
            _panel.BackColor = Color.Transparent;
            _panel.ForeColor = Color.Transparent;
            _panel.Text = "";
            _panel.PerformLayout();
            // add the listbox to the panel if not already done
            if (!_panel.Controls.Contains(_listBox))
            {
                _panel.Controls.Add(_listBox);
            }

            // make the listbox fill the panel
            _listBox.Dock = DockStyle.Fill;
            // only one itme can be selected from the listbox
            _listBox.SelectionMode = SelectionMode.One;
            // the events to be fired if an item is selected
            _listBox.KeyDown += listBox_KeyDown;
            _listBox.MouseClick += listBox_MouseClick;
            _listBox.MouseDoubleClick += listBox_MouseDoubleClick;

            #region Excursus: ArrayList vs. List<string>

            // surpringly ArrayList is a little faster than List<string>
            // to use ArrayList instead, replace every 'List<string>' with 'ArrayList'
            // i will used List<string> cause it's generic type

            #endregion Excursus: ArrayList vs. List<string>

            // the list of suggestions actually displayed in the listbox
            // a subset of AutoCompleteList according to the typed in keyphrase
            CurrentAutoCompleteList = new List<string>();

            #region Excursus: DataSource vs. AddRange

            // using DataSource is faster than adding items (see
            // uncommented listBox.Items.AddRange method below)

            #endregion Excursus: DataSource vs. AddRange

            // Bind the CurrentAutoCompleteList as DataSource to the listbox
            _listBox.DataSource = CurrentAutoCompleteList;

            // set the input to remember, which is empty so far
            _oldText = Text;
        }

        #endregion Constructors

        #region Properties

        // the list for our suggestions database
        public List<string> AutoCompleteList
        {
            get;
            set;
        }

        // case sensitivity
        public bool CaseSensitive
        {
            get;
            set;
        }

        // minimum characters to be typed before suggestions are displayed
        public int MinTypedCharacters
        {
            get;
            set;
        }

        // the index selected in the listbox
        // maybe of intrest for other classes
        public int SelectedIndex
        {
            get
            {
                return _listBox.SelectedIndex;
            }
            set
            {
                // musn't be null
                if (_listBox.Items.Count != 0)
                { _listBox.SelectedIndex = value; }
            }
        }

        // the actual list of currently displayed suggestions
        private List<string> CurrentAutoCompleteList
        {
            set;
            get;
        }

        // the parent Form of this control
        private Form ParentForm
        {
            get { return Parent.FindForm(); }
        }

        #endregion Properties

        #region Methods

        // hides the listbox
        public void HideSuggestionListBox()
        {
            if ((ParentForm != null))
            {
                // hiding the panel also hides the listbox
                _panel.Hide();
                // now remove it from the ParentForm (Form1 in this example)
                if (ParentForm.Controls.Contains(_panel))
                {
                    ParentForm.Controls.Remove(_panel);
                }
            }
        }

        protected override void OnKeyDown(KeyEventArgs args)
        {
            // if user presses key.up
            if ((args.KeyCode == Keys.Up))
            {
                // move the selection in listbox one up
                MoveSelectionInListBox((SelectedIndex - 1));
                // work is done
                args.Handled = true;
            }
            // on key.down
            else if ((args.KeyCode == Keys.Down))
            {
                //move one down
                MoveSelectionInListBox((SelectedIndex + 1));
                args.Handled = true;
            }
            else if ((args.KeyCode == Keys.PageUp))
            {
                //move 10 up
                MoveSelectionInListBox((SelectedIndex - 10));
                args.Handled = true;
            }
            else if ((args.KeyCode == Keys.PageDown))
            {
                //move 10 down
                MoveSelectionInListBox((SelectedIndex + 10));
                args.Handled = true;
            }
            // on enter
            else if ((args.KeyCode == Keys.Enter))
            {
                // select the item in the ListBox
                SelectItem();
                args.Handled = true;
            }
            else
            {
                // work is not done, maybe the base class will process the event, so call it...
                base.OnKeyDown(args);
            }
        }

        // if the user leaves the TextBox, the ListBox and the panel ist hidden
        protected override void OnLostFocus(EventArgs e)
        {
            if (!_panel.ContainsFocus)
            {
                // call the baseclass event
                base.OnLostFocus(e);
                // then hide the stuff
                HideSuggestionListBox();
            }
        }

        // if the input changes, call ShowSuggests()
        protected override void OnTextChanged(EventArgs args)
        {
            // avoids crashing the designer
            if (!DesignMode)
                ShowSuggests();
            base.OnTextChanged(args);
            // remember input
            _oldText = Text;
        }

        // event for any key pressed in the ListBox
        private void listBox_KeyDown(object sender, KeyEventArgs e)
        {
            // on enter
            if (e.KeyCode == Keys.Enter)
            {
                // select the current item
                SelectItem();
                // work done
                e.Handled = true;
            }
        }

        // event for MouseClick in the ListBox
        private void listBox_MouseClick(object sender, MouseEventArgs e)
        {
            // select the current item
            SelectItem();
        }

        // event for DoubleClick in the ListBox
        private void listBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // select the current item
            SelectItem();
        }

        private void MoveSelectionInListBox(int index)
        {
            // beginning of list
            if (index <= -1)
            { SelectedIndex = 0; }
            else
                // end of liste
                if (index > (_listBox.Items.Count - 1))
                {
                    SelectedIndex = (_listBox.Items.Count - 1);
                }
                else
                // somewhere in the middle
                { SelectedIndex = index; }
        }

        // selects the current item
        private void SelectItem()
        {
            // if the ListBox is not empty
            if (((_listBox.Items.Count > 0) && (SelectedIndex > -1)))
            {
                // set the Text of the TextBox to the selected item of the ListBox
                Text = _listBox.SelectedItem.ToString();
                // and hide the ListBox
                HideSuggestionListBox();
            }
        }

        // shows the suggestions in ListBox beneath the TextBox
        // and fitting it into the ParentForm
        private void ShowSuggests()
        {
            // show only if MinTypedCharacters have been typed
            if (Text.Length >= MinTypedCharacters)
            {
                // prevent overlapping problems with other controls
                // while loading data there is nothing to draw, so suspend layout
                _panel.SuspendLayout();
                // user is typing forward, char has been added at the end of the former input
                if ((Text.Length > 0) && (_oldText == Text.Substring(0, Text.Length - 1)))
                {
                    //handle forward typing with refresh
                    UpdateCurrentAutoCompleteList();
                }
                // user is typing backward - char bas been removed
                else if ((_oldText.Length > 0) && (Text == _oldText.Substring(0, _oldText.Length - 1)))
                {
                    //handle backward typing with refresh
                    UpdateCurrentAutoCompleteList();
                }
                // something within has changed
                else
                {
                    // handle other things like corrections in the middle of the input, clipboard pastes, etc. with refresh
                    UpdateCurrentAutoCompleteList();
                }

                if (((CurrentAutoCompleteList != null) && CurrentAutoCompleteList.Count > 0))
                {
                    // finally show Panel and ListBox
                    // (but after refresh to prevent drawing empty rectangles)
                    _panel.Show();
                    // at the top of all controls
                    _panel.BringToFront();
                    // then give the focuse back to the TextBox (this control)
                    Focus();
                }
                // or hide if no results
                else
                {
                    HideSuggestionListBox();
                }
                // prevent overlapping problems with other controls
                _panel.ResumeLayout(true);
            }
            // hide if typed chars <= MinCharsTyped
            else
            {
                HideSuggestionListBox();
            }
        }

        // This is a timecritical part
        // Fills/ refreshed the CurrentAutoCompleteList with appropreate candidates
        private void UpdateCurrentAutoCompleteList()
        {
            // the list of suggestions has to be refreshed so clear it
            CurrentAutoCompleteList.Clear();
            // an find appropreate candidates for the new CurrentAutoCompleteList in AutoCompleteList
            foreach (string str in AutoCompleteList)
            {
                // be casesensitive
                if (CaseSensitive)
                {
                    // search for the substring (equal to SQL Like Command)
                    if ((str.IndexOf(Text, StringComparison.Ordinal) > -1))
                    // Add candidates to new CurrentAutoCompleteList
                    { CurrentAutoCompleteList.Add(str); }
                }
                // or ignore case
                else
                {
                    // and search for the substring (equal to SQL Like Command)
                    if ((str.ToLower().IndexOf(Text.ToLower(), StringComparison.Ordinal) > -1))
                    // Add candidates to new CurrentAutoCompleteList
                    { CurrentAutoCompleteList.Add(str); }
                }
            }

            #region Excursus: Performance measuring of Linq queries

            // This is a simple example of speedmeasurement for Linq querries
            /*
            CurrentAutoCompleteList.Clear();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            // using Linq query seems to be slower (twice as slow)
            var query =
                from expression in this.AutoCompleteList
                where expression.ToLower().Contains(this.Text.ToLower())
                select expression;
            foreach (string searchTerm in query)
            {
                CurrentAutoCompleteList.Add(searchTerm);
            }
            stopWatch.Stop();
            // method (see below) for printing the performance values to console
            PrintStopwatch(stopWatch, "Linq - Contains");
            */

            #endregion Excursus: Performance measuring of Linq queries

            // countinue to update the ListBox - the visual part
            UpdateListBoxItems();
        }

        // This is the most timecritical part, adding items to the ListBox
        private void UpdateListBoxItems()
        {
            #region Excursus: DataSource vs. AddRange

            /*
                    // This part is not used due to 'listBox.DataSource' approach (see constructor)
                    // left for comparison, delete for productive use
                    listBox.BeginUpdate();
                    listBox.Items.Clear();
                    //Fills the ListBox
                    listBox.Items.AddRange(this.CurrentAutoCompleteList.ToArray());
                    listBox.EndUpdate();
                    // to use this method remove the following
                    // "((CurrencyManager)listBox.BindingContext[CurrentAutoCompleteList]).Refresh();" line and
                    // "listBox.DataSource = CurrentAutoCompleteList;" line from the constructor
                    */

            #endregion Excursus: DataSource vs. AddRange

            // if there is a ParentForm
            if ((ParentForm != null))
            {
                // get its width
                _panel.Width = Width;
                // calculate the remeining height beneath the TextBox
                _panel.Height = 200;//ParentForm.ClientSize.Height - Height - Location.Y;
                // and the Location to use
                _panel.Location = Location + new Size(189, 209);
                // Panel and ListBox have to be added to ParentForm.Controls before calling BingingContext
                if (!ParentForm.Controls.Contains(_panel))
                {
                    // add the Panel and ListBox to the PartenForm
                    ParentForm.Controls.Add(_panel);
                }
                // Update the listBox manually - List<string> dosn't support change events
                // this is the DataSource approche, this is a bit tricky and may cause conflicts,
                // so in case fall back to AddRange approache (see Excursus)
                ((CurrencyManager)_listBox.BindingContext[CurrentAutoCompleteList]).Refresh();
            }
        }

        #endregion Methods

        #region Other

        /*
        // only needed for performance measuring - delete for productional use
        private void PrintStopwatch(Stopwatch stopWatch, string comment)
        {
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}h:{1:00}m:{2:00},{3:000}s \t {4}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds, comment);
            Console.WriteLine("RunTime " + elapsedTime);
        }
        */

        #endregion Other
    }
}