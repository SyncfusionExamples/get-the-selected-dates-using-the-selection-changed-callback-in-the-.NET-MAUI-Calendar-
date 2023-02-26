using Syncfusion.Maui.Calendar;
using System.Collections.ObjectModel;

namespace SelectedDateDetails
{
    public class CalendarBehavior : Behavior<ContentPage>
    {
        private SfCalendar calendar;
        private Button selectionModeButton;

        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);
            
            this.calendar = bindable.FindByName<SfCalendar>("calendar");
            this.calendar.SelectionChanged += Calendar_SelectionChanged;
            this.selectionModeButton = bindable.FindByName<Button>("selectionModeButton");
            this.selectionModeButton.Text = this.calendar.SelectionMode.ToString();
            this.selectionModeButton.Clicked += SelectionModeButton_Clicked;
        }

        private void SelectionModeButton_Clicked(object sender, EventArgs e)
        {
            if (this.calendar.SelectionMode == CalendarSelectionMode.Single)
            {
                this.calendar.SelectionMode = CalendarSelectionMode.Multiple;
                this.selectionModeButton.Text = this.calendar.SelectionMode.ToString();
            }
            else if (this.calendar.SelectionMode == CalendarSelectionMode.Multiple)
            {
                this.calendar.SelectionMode = CalendarSelectionMode.Range;
                this.selectionModeButton.Text = this.calendar.SelectionMode.ToString();
            }
            else
            {
                this.calendar.SelectionMode = CalendarSelectionMode.Single;
                this.selectionModeButton.Text = this.calendar.SelectionMode.ToString();
            }
        }

        private void Calendar_SelectionChanged(object sender, CalendarSelectionChangedEventArgs e)
        {
            if (this.calendar.SelectionMode == CalendarSelectionMode.Single)
            {
                App.Current.MainPage.DisplayAlert("Date selected", ((DateTime)e.NewValue).ToString("dd MMMM yyyy"), "OK");
            }
            else if (this.calendar.SelectionMode == CalendarSelectionMode.Multiple)
            {
                var newDate = (ReadOnlyObservableCollection<DateTime>)e.NewValue;
                var oldDate = (ReadOnlyObservableCollection<DateTime>)e.OldValue;
                string newDateList = string.Empty;
                foreach (var item in newDate)
                {
                    newDateList += item.ToString("dd/MM/yyyy") + "\n";
                }

                if (newDate.Count > oldDate.Count)
                {
                    App.Current.MainPage.DisplayAlert("Date selected: " + newDate.ElementAt(newDate.Count - 1).ToString("dd MMMM yyyy"), 
                        "Count: " + newDate.Count.ToString() + "\n" + newDateList, "OK");
                }
                else
                {
                    foreach (var item in oldDate)
                    {
                        if (!newDate.Contains(item))
                        {
                            App.Current.MainPage.DisplayAlert("Date deselected: " + item.ToString("dd MMMM yyyy"), 
                                "Count: " + newDate.Count.ToString() + "\n" + newDateList, "Ok");
                        }
                    }
                }
            }
            else
            {
                var startDate = (DateTime)this.calendar.SelectedDateRange.StartDate;
                var endDate = (DateTime)((this.calendar.SelectedDateRange.EndDate != null) ? this.calendar.SelectedDateRange.EndDate : this.calendar.SelectedDateRange.StartDate);
                App.Current.MainPage.DisplayAlert("StartDate: " + startDate.ToString("dd/MM/yyyy"), 
                    "EndDate: " + endDate.ToString("dd/MM/yyyy"), "OK");
            }
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            base.OnDetachingFrom(bindable);
            if (this.calendar != null)
            {
                this.calendar.SelectionChanged -= Calendar_SelectionChanged;
            }

            if (this.selectionModeButton != null)
            {
                this.selectionModeButton.Clicked -= SelectionModeButton_Clicked;
            }

            this.calendar = null;
            this.selectionModeButton = null;
        }
    }
}
