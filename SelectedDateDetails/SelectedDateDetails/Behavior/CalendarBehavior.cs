using Syncfusion.Maui.Calendar;

namespace SelectedDateDetails
{
    public class CalendarBehavior : Behavior<SfCalendar>
    {
        private SfCalendar calendar;

        protected override void OnAttachedTo(SfCalendar bindable)
        {
            base.OnAttachedTo(bindable);
            this.calendar = bindable;
            this.calendar.SelectionChanged += Calendar_SelectionChanged;
        }

        private void Calendar_SelectionChanged(object sender, CalendarSelectionChangedEventArgs e)
        {
            App.Current.MainPage.DisplayAlert("Details shown by selection changed callback", "You have selected " + ((DateTime)e.NewValue).ToString("dddd, dd MMMM yyyy"), "OK");
        }

        protected override void OnDetachingFrom(SfCalendar bindable)
        {
            base.OnDetachingFrom(bindable);

            if (this.calendar != null)
            {
                this.calendar.SelectionChanged -= Calendar_SelectionChanged;
            }

            this.calendar = null;
        }
    }
}
