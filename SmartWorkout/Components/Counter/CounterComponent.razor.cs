using Blazorise;
using Microsoft.AspNetCore.Components;

namespace SmartWorkout.Components.Counter
{
    public partial class CounterComponent : ComponentBase

    {
	    private bool isLoading = true;

	    private int currentCount = 0;

	    private void IncrementCount()
	    {
		    currentCount++;
	    }

	    private async Task ShowLoading()
	    {
		    isLoading = true;

		    await Task.Delay(TimeSpan.FromSeconds(3));

		    isLoading = false;
	    }
	}
}
